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
        //employee muokaminen id:n perusteella
        [HttpPut("{id}")]
        public ActionResult EditEmployee(int id, [FromBody] Employee tekijä)
        {
            try
            {
                var employee = db.Employees.Find(id);
                if (employee != null)
                {
                    employee.LastName = tekijä.LastName;
                    employee.FirstName = tekijä.FirstName;
                    employee.Title = tekijä.Title;
                    employee.TitleOfCourtesy = tekijä.TitleOfCourtesy;
                    employee.BirthDate = tekijä.BirthDate;
                    employee.HireDate = tekijä.HireDate;
                    employee.Address = tekijä.Address;
                    employee.City = tekijä.City;
                    employee.Region = tekijä.Region;
                    employee.PostalCode = tekijä.PostalCode;
                    employee.Country = tekijä.Country;
                    employee.HomePhone = tekijä.HomePhone;
                    employee.Extension = tekijä.Extension;
                    employee.Photo = tekijä.Photo;
                    employee.Notes = tekijä.Notes;
                    employee.ReportsTo = tekijä.ReportsTo;
                    employee.PhotoPath = tekijä.PhotoPath;

                    db.SaveChanges();
                    return Ok($"Työntekijän id:llä {id} on päivitetty.");
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
