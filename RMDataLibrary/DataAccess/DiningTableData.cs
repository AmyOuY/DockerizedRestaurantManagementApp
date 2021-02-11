using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class DiningTableData : IDiningTableData
    {
        private readonly ISqlDataAccess _sql;

        public DiningTableData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        public async Task InsertDiningTable(DiningTableModel table)
        {
            await _sql.SaveData("spDiningTable_Insert", table);
        }


        public async Task<List<DiningTableModel>> GetAllDiningTables()
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("spDiningTable_GetAll", new { });

            return results;
        }


        public async Task<DiningTableModel> GetDiningTableById(int id)
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("spDiningTable_GetById", new { id });

            return results.FirstOrDefault();
        }


        public async Task<DiningTableModel> GetDiningTableByTableNumber(int tableNumber)
        {
            var results = await _sql.LoadData<DiningTableModel, dynamic>("spDiningTable_GetByTableNumber", new { tableNumber });

            return results.FirstOrDefault();
        }


        public async Task UpdateDiningTable(DiningTableModel table)
        {
            await _sql.SaveData("spDiningTable_Update", table);
        }


        public async Task DeleteDiningTable(int id)
        {
            await _sql.DeleteData("spDiningTable_Delete", new { id });
        }


        public async Task<bool> IsValidTableNumber(int tableNumber)
        {
            var allDiningTables = await _sql.LoadData<DiningTableModel, dynamic>("spDiningTable_GetAll", new { });
            HashSet<int> allTableNumbers = new HashSet<int>(allDiningTables.Select(t => t.TableNumber));

            return allTableNumbers.Contains(tableNumber);
        }
    }
}
