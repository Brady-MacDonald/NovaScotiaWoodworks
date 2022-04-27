using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public interface IUserData
    {
        Task<IEnumerable<UserModel>> GetUsers();
        Task InsertUser(UserModel user);
    }
}