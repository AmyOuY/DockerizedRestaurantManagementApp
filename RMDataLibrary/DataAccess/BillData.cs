using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class BillData : IBillData
    {
        private readonly ISqlDataAccess _sql;

        public BillData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        public async Task InsertBill(BillModel bill)
        {
            await _sql.SaveData("spOrder_UpdateBillPaid", new { Id = bill.OrderId });
            await _sql.SaveData("spOrderDetail_UpdateBillPaid", new { bill.DiningTableId, bill.OrderId });
            await _sql.SaveData("spBill_Insert", bill);
        }


        public async Task<List<BillModel>> GetAllPaidBills()
        {
            var results = await _sql.LoadData<BillModel, dynamic>("spBill_GetAllPaid", new { });

            return results;
        }
    }
}
