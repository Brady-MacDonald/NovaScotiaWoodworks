using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<IEnumerable<UserModel>> GetUsers();
        Task<UserModel?> GetUser(string username);
        Task InsertUser(UserModel user);
        Task UpdateUser(UserModel user);
        Task DeleteUser(int id);

    }
}