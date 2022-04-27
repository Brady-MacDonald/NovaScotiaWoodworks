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
    }
}
