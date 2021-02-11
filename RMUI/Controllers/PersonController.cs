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
    public class PersonController : Controller
    {
        private readonly IPersonData _data;

        public PersonController(IPersonData data)
        {
            _data = data;
        }


        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> InsertPerson(PersonDisplayModel person)
        {
            if (ModelState.IsValid)
            {
                PersonModel newPerson = new PersonModel { 
                    EmployeeID = person.EmployeeID,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Phone = person.Phone
                };

                await _data.InsertPerson(newPerson);

                return RedirectToAction("ViewAllPersons");
            }

            return View();
        }


        public async Task<IActionResult> ViewAllPersons()
        {
            var displayPersons = new List<PersonDisplayModel>();
            List<PersonModel> allPersons = await _data.GetAllPersons();

            foreach (var person in allPersons)
            {
                displayPersons.Add(new PersonDisplayModel { 
                    Id = person.Id,
                    EmployeeID = person.EmployeeID,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Phone = person.Phone
                });
            }

            return View(displayPersons);
        }


        public async Task<IActionResult> EditPerson(int id)
        {
            PersonModel foundPerson = await _data.GetPerson(id);

            var person = new PersonDisplayModel { 
                Id = foundPerson.Id,
                EmployeeID = foundPerson.EmployeeID,
                FirstName = foundPerson.FirstName,
                LastName = foundPerson.LastName,
                Email = foundPerson.Email,
                Phone = foundPerson.Phone
            };

            return View(person);

        }


        public async Task<IActionResult> UpdatePerson(PersonDisplayModel person)
        {
            if (ModelState.IsValid)
            {
                var updatePerson = new PersonModel { 
                    Id = person.Id,
                    EmployeeID = person.EmployeeID,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Email = person.Email,
                    Phone = person.Phone
                };

                await _data.UpdatePerson(updatePerson);

                return RedirectToAction("ViewAllPersons");
            }

            return View();
        }


        public async Task<IActionResult> DeletePerson(int id)
        {
            await _data.DeletePerson(id);

            return RedirectToAction("ViewAllPersons");
        }
    }
}