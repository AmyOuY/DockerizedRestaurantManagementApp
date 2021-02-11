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


        public async Task InsertFoodType(FoodTypeModel foodType)
        {
            await _sql.SaveData("spFoodType_Insert", foodType);
        }


        public async Task<List<FoodTypeModel>> GetAllFoodTypes()
        {
            var results = await _sql.LoadData<FoodTypeModel, dynamic>("spFoodType_GetAll", new { });

            return results;
        }


        public async Task DeleteFoodType(int id)
        {
            await _sql.DeleteData("spFoodType_Delete", new { id });
        }


        public async Task<int> GetTypeIdByTypeName(string type)
        {
            var results = await _sql.LoadData<int, dynamic>("spFoodType_GetIdByName", new { type });

            return results.FirstOrDefault();
        }


        public async Task<string> GetTypeNameByTypeId(int id)
        {
            var results = await _sql.LoadData<string, dynamic>("spFoodType_GetNameById", new { id });

            return results.FirstOrDefault();
        }


        public async Task InsertFood(FoodModel food)
        {
            await _sql.SaveData("spFood_Insert", food);
        }


        public async Task<List<FoodModel>> GetAllFoods()
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetAll", new { });

            return results;
        }


        public async Task<FoodModel> GetFoodById(int id)
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetById", new { id });

            return results.FirstOrDefault();
        }


        public async Task<FoodModel> GetFoodByName(string foodName)
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetByName", new { foodName });

            return results.FirstOrDefault();
        }


        public async Task<List<FoodModel>> GetFoodsByTypeId(int typeId)
        {
            var results = await _sql.LoadData<FoodModel, dynamic>("spFood_GetByTypeId", new { typeId });

            return results;
        }


        public async Task UpdateFood(FoodModel food)
        {
            await _sql.SaveData("spFood_Update", food);
        }


        public async Task DeleteFood(int id)
        {
            await _sql.DeleteData("spFood_Delete", new { id });
        }
    }
}
