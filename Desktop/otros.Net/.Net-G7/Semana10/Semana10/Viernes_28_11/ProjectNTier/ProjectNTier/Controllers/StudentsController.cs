using BLL.LogicService;
using BOL.DataBaseEntities;
using Microsoft.AspNetCore.Mvc;

namespace ProjectNTier.Controllers
{
    [Route("Students")]
    public class StudentsController : Controller
    {
        private readonly IStudentLogic _studentLogic;
        public StudentsController(IStudentLogic studentLogic)
        {
            _studentLogic = studentLogic;
        }
        // GET: StudentsController
        [HttpGet("getStudents")]
        public ActionResult Index()
        {
            List<Student> listStudents = _studentLogic.GetStudentLogic();
            return View(listStudents);
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: StudentsController/Create
        [HttpGet("Create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentsController/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateStudent(Student student)
        {
            try
            {
                string result = "";
                result = _studentLogic.SaveStudentLogic(student);
                if(result == "Estudiante guardado con exito")
                    return RedirectToAction(nameof(Index));
                else
                    return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Edit/5
        [HttpGet("Edit")]
        public ActionResult Edit(int id)
        {
            var student = _studentLogic.GetStudentLogic().FirstOrDefault(x => x.Id == id);
            if (student != null)
                return View(student);
            return View();
        }

        // POST: StudentsController/Edit/5
        [HttpPost("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditStudent(int id, Student student)
        {
            try
            {
                string result = _studentLogic.UpdateStudentLogic(student);
                if( result == "Estudiante actualizado con exito")
                    return RedirectToAction(nameof(Index));
                else return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: StudentsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: StudentsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteStudent(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
