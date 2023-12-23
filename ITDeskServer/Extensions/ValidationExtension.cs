using FluentValidation.Validators;
using FluentValidation;
using System.Reflection.Metadata.Ecma335;

namespace ITDeskServer.Extensions;

public static class ValidationExtension
{
    public static IRuleBuilderOptions<T, TProperty> NotEmptyOrNull<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        return ruleBuilder.SetValidator(new NotEmptyValidator<T, TProperty>());
    }

}
