using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Coordinare.Models;

namespace Coordinare.Interfaces
{
    public interface IUserCatalog
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserFromIdAsync(int id);
        Task<bool> CreateUserAsync(User user);
        Task<User> DeleteUserAsync(int id);
        Task<bool> UpdateUserAsync(User user, int id);
    }
}
