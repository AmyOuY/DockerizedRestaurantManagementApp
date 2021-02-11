using RMDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public interface IFoodData
    {
        Task DeleteFood(int id);
        Task DeleteFoodType(int id);
        Task<List<FoodModel>> GetAllFoods();
        Task<List<FoodTypeModel>> GetAllFoodTypes();
        Task<FoodModel> GetFoodById(int id);
        Task<List<FoodModel>> GetFoodsByTypeId(int typeId);
        Task<FoodModel> GetFoodByName(string foodName);
        Task<int> GetTypeIdByTypeName(string type);
        Task<string> GetTypeNameByTypeId(int id);
        Task InsertFood(FoodModel food);
        Task InsertFoodType(FoodTypeModel foodType);
        Task UpdateFood(FoodModel food);
    }
}