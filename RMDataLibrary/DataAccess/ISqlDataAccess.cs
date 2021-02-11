using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        Task DeleteData<T>(string storedProcedure, T parameters);
        string GetConnectionString();
        Task<List<T>> LoadData<T, U>(string storedProcedure, U parameters);
        Task SaveData<T>(string storedProcedure, T parameters);
    }
}