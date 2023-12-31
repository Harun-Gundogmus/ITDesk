﻿using FluentValidation;
using ITDeskServer.DTOs;
using ITDeskServer.Extensions;

namespace ITDeskServer.Validator;

public sealed class LoginValidator: AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(p => p.UserNameOrEmail).NotEmpty().WithMessage("Geçerli bir kullanıcı adı ya da mail adresi girin");
        RuleFor(p => p.UserNameOrEmail).MinimumLength(3).WithMessage("Geçerli bir kullanıcı adı ya da mail adresi girin");

        RuleFor(p => p.Password).NotEmpty().WithMessage("Şifre alanı boş geçilemez.");
        RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Şifre en az 1 adet büyük harf içermelidir.");
        RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Şifre en az 1 adet küçük harf içermelidir.");
        RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Şifre en az 1 adet rakam içermelidir.");
        RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Şifre en az 1 adet özel karakter içermelidir.");
    }
}
