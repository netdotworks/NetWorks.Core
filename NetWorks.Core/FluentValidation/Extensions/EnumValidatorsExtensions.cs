using FluentValidation;
using System;

namespace NetWorks.Core.FluentValidation.Extensions
{
    public static class EnumValidatorsExtensions
    {
        public static IRuleBuilderOptions<T, int> ContainsEnumValues<T>(this IRuleBuilder<T, int> builder, Type enumType)
        {
            return builder.Must(v => Enum.IsDefined(enumType, v)).WithMessage(ErrorMessage(enumType));
        }

        private static string ErrorMessage(Type type)
        {
            var message = "{PropertyName} contains: {PropertyValue}. Available values are: ";
            foreach (var item in Enum.GetValues(type))
            {
                message += $"{item} -> {(int)item}, ";
            }

            return message;
        }
    }
}