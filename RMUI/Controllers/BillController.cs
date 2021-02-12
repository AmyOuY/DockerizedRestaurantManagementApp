using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    public class BillController : Controller
    {
        private readonly IBillData _bill;
        private readonly IOrderData _order;
        private readonly IDiningTableData _table;
        private readonly IPersonData _person;
        private readonly IFoodData _food;

        public BillController(IBillData bill, IOrderData order, IDiningTableData table, IPersonData person, IFoodData food)
        {
            _bill = bill;
            _order = order;
            _table = table;
            _person = person;
            _food = food;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> ViewBillByTable(int tableNumber)
        {
            if (await _table.IsValidTableNumber(tableNumber) == false)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! Table Number {tableNumber} does not exist!" });
            }

            DiningTableModel table = await _table.GetDiningTableByTableNumber(tableNumber);
            OrderModel order = await _order.GetUnpaidOrderByDiningTableId(table.Id);

            if (order == null)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! There is no currently active order for Table {tableNumber}!" });
            }

            PersonModel attendant = await _person.GetPerson(order.AttendantId);
            
            BillDisplayModel bill = new BillDisplayModel { 
                OrderId = order.Id,
                TableNumber = tableNumber,
                Attendant = attendant.FirstName + " " + attendant.LastName,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Total = order.Total,
                BillPaid = order.BillPaid
            };

            List<OrderDetailModel> orderDetails = await _order.GetOrderDetailsByDiningTableIdUnpaid(table.Id);

            if (orderDetails == null)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! There is no currently active order for Table Number {tableNumber}!" });
            }
            else
            {
                foreach (var detail in orderDetails)
                {
                    FoodModel food = await _food.GetFoodById(detail.FoodId);

                    bill.OrderDetails.Add(new OrderDetailDisplayModel { 
                        Id = detail.Id,
                        TableNumber = tableNumber,
                        Attendant = attendant.FirstName + " " + attendant.LastName,
                        FoodName = food.FoodName,
                        Price = food.Price,
                        Quantity = detail.Quantity,
                        OrderDate = detail.OrderDate
                    });
                }
            }

            return View(bill);
        }


        public IActionResult SearchBill()
        {
            return View();
        }


        public async Task<IActionResult> PayBill(BillDisplayModel displayBill)
        {
            DiningTableModel table = await _table.GetDiningTableByTableNumber(displayBill.TableNumber);
            string[] fullName = displayBill.Attendant.Split(" ");
            PersonModel attendant = await _person.GetPersonByFullName(fullName[0], fullName[1]);

            BillModel bill = new BillModel { 
                OrderId = displayBill.OrderId,
                DiningTableId = table.Id,
                AttendantId = attendant.Id,
                SubTotal = displayBill.SubTotal,
                Tax = displayBill.Tax,
                Total = displayBill.Total,
                BillPaid = true
            };

            await _bill.InsertBill(bill);

            return RedirectToAction("Alert", "Home", new { message = $"Successfully Pay Bill for Table Number {table.TableNumber}!" });
        }


        public async Task<IActionResult> ViewAllPaidBills()
        {
            List<BillModel> bills = await _bill.GetAllPaidBills();
            List<BillDisplayModel> displayBills = new List<BillDisplayModel>();

            foreach (var bill in bills)
            {
                DiningTableModel table = await _table.GetDiningTableById(bill.DiningTableId);
                PersonModel attendant = await _person.GetPerson(bill.AttendantId);

                displayBills.Add(new BillDisplayModel { 
                    OrderId = bill.OrderId,
                    TableNumber = table.TableNumber,
                    Attendant = attendant.FirstName + " " + attendant.LastName,
                    SubTotal = bill.SubTotal,
                    Tax = bill.Tax,
                    Total = bill.Total,
                    BillPaid = bill.BillPaid
                });
            }

            return View(displayBills);
        }
    }
}