using Application.Common.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Requests.Authorization.Command.Register;

public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;

    public RegisterHandler(IUserRepository userRepository, UserManager<User> userManager, IMapper mapper)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userIsExist = await _userRepository.GetByEmailAsync(request.Email);
        var userIsExist2 = await _userRepository.GetByUserNameAsync(request.Username);

        if (userIsExist is not null || userIsExist2 is not null)
        {
            return false;
        }
        
        var newUser = _mapper.Map<User>(request);
        
        var createNewUser = await _userManager.CreateAsync(newUser, request.Password);
        var addRole = await _userManager.AddToRoleAsync(newUser, "User");

        return addRole.Succeeded && createNewUser.Succeeded;
    }
}