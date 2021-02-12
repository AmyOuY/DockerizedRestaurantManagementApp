using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IOrderData
    {
        Task DeleteOrder(int id);
        Task DeleteOrderDetail(int id);
        Task<List<OrderModel>> GetAllUnpaidOrders();
        Task<OrderModel> GetOrderById(int id);
        Task<List<OrderDetailModel>> GetOrderDetailsByDiningTableIdUnpaid(int tableId);
        Task<OrderDetailModel> GetOrderDetailById(int id);
        Task<OrderModel> GetUnpaidOrderByDiningTableId(int id);
        Task InsertOrderByTableId(int tableId);
        Task InsertOrderDetail(OrderDetailModel detail);
        Task UpdateOrder(OrderModel order);
        Task UpdateOrderDetail(OrderDetailModel detail);
        Task<List<OrderDetailModel>> GetAllOrderDetails();
        Task<List<OrderModel>> GetAllOrders();
    }
}