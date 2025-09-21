﻿using Application.DTO.Base;
using Application.Factory.Base;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Factory
{
    public class ValidatorFactory(IServiceProvider validationServices) : IValidatorFactoryBase
    {
        public IValidator<T> GetValidator<T>() where T : IDataTransferObjectBase
        {
            var validator = validationServices.GetRequiredService<IValidator<T>>();
            if (validator == null)
                throw new ArgumentNullException($"Validator for type {typeof(T).Name} not registered");
            return validator;
        }
    }
}
