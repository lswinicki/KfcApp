using Domain.Entities;

namespace Application.Common.Interfaces.Repositories;

public interface IUserRepository : IBasicRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
}
