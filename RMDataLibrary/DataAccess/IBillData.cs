using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IBillData
    {
        Task InsertBill(BillModel bill);
        Task<List<BillModel>> GetAllPaidBills();
    }
}