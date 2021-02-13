using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class FoodData : IFoodData
    {
        private readonly ISqlDataAccess _sql;

        public FoodData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        // Insert new food type into database
        public async Task InsertFoodType(FoodTypeModel foodType)
        {
            await _sql.SaveData("spFoodType_Insert", foodType);
        }


        // Get all Food Types from database
        public async Task<List<FoodTypeModel>> GetAllFoodTypes()
        {
            var results = await _sql.LoadData<FoodTypeModel, dynamic>("spFoodType_GetAll", new { });

            return results;
        }


        // Delete specific food type with Id = id
        public async Task DeleteFoodType(int id)
        {
            await _sql.DeleteData("spFoodType_Delete", new { id });
        }


        // Get food type Id with food type name = type
        public async Task<int> GetTypeIdByTypeName(string type)
        {
            var results = await _sql.LoadData<int, dynamic>("spFoodType_GetIdByName", new { type });

            return results.FirstOrDefault();
        }


        // Get food type name with food type Id = id
        public async Task<string> GetTypeNameByTypeId(int id)
        {
            var results = await _sql.LoadData<string, dynamic>("spFoodType_GetNameById", new { id });

            return results.FirstOrDefault();
        }

        
        // Insert new food into database
        public async Task InsertFood(FoodModel food)
        {
            await _sql.SaveData("spFood_Insert", food);
        }


        // Get info of all foods from the database
        public async Task<List<FoodModel>> GetAllFoods()
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetAll", new { });

            return results;
        }


        // Get specific food info with Id = id
        public async Task<FoodModel> GetFoodById(int id)
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetById", new { id });

            return results.FirstOrDefault();
        }


        // Get specific food info with FoodName = foodName
        public async Task<FoodModel> GetFoodByName(string foodName)
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetByName", new { foodName });

            return results.FirstOrDefault();
        }


        // Get all food belong to a specific Food Type with TypeId = typeId
        public async Task<List<FoodModel>> GetFoodsByTypeId(int typeId)
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetByTypeId", new { typeId });

            return results;
        }


        // Update food info in the database
        public async Task UpdateFood(FoodModel food)
        {
            await _sql.SaveData("spFood_Update", food);
        }


        // Delete specific food from database with Id = id
        public async Task DeleteFood(int id)
        {
            await _sql.DeleteData("spFood_Delete", new { id });
        }
    }
}
