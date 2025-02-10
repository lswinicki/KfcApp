using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Authorization.Command.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;

    public RegisterHandler(IUserRepository userRepository, UserManager<User> userManager)
    {
        _userRepository = userRepository;
        _userManager = userManager;
    }

    public Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}