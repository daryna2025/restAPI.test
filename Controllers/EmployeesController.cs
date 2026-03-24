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

        //hakee employeen kaupungin perusteella
        [HttpGet("city/{cname}")]
        public ActionResult GetByCity(string cname)
        {
            try
            {
                var kaupunki = db.Employees.Where(c => c.City.Contains(cname));
                return Ok(kaupunki);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        //hakee työntekijän id:n perusteella
        [HttpGet("{id}")]
        public ActionResult GetOneEmployeeById(int id)
        {
            try
            {
                var työnt = db.Employees.Find(id);
                if (työnt != null)
                {
                    return Ok(työnt);
                }
                else
                {
                    return NotFound($"Työntekijän id:llä {id} ei löydy.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }



    }
}
