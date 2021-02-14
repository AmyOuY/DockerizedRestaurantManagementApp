using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Manager")]
    public class DiningTableController : Controller
    {
        private readonly IDiningTableData _data;

        public DiningTableController(IDiningTableData data)
        {
            _data = data;
        }


        public IActionResult Index()
        {
            return View();
        }


        // Insert new Dining Table into database
        public async Task<IActionResult> InsertDiningTable(DiningTableDisplayModel table)
        {
            if (await _data.IsValidTableNumber(table.TableNumber) == true)
            {
                return RedirectToAction("Alert", "Home", new { message = $"Error! Table Number {table.TableNumber} already exists!" });
            }

            if (ModelState.IsValid)
            {
                DiningTableModel newTable = new DiningTableModel {
                    TableNumber = table.TableNumber,
                    Seats = table.Seats
                };

                await _data.InsertDiningTable(newTable);

                return RedirectToAction("ViewAllDiningTables");
            }

            return View();
        }


        // View all Dining Tables as a list
        public async Task<IActionResult> ViewAllDiningTables()
        {
            var displayTables = new List<DiningTableDisplayModel>();
            List<DiningTableModel> allTables = await _data.GetAllDiningTables();

            foreach (var table in allTables)
            {
                displayTables.Add(new DiningTableDisplayModel { 
                    Id = table.Id,
                    TableNumber = table.TableNumber,
                    Seats = table.Seats
                });
            }

            return View(displayTables);
        }


        // Edit Dining Table with Id = id
        public async Task<IActionResult> EditDiningTable(int id)
        {
            DiningTableModel foundTable = await _data.GetDiningTableById(id);

            var displayTable = new DiningTableDisplayModel { 
                Id = foundTable.Id,
                TableNumber = foundTable.TableNumber,
                Seats = foundTable.Seats
            };

            return View(displayTable);
        }


        // Update Dining Table info
        public async Task<IActionResult> UpdateDiningTable(DiningTableDisplayModel table)
        {          
            if (ModelState.IsValid)
            {
                var updateTable = new DiningTableModel { 
                    Id = table.Id,
                    TableNumber = table.TableNumber,
                    Seats = table.Seats
                };

                await _data.UpdateDiningTable(updateTable);

                return RedirectToAction("ViewAllDiningTables");
            }

            return View();
        }


        // Delete Dining Table with Id = id
        public async Task<IActionResult> DeleteDiningTable(int id)
        {
            await _data.DeleteDiningTable(id);

            return RedirectToAction("ViewAllDiningTables");
        }
    }
}