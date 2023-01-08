﻿using System.Security.Claims;
using BankApplication.Application.Common.Errors;
using BankApplication.Application.Common.Interfaces.Persistence;
using BankApplication.Application.Customers.Response.Commands;
using Domain.Domains;
using Domain.Models;
using MediatR;

namespace BankApplication.Application.Customers.Commands;

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResult>
{
    private readonly ICustomersRepository _customerRepository;

    public CreateAccountCommandHandler(ICustomersRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<CreateAccountResult> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {

        var result = await _customerRepository.CreateAccount(request.UserId, request.Frequency);
        if (result is not Account account)
        {
            throw new InvalidFrequency();
        }
        result.User = new User { UserId = request.UserId };
        return new CreateAccountResult(result);
    }
}

