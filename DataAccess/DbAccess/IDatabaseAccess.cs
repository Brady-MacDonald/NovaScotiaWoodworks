
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.DbAccess
{
    public interface IDatabaseAccess
    {
        Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters, string connectionId = "LocalDefault");
        Task SaveData<T>(string storedProcedure, T parameters, string connectionId = "LocalDefault");
    }
}