﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using NetWorks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace NetWorks.Core.DataAccess.Repositories
{
    public class BaseRepo<T> : IRepo<T> where T : BaseEntity, new()
    {
        protected readonly DatabaseContext Db;

        private readonly bool _disposeContext;
        private IDbContextTransaction _transaction;

        public DbSet<T> Table { get; }

        public DatabaseContext Context => Db;

        public BaseRepo(DbContextOptions<DatabaseContext> options) : this(new DatabaseContext(options))
        {
            _disposeContext = true;
        }

        protected BaseRepo(DatabaseContext context)
        {
            Db = context;
            Table = Db.Set<T>();
        }

        public int Count => Table.Count();

        public bool HasChanges => Db.ChangeTracker.HasChanges();

        public bool Any() => Table.Any();

        public bool Any(Expression<Func<T, bool>> where) => Table.Any(where);

        public virtual IEnumerable<T> GetAll() => Table;

        public IEnumerable<T> GetAll<TIncludeField>(Expression<Func<T, TIncludeField>> include)
            => Table.Include(include);

        public IEnumerable<T> GetAll<TSortField>(Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.OrderBy(orderBy) : Table.OrderByDescending(orderBy);

        public IEnumerable<T> GetAll<TIncludeField, TSortField>(
            Expression<Func<T, TIncludeField>> include, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Include(include).OrderBy(orderBy) : Table.Include(include).OrderByDescending(orderBy);

        public T First() => Table.FirstOrDefault();

        public T First(Expression<Func<T, bool>> where) => Table.FirstOrDefault(where);

        public T First<TIncludeField>(Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include).FirstOrDefault();

        public T First<TSortField>(Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending)
        {
            return ascending ? Table.Where(where).OrderBy(orderBy).FirstOrDefault() : Table.Where(where).OrderByDescending(orderBy).FirstOrDefault();
        }

        public T Find(int id) => Table.Find(id);

        public T Find(Expression<Func<T, bool>> where)
            => Table.Where(where).FirstOrDefault();

        public T Find<TIncludeField>(Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include)
            => Table.Where(@where).Include(include).FirstOrDefault();

        public IEnumerable<T> GetSome(Expression<Func<T, bool>> where)
            => Table.Where(where);

        public IEnumerable<T> GetSome<TIncludeField>(Expression<Func<T, bool>> where,
            Expression<Func<T, TIncludeField>> include)
            => Table.Where(where).Include(include);

        public IEnumerable<T> GetSome<TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ? Table.Where(where).OrderBy(orderBy) : Table.Where(where).OrderByDescending(orderBy);

        public IEnumerable<T> GetSome<TIncludeField, TSortField>(
            Expression<Func<T, bool>> where, Expression<Func<T, TIncludeField>> include,
            Expression<Func<T, TSortField>> orderBy, bool ascending)
            => ascending ?
                Table.Where(where).OrderBy(orderBy).Include(include) :
                Table.Where(where).OrderByDescending(orderBy).Include(include);

        public IEnumerable<T> FromSql(string sqlString)
            => Table.FromSqlRaw(sqlString);

        public virtual IEnumerable<T> GetRange(int skip, int take)
            => GetRange(Table, skip, take);

        public IEnumerable<T> GetRange(IQueryable<T> query, int skip, int take)
            => query.Skip(skip).Take(take);

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int AddReturnEntityId(T entity, bool persist = true)
        {
            Table.Add(entity);
            SaveChanges();
            return entity.Id;
        }

        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Update(T entity, bool persist = true)
        {
            Table.Update(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }

        public virtual int Delete(T entity, bool persist = true)
        {
            Table.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }

        internal T GetEntryFromChangeTracker(int? id)
        {
            return Db.ChangeTracker.Entries<T>()
                .Select((EntityEntry e) => (T)e.Entity)
                    .FirstOrDefault(x => x.Id == id);
        }

        //TODO: Check For Cascade Delete
        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entry = GetEntryFromChangeTracker(id);
            if (entry != null)
            {
                if (timeStamp != null && entry.Timestamp.SequenceEqual(timeStamp))
                {
                    return Delete(entry, persist);
                }
                throw new Exception("Unable to delete due to concurrency violation.");
            }
            Db.Entry(new T { Id = id, Timestamp = timeStamp }).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }

        public int SaveChanges()
        {
            try
            {
                return Db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                //A concurrency error occurred
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (RetryLimitExceededException ex)
            {
                //DbResiliency retry limit exceeded
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
            catch (Exception ex)
            {
                //Should handle intelligently
                Console.WriteLine(ex);
                throw;
            }
        }

        public void BeginTransaction()
        {
            _transaction = Context.Database.BeginTransaction(IsolationLevel.RepeatableRead);
        }

        public void CommitTransaction()
        {
            _transaction.Commit();
        }

        public void RollbackTransaction()
        {
            _transaction.Rollback();
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                //
            }
            if (_disposeContext)
            {
                Db.Dispose();
            }
            _disposed = true;
        }
    }
}