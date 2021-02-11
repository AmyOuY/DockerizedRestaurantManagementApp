using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
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


        public async Task<IActionResult> InsertDiningTable(DiningTableDisplayModel table)
        {
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


        public async Task<IActionResult> DeleteDiningTable(int id)
        {
            await _data.DeleteDiningTable(id);

            return RedirectToAction("ViewAllDiningTables");
        }
    }
}