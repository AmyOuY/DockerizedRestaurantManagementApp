using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IDiningTableData
    {
        Task DeleteDiningTable(int id);
        Task<List<DiningTableModel>> GetAllDiningTables();
        Task<DiningTableModel> GetDiningTableById(int id);
        Task<DiningTableModel> GetDiningTableByTableNumber(int tableNumber);
        Task InsertDiningTable(DiningTableModel table);
        Task UpdateDiningTable(DiningTableModel table);
        Task<bool> IsValidTableNumber(int tableNumber);
    }
}