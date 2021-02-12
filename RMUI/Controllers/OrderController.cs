﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderData _order;
        private readonly IFoodData _food;
        private readonly IDiningTableData _diningTable;
        private readonly IPersonData _person;

        public OrderController(IOrderData order, IFoodData food, IDiningTableData diningTable, IPersonData person)
        {
            _order = order;
            _food = food;
            _diningTable = diningTable;
            _person = person;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<List<SelectListItem>> GetAllDiningTables()
        {
            var allDiningTables = await _diningTable.GetAllDiningTables();
            var tableList = new List<SelectListItem>();
 
            foreach (var table in allDiningTables)
            {
                tableList.Add(new SelectListItem { 
                    Text = table.TableNumber.ToString(),
                    Value = table.Id.ToString()
                });
            }

            return tableList;
        }


        public async Task<List<SelectListItem>> GetAllAttendants()
        {
            var allPersons = await _person.GetAllPersons();
            var attendantList = new List<SelectListItem>();

            foreach (var person in allPersons)
            {
                attendantList.Add(new SelectListItem { 
                    Text = person.FirstName + " " + person.LastName,
                    Value = person.Id.ToString()
                });
            }

            return attendantList;
        }


        public async Task<List<SelectListItem>> GetAllFoodTypes()
        {
            var allFoodTypes = await _food.GetAllFoodTypes();
            var foodTypeList = new List<SelectListItem>();

            foreach (var foodType in allFoodTypes)
            {
                foodTypeList.Add(new SelectListItem { 
                    Text = foodType.FoodType,
                    Value = foodType.Id.ToString()
                });
            }

            return foodTypeList;
        }


        public async Task<JsonResult> GetFoodsByTypeId(int typeId)
        {
            var foods = await _food.GetFoodsByTypeId(typeId);
            var list = new List<SelectListItem>();

            foreach (var food in foods)
            {
                list.Add(new SelectListItem { 
                    Text = food.FoodName,
                    Value = food.Id.ToString()
                });
            }

            return Json(list);
        }


        public async Task<JsonResult> GetFoodById(int id)
        {
            var food = await _food.GetFoodById(id);

            return Json(food);
        }


        public async Task<IActionResult> CreateOrder()
        {
            var tables = await GetAllDiningTables();
            ViewBag.TableList = tables;

            var attendants = await GetAllAttendants();
            ViewBag.AttendantList = attendants;

            var foodTypes = await GetAllFoodTypes();
            ViewBag.FoodTypeList = foodTypes;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDetailFillInModel orderDetail)
        {
            if (ModelState.IsValid)
            {
                OrderDetailModel detail = new OrderDetailModel { 
                    DiningTableId = orderDetail.DiningTableId,
                    AttendantId = orderDetail.AttendantId,
                    FoodId = orderDetail.FoodId,
                    Quantity = orderDetail.Quantity
                };

                await _order.InsertOrderDetail(detail);
                
            }
            await CreateOrder();

            return View();
        }


        public async Task<IActionResult> ViewOrderDetailsByTableNumber(int tableNumber)
        {
            if (await _diningTable.IsValidTableNumber(tableNumber) == false)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! Table Number {tableNumber} does not exist!" });
            }

            DiningTableModel table = await _diningTable.GetDiningTableByTableNumber(tableNumber);

            List<OrderDetailModel> orderDetails = await _order.GetOrderDetailsByDiningTableIdUnpaid(table.Id);

            if (orderDetails.Count == 0)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! There is no currently active order for Table {tableNumber}!" });
            }
            else
            {
                List<OrderDetailDisplayModel> displayDetails = new List<OrderDetailDisplayModel>();
                var attendant = await _person.GetPerson(orderDetails[0].AttendantId);

                foreach (var detail in orderDetails)
                {
                    FoodModel food = await _food.GetFoodById(detail.FoodId);

                    displayDetails.Add(new OrderDetailDisplayModel { 
                        Id = detail.Id,
                        TableNumber = tableNumber,
                        Attendant = attendant.FirstName + " " + attendant.LastName,
                        FoodName = food.FoodName,
                        Price = food.Price,
                        Quantity = detail.Quantity,
                        OrderDate = detail.OrderDate        
                    });
                }
                return View(displayDetails);
            }
        }


        public IActionResult SearchOrderByTable()
        {
            return View();
        }


        public async Task<IActionResult> InsertOrderByTable(int tableNumber)
        {
            if (await _diningTable.IsValidTableNumber(tableNumber) == false)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! Table Number {tableNumber} does not exist!" });
            }

            List<OrderModel> allOrders = await _order.GetAllUnpaidOrders();
            DiningTableModel table = await _diningTable.GetDiningTableByTableNumber(tableNumber);

            foreach (var order in allOrders)
            {
                if (order.DiningTableId == table.Id)
                {
                    await _order.DeleteOrder(order.Id);
                }
            }

            await _order.InsertOrderByTableId(table.Id);

            return RedirectToAction("Alert", "Home", new { message = $"Successfully Submit Order for Table Number {tableNumber}!" });
        }


        public async Task<IActionResult> ViewAllUnpaidOrders()
        {
            List<OrderModel> allOrders = await _order.GetAllUnpaidOrders();
            List<OrderDisplayModel> displayOrders = new List<OrderDisplayModel>();

            foreach (var order in allOrders)
            {
                DiningTableModel table = await _diningTable.GetDiningTableById(order.DiningTableId);
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


        public async Task<IActionResult> ViewOrderDetail(int id)
        {
            OrderDetailModel detail = await _order.GetOrderDetailById(id);
            DiningTableModel table = await _diningTable.GetDiningTableById(detail.DiningTableId);
            PersonModel attendant = await _person.GetPerson(detail.AttendantId);
            FoodModel food = await _food.GetFoodById(detail.FoodId);

            OrderDetailDisplayModel displayDetail = new OrderDetailDisplayModel { 
                Id = id,
                TableNumber = table.TableNumber,
                Attendant = attendant.FirstName + " " + attendant.LastName,
                FoodName = food.FoodName,
                Price = food.Price,
                Quantity = detail.Quantity,
                OrderDate = detail.OrderDate
            };

            return View(displayDetail);
        }


        public async Task<IActionResult> EditOrderDetail(int id)
        {
            OrderDetailModel detail = await _order.GetOrderDetailById(id);
            DiningTableModel table = await _diningTable.GetDiningTableById(detail.DiningTableId);
            PersonModel attendant = await _person.GetPerson(detail.AttendantId);
            FoodModel food = await _food.GetFoodById(detail.FoodId);

            OrderDetailDisplayModel displayDetail = new OrderDetailDisplayModel
            {
                Id = id,
                TableNumber = table.TableNumber,
                Attendant = attendant.FirstName + " " + attendant.LastName,
                FoodName = food.FoodName,
                Price = food.Price,
                Quantity = detail.Quantity,
                OrderDate = detail.OrderDate
            };

            return View(displayDetail);
        }


        public async Task<IActionResult> UpdateOrderDetail(OrderDetailDisplayModel displayDetail)
        {
            DiningTableModel table = await _diningTable.GetDiningTableByTableNumber(displayDetail.TableNumber);
            string[] fullName = displayDetail.Attendant.Split(" ");
            PersonModel attendant = await _person.GetPersonByFullName(fullName[0], fullName[1]);
            FoodModel food = await _food.GetFoodByName(displayDetail.FoodName);

            OrderDetailModel orderDetail = new OrderDetailModel { 
                Id = displayDetail.Id,
                DiningTableId = table.Id,
                AttendantId = attendant.Id,
                FoodId = food.Id,
                Quantity = displayDetail.Quantity,
                OrderDate = displayDetail.OrderDate
            };

            await _order.UpdateOrderDetail(orderDetail);

            return RedirectToAction("ViewOrderDetailsByTableNumber", new { table.TableNumber });
        }


        public async Task<IActionResult> EditOrder(int id)
        {
            OrderModel order = await _order.GetOrderById(id);
            DiningTableModel table = await _diningTable.GetDiningTableById(order.DiningTableId);
            PersonModel attendant = await _person.GetPerson(order.AttendantId);

            OrderDisplayModel displayOrder = new OrderDisplayModel { 
                Id = order.Id,
                TableNumber = table.TableNumber,
                Attendant = attendant.FirstName + " " + attendant.LastName,
                SubTotal = order.SubTotal,
                Tax = order.Tax,
                Total = order.Total,
                CreatedDate = order.CreatedDate,
                BillPaid = order.BillPaid
            };

            return View(displayOrder);
        }


        public async Task<IActionResult> UpdateOrder(OrderDisplayModel displayOrder)
        {
            DiningTableModel table = await _diningTable.GetDiningTableByTableNumber(displayOrder.TableNumber);
            string[] fullName = displayOrder.Attendant.Split(" ");
            PersonModel attendant = await _person.GetPersonByFullName(fullName[0], fullName[1]);

            OrderModel order = new OrderModel { 
                Id = displayOrder.Id,
                DiningTableId = table.Id,
                AttendantId = attendant.Id,
                SubTotal = displayOrder.SubTotal,
                Tax = displayOrder.Tax,
                Total = displayOrder.Total,
                CreatedDate = displayOrder.CreatedDate,
                BillPaid = displayOrder.BillPaid
            };

            await _order.UpdateOrder(order);

            return RedirectToAction("ViewAllUnpaidOrders");
        }


        public async Task<IActionResult> ViewOrderDetailsByOrderId(int id)
        {
            OrderModel order = await _order.GetOrderById(id);
            DiningTableModel table = await _diningTable.GetDiningTableById(order.DiningTableId);
            List<OrderDetailModel> orderDetails = await _order.GetOrderDetailsByDiningTableIdUnpaid(table.Id);
            
            if (orderDetails.Count == 0)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! There is no currently active order for Table {table.TableNumber}!" });
            }
            else
            {
                List<OrderDetailDisplayModel> displayDetails = new List<OrderDetailDisplayModel>();
                PersonModel attendant = await _person.GetPerson(orderDetails[0].AttendantId);

                foreach (var detail in orderDetails)
                {
                    FoodModel food = await _food.GetFoodById(detail.FoodId);

                    displayDetails.Add(new OrderDetailDisplayModel { 
                        Id = detail.Id,
                        TableNumber = table.Id,
                        Attendant = attendant.FirstName + " " + attendant.LastName,
                        FoodName = food.FoodName,
                        Price = food.Price,
                        Quantity = detail.Quantity,
                        OrderDate = detail.OrderDate
                    });
                }

                return View(displayDetails);
            }
        }


        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            OrderDetailModel orderDetail = await _order.GetOrderDetailById(id);
            DiningTableModel table = await _diningTable.GetDiningTableById(orderDetail.DiningTableId);

            await _order.DeleteOrderDetail(id);

            return RedirectToAction("ViewOrderDetailsByTableNumber", new { table.TableNumber });
        }


        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _order.DeleteOrder(id);

            return RedirectToAction("ViewAllUnpaidOrders");
        }
    }
}