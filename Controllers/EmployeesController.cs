using Microsoft.AspNetCore.Mvc;
using restAPI.Models;

namespace restAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Controller
    {
        //------dependency injection tapa------
        private NorthwindOriginalContext db;

        public EmployeesController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

        //hakee kaikki työntekijät tietokannasta ja palauttaa ne JSON-muodossa
        [HttpGet]

        public ActionResult GetAllEmployees()
        {
            try
            {
                var työntekijä = db.Employees.ToList();
                return Json(työntekijä);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

    }
}
