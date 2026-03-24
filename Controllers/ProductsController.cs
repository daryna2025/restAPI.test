using Microsoft.AspNetCore.Mvc;
using restAPI.Models;

namespace restAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        //------dependency injection tapa------
        private NorthwindOriginalContext db;

        public ProductsController(NorthwindOriginalContext dbparametri)
        {
            db = dbparametri;
        }

        //hakee kaikki tuotteet tietokannasta ja palauttaa ne JSON-muodossa
        [HttpGet]

        public ActionResult GetAllProducts()
        {
            try
            {
                var products = db.Products.ToList();
                return Json(products);
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

        //hakee tietokannasta tuotteen id:n perusteella ja palauttaa sen JSON-muodossa
        [HttpGet("{id}")]
        public ActionResult GetOneProductById(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    return Ok(product);
                }
                else
                {
                    return NotFound($"Tuoteet id:llä {id} ei löydy."); //string interpolation -tapa;
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }

            //lisää uusi tuote tietokantaan
            [HttpPost]

            public ActionResult AddNew([FromBody] Product prod)

            {
                try
                {
                    db.Products.Add(prod);
                    db.SaveChanges();
                    return Ok($"Lisättiin uusi tuote {prod.ProductName} and {prod.QuantityPerUnit}");
                }
                catch (Exception ex)
                {
                    return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
                }
            }
        }
    }

