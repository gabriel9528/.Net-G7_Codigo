using DapperMVCDemo.Data.Models.Domain;
using DapperMVCDemo.Data.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DapperMVCDemo.UI.Controllers
{
    public class PersonsController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<IActionResult> Index()
        {
            var listPerson = await _personRepository.GetAllPersonsAsync();
            if (listPerson != null)
            {
                return View(listPerson);
            }
            return NotFound();
        }

        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);

                bool addPersonResult = await _personRepository.AddPersonAsync(person);
                if (addPersonResult)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                return View(person);
            }
        }


        public async Task<IActionResult> Edit(int id)
        {
            Person person = null;
            try
            {
                person = await _personRepository.GetPersonByIdAsync(id);
                if (person != null)
                {
                    return View(person);
                }
                else
                {
                    return View(null);
                }

            }
            catch (Exception ex)
            {
                return View(null);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person person)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(person);

                bool updatePersonResult = await _personRepository.UpdatePersonAsync(person);
                if (updatePersonResult)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(person);
                }
            }
            catch (Exception ex)
            {
                return View(person);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                bool deleteResult = await _personRepository.DeletePersonAsync(id);
                if (deleteResult)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }

    }
}
