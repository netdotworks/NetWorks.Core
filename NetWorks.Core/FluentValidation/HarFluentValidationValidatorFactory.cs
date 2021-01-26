using Autofac;
using FluentValidation;
using System;

namespace NetWorks.Core.FluentValidation
{
    public class HarFluentValidationValidatorFactory : ValidatorFactoryBase
    {
        private readonly IComponentContext _context;

        public HarFluentValidationValidatorFactory(IComponentContext context)
        {
            _context = context;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            if (_context.IsRegistered(validatorType))
            {
                return _context.Resolve(validatorType) as IValidator;
            }
            return null;
        }
    }
}