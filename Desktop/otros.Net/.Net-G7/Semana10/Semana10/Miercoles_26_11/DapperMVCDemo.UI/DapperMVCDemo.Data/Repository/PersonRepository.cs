using DapperMVCDemo.Data.DataAccess;
using DapperMVCDemo.Data.Models.Domain;

namespace DapperMVCDemo.Data.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ISqlDataAccess _db;
        public PersonRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<bool> AddPersonAsync(Person person)
        {
            try
            {
                await _db.SaveDataAsync("sp_insert_person",
                new { person.Name, person.Email, person.Address });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePersonAsync(int id)
        {
            try
            {
                await _db.SaveDataAsync("sp_delete_person", new { Id = id });

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Person>> GetAllPersonsAsync()
        {
            try
            {
                IEnumerable<Person> listPerson =
                    await _db.GetDataAsync<Person, dynamic>("sp_get_all_persons", new { });

                return listPerson;
            }
            catch (Exception ex)
            {
                return Enumerable.Empty<Person>();
            }
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            try
            {
                IEnumerable<Person> result = await _db.GetDataAsync<Person, dynamic>
                ("sp_get_person_by_id", new { Id = id });

                return result.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> UpdatePersonAsync(Person person)
        {
            try
            {
                await _db.SaveDataAsync("sp_update_person", person);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
