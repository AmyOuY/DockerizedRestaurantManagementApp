using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMDataLibrary.DataAccess;
using RMDataLibrary.Models;
using RMUI.Models;

namespace RMUI.Controllers
{
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


        public async Task<IActionResult> DeleteFoodType(int id)
        {
            await _data.DeleteFoodType(id);

            return RedirectToAction("ViewAllFoodTypes");
        }


        private async Task<int> GetTypeIdByTypeName(string type)
        {
            var result = await _data.GetTypeIdByTypeName(type);

            return result;
        }


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


        private async Task<string> GetTypeNameByTypeId(int id)
        {
            var result = await _data.GetTypeNameByTypeId(id);

            return result;
        }


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


        public async Task<IActionResult> DeleteFood(int id)
        {
            await _data.DeleteFood(id);

            return RedirectToAction("ViewAllFoods");
        }
    }
}