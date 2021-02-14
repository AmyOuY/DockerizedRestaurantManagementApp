using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RepositoryController : Controller
    {
        private readonly IOrderData _order;
        private readonly IPersonData _person;
        private readonly IDiningTableData _table;
        private readonly IFoodData _food;

        public RepositoryController(IOrderData order, IPersonData person, IDiningTableData table, IFoodData food)
        {
            _order = order;
            _person = person;
            _table = table;
            _food = food;
        }


        public IActionResult Index()
        {
            return View();
        }


        // View all order details in the database
        public async Task<IActionResult> ViewAllOrderDetails()
        {
            List<OrderDetailModel> orderDetails = await _order.GetAllOrderDetails();
            List<OrderDetailDisplayModel> displayDetails = new List<OrderDetailDisplayModel>();

            foreach (var detail in orderDetails)
            {
                DiningTableModel table = await _table.GetDiningTableById(detail.DiningTableId);
                PersonModel attendant = await _person.GetPerson(detail.AttendantId);
                FoodModel food = await _food.GetFoodById(detail.FoodId);

                displayDetails.Add(new OrderDetailDisplayModel { 
                    Id = detail.Id,
                    TableNumber = table.TableNumber,
                    Attendant = attendant.FirstName + " " + attendant.LastName,
                    FoodName = food.FoodName,
                    Price = food.Price,
                    Quantity = detail.Quantity,
                    OrderDate = detail.OrderDate,
                    OrderId = detail.OrderId
                });
            }

            return View(displayDetails);
        }


        // View all orders in the database
        public async Task<IActionResult> ViewAllOrders()
        {
            List<OrderModel> orders = await _order.GetAllOrders();
            List<OrderDisplayModel> displayOrders = new List<OrderDisplayModel>();

            foreach (var order in orders)
            {
                DiningTableModel table = await _table.GetDiningTableById(order.DiningTableId);
                PersonModel attendant = await _person.GetPerson(order.AttendantId);

                displayOrders.Add(new OrderDisplayModel { 
                    Id = order.Id,
                    TableNumber = table.TableNumber,
                    Attendant = attendant.FirstName + " " + attendant.LastName,
                    SubTotal = order.SubTotal,
                    Tax = order.Tax,
                    Total = order.Total,
                    CreatedDate = order.CreatedDate,
                    BillPaid = order.BillPaid
                });
            }

            return View(displayOrders);
        }
    
    }
}