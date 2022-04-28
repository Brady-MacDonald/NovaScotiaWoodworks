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

        Task<IEnumerable<OrderModel>> GetOrders();
        Task<OrderModel?> GetOrderByProduct(string product);
        Task<IEnumerable<OrderModel>> GetOrderByUserName(string username);
        Task InsertOrder(OrderModel order);
        Task DeleteOrder(int id);


    }
}