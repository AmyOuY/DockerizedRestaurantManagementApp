using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
    [Authorize(Roles = "Manager")]
    public class FoodController : Controller
    {
        private readonly IFoodData _data;

        public FoodController(IFoodData data)
        {
            _data = data;
        }


        public IActionResult Index()
        {
            return View();
        }


        // Insert new Food Type into database
        public async Task<IActionResult> InsertFoodType(FoodTypeDisplayModel type)
        {
            if (ModelState.IsValid)
            {
                var newFoodType = new FoodTypeModel { 
                    FoodType = type.FoodType
                };

                await _data.InsertFoodType(newFoodType);

                return RedirectToAction("ViewAllFoodTypes");
            }

            return View();
        }


        // View all Food Types
        public async Task<IActionResult> ViewAllFoodTypes()
        {
            List<FoodTypeModel> allFoodTypes = await _data.GetAllFoodTypes();
            var displayFoodTypes = new List<FoodTypeDisplayModel>();

            foreach (var foodType in allFoodTypes)
            {
                displayFoodTypes.Add(new FoodTypeDisplayModel {
                    Id = foodType.Id,
                    FoodType = foodType.FoodType
                });
            }

            return View(displayFoodTypes);
        }


        // Delete Food Type with Id = id
        public async Task<IActionResult> DeleteFoodType(int id)
        {
            await _data.DeleteFoodType(id);

            return RedirectToAction("ViewAllFoodTypes");
        }


        // Get Food Type Id by Food Type Name = type
        private async Task<int> GetTypeIdByTypeName(string type)
        {
            var result = await _data.GetTypeIdByTypeName(type);

            return result;
        }


        // Get Food Type Name by Food Type Id = id 
        private async Task<string> GetTypeNameByTypeId(int id)
        {
            var result = await _data.GetTypeNameByTypeId(id);

            return result;
        }


        // Insert new Food into database
        public async Task<IActionResult> InsertFood(FoodDisplayModel food)
        {
            if (ModelState.IsValid)
            {
                int typeId = await GetTypeIdByTypeName(food.FoodType);

                Console.WriteLine($"Type is {typeId}");

                if (typeId == 0) return View();

                var newFood = new FoodModel { 
                    TypeId = typeId,
                    FoodName = food.FoodName,
                    Price = food.Price
                };

                await _data.InsertFood(newFood);

                return RedirectToAction("ViewAllFoods");
            }

            return View();
        }


        // View info of all Foods
        public async Task<IActionResult> ViewAllFoods()
        {
            List<FoodModel> allFoods = await _data.GetAllFoods();
            var displayFoods = new List<FoodDisplayModel>();

            foreach (var food in allFoods)
            {
                string foodType = await GetTypeNameByTypeId(food.TypeId);

                displayFoods.Add(new FoodDisplayModel { 
                    Id = food.Id,
                    FoodType = foodType,
                    FoodName = food.FoodName,
                    Price = food.Price
                });
            }

            return View(displayFoods);
        }


        // Edit Food with Id = id
        public async Task<IActionResult> EditFood(int id)
        {
            FoodModel foundFood = await _data.GetFoodById(id);
            string foodType = await GetTypeNameByTypeId(foundFood.TypeId);

            var displayFood = new FoodDisplayModel { 
                Id = foundFood.Id,
                FoodType = foodType,
                FoodName = foundFood.FoodName,
                Price = foundFood.Price
            };

            return View(displayFood);
        }


        // Update Food info 
        public async Task<IActionResult> UpdateFood(FoodDisplayModel food)
        {
            int typeId = await GetTypeIdByTypeName(food.FoodType);

            if (typeId > 0 && ModelState.IsValid)
            {
                var updateFood = new FoodModel { 
                    Id = food.Id, 
                    TypeId = typeId,
                    FoodName = food.FoodName,
                    Price = food.Price
                };

                await _data.UpdateFood(updateFood);

                return RedirectToAction("ViewAllFoods");
            }

            return View();
        }


        // Delete Food with Id = id
        public async Task<IActionResult> DeleteFood(int id)
        {
            await _data.DeleteFood(id);

            return RedirectToAction("ViewAllFoods");
        }
    }
}