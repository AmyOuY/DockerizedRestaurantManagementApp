using Microsoft.Extensions.Configuration;
using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class OrderData : IOrderData
    {
        private readonly ISqlDataAccess _sql;
        private readonly IFoodData _food;
        private readonly IConfiguration _config;

        public OrderData(ISqlDataAccess sql, IFoodData food, IConfiguration config)
        {
            _sql = sql;
            _food = food;
            _config = config;
        }


        // Insert detail of one ordered food for a specific dining table to the database
        // detail includes Id, dining table Id, attendant Id, food Id, quantity, order date and order Id
        public async Task InsertOrderDetail(OrderDetailModel detail)
        {
            await _sql.SaveData("spOrderDetail_Insert", detail);
        }


        // Get a specific ordered food detail with Id = id
        public async Task<OrderDetailModel> GetOrderDetailById(int id)
        {
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("spOrderDetail_GetById", new { id });

            return results.FirstOrDefault();
        }


        // Get list of food ordered details for a specific dining table with Id = tableId 
        public async Task<List<OrderDetailModel>> GetOrderDetailsByDiningTableIdUnpaid(int tableId)
        {
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("spOrderDetail_GetByDiningTableIdUnpaid", new { DiningTableId = tableId });

            return results;
        }


        // Update an ordered food detail
        public async Task UpdateOrderDetail(OrderDetailModel detail)
        {
            await _sql.SaveData("spOrderDetail_Update", detail);
        }


        // Delete a specific ordered food detail with Id = id
        public async Task DeleteOrderDetail(int id)
        {
            await _sql.DeleteData("spOrderDetail_Delete", new { id });
        }


        // Retrieve Tax Rate info from appsettings.json 
        private decimal GetTaxRate()
        {
            string rateText = _config.GetValue<string>("TaxRate");

            bool isValidTaxRate = decimal.TryParse(rateText, out decimal output);

            if (isValidTaxRate == false)
            {
                throw new ConfigurationErrorsException("The tax rate is not properly set.");
            }

            output /= 100;

            return output;
        }


        // Insert summary of order by a specific dining table to the database
        // summary includes Id, dining table Id, attendant Id, subTotal, tax, total, date and bill paid info
        // Each dining table has a specific unpaid order summary
        public async Task InsertOrderByTableId(int tableId)
        {
            OrderModel order = new OrderModel();
            List<OrderDetailModel> orderDetails = await GetOrderDetailsByDiningTableIdUnpaid(tableId);

            if (orderDetails == null) return;

            order.DiningTableId = tableId;
            order.AttendantId = orderDetails[0].AttendantId;

            foreach (var detail in orderDetails)
            {
                FoodModel food = await _food.GetFoodById(detail.FoodId);
                order.SubTotal += detail.Quantity * food.Price;
            }

            decimal taxRate = GetTaxRate();
            order.Tax = order.SubTotal * taxRate;
            order.Total = order.SubTotal + order.Tax;

            await _sql.SaveData("spOrder_Insert", order);
        }


        // Get a specific order summary with Id = id
        public async Task<OrderModel> GetOrderById(int id)
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("spOrder_GetById", new { id });

            return results.FirstOrDefault();
        }


        // Get order summary for all the unpaid dining tables
        public async Task<List<OrderModel>> GetAllUnpaidOrders()
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("spOrder_GetAllUnpaid", new { });

            return results;
        }


        // Get summary of order for a specific unpaid dining table with Id = tableId
        public async Task<OrderModel> GetUnpaidOrderByDiningTableId(int id)
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("spOrder_GetByDiningTableIdUnpaid", new { DiningTableId = id });

            return results.FirstOrDefault();
        }


        // Update an order summary
        public async Task UpdateOrder(OrderModel order)
        {
            await _sql.SaveData("spOrder_Update", order);
        }


        // Delete a specific order summary with Id = id
        public async Task DeleteOrder(int id)
        {
            await _sql.DeleteData("spOrder_Delete", new { id });
        }


        // Get all order details in database
        public async Task<List<OrderDetailModel>> GetAllOrderDetails()
        {
            var results = await _sql.LoadData<OrderDetailModel, dynamic>("spOrderDetail_GetAll", new { });

            return results;
        }


        // Get all orders in database
        public async Task<List<OrderModel>> GetAllOrders()
        {
            var results = await _sql.LoadData<OrderModel, dynamic>("spOrder_GetAll", new { });

            return results;
        }
    }
}
