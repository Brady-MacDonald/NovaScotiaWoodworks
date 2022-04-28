using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class UserData : IUserData
    {
        private readonly IDatabaseAccess _db;

        public UserData(IDatabaseAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<UserModel>> GetUsers() =>
            _db.LoadData<UserModel, dynamic>("dbo.spUsers_GetAll", new { });

        public async Task<UserModel?> GetUser(string username)
        {
            var results = await _db.LoadData<UserModel, dynamic>(
                "dbo.spUsers_Get", new { UserName = username });

            return results.FirstOrDefault();
        }

        public Task InsertUser(UserModel user) =>
            //Dont need to put the equals sign so it assums the name of the param will be same as property name(same capitilization)
            _db.SaveData("dbo.spUsers_Insert", new
            {
                user.FirstName,
                user.LastName,
                user.EmailAddress,
                user.UserName,
                user.Password,
                user.Salt
            });

        public Task UpdateUser(UserModel user) =>
            _db.SaveData("dbo.spUsers_Update", user);

        public Task DeleteUser(int id) =>
            _db.SaveData("dbo.spUsers_Delete", new { Id = id });


        public Task<IEnumerable<OrderModel>> GetOrders() =>
            _db.LoadData<OrderModel, dynamic>("dbo.spOrders_GetAll", new { });

        public async Task<OrderModel?> GetOrderByProduct(string product)
        {
            var results = await _db.LoadData<OrderModel, dynamic>(
                "dbo.spOrders_GetByProduct", new { Product = product });

            return results.FirstOrDefault();
        }

        public async Task<IEnumerable<OrderModel>?> GetOrderByUserName(string username)
        {
            var results = await _db.LoadData<OrderModel, dynamic>(
                "dbo.spOrders_GetByUserName", new { UserName = username });

            return results;
        }

        public Task InsertOrder(OrderModel order) =>
            //Dont need to put the equals sign so it assums the name of the param will be same as property name(same capitilization)
            _db.SaveData("dbo.spOrders_Insert", new
            {
                order.UserName,
                order.EmailAddress,
                order.Product,
                order.OrderTime,
                order.Status
            });


        public Task DeleteOrder(int id) =>
            _db.SaveData("dbo.spOrders_Delete", new { Id = id });
    }
}
