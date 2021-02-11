using RMDataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMDataLibrary.DataAccess
{
    public class PersonData : IPersonData
    {
        private readonly ISqlDataAccess _sql;

        public PersonData(ISqlDataAccess sql)
        {
            _sql = sql;
        }


        public async Task InsertPerson(PersonModel person)
        {
            await _sql.SaveData("spPerson_Insert", person);
        }


        public async Task<List<PersonModel>> GetAllPersons()
        {
            var results = await _sql.LoadData<PersonModel, dynamic>("spPerson_GetAll", new { });

            return results;
        }


        public async Task<PersonModel> GetPerson(int id)
        {
            var results = await _sql.LoadData<PersonModel, dynamic>("spPerson_Get", new { id });

            return results.FirstOrDefault();
        }


        public async Task<PersonModel> GetPersonByFullName(string firstName, string lastName)
        {
            var results = await _sql.LoadData<PersonModel, dynamic>("spPerson_GetByFullName", new { firstName, lastName });

            return results.FirstOrDefault();
        }


        public async Task UpdatePerson(PersonModel person)
        {
            await _sql.SaveData("spPerson_Update", person);
        }


        public async Task DeletePerson(int id)
        {
            await _sql.DeleteData("spPerson_Delete", new { id });
        }
    }
}
