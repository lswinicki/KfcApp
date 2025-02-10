using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository : IBasicRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByUserNameAsync(string userName);
}
