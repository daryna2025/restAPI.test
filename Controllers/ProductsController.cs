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
        //hakee tuotet tietokannasta tuotteen nimen 
        [HttpGet("companyname/{cname}")]
        public ActionResult GetByName(string cname)
        {
            try
            {
                var prod = db.Products.Where(c => c.ProductName.Contains(cname));
                  return Ok(prod);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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
        //poistaa tuotteen id:n perusteella
        [HttpDelete("{id}")]

        public ActionResult Delete(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    return Ok($"Tuote id:llä {id} on poistettu.");
                }
                else
                {
                    return NotFound($"Tuote id:llä {id} ei löydy.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }

        }
        //tuote muokkaaminen ja JSON-muodossa lähetettävän tuote-olion esimerkki:
        //      {
        //"productId": 77,
        //"productName": "UUSI",
        //"supplierId": 12,
        //"categoryId": 2,
        //"quantityPerUnit": "string",
        //"unitPrice": 0,
        //"unitsInStock": 0,
        //"unitsOnOrder": 0,
        //"reorderLevel": 0,
        //"discontinued": true
        //}

        [HttpPut("{id}")]
        public ActionResult EditTuote(int id, [FromBody] Product tuote)
        {
            try
            {
                var product = db.Products.Find(id);
                if (product != null)
                {
                    product.ProductName = tuote.ProductName;
                    product.SupplierId = tuote.SupplierId;
                    product.CategoryId = tuote.CategoryId;
                    product.QuantityPerUnit = tuote.QuantityPerUnit;
                    product.UnitPrice = tuote.UnitPrice;
                    product.UnitsInStock = tuote.UnitsInStock;
                    product.UnitsOnOrder = tuote.UnitsOnOrder;
                    product.ReorderLevel = tuote.ReorderLevel;
                    product.Discontinued = tuote.Discontinued;
                    product.ImageLink = tuote.ImageLink;
                    db.SaveChanges();
                    return Ok($"Tuote id:llä {id} on päivitetty.");
                }
                else
                {
                    return NotFound($"Tuote id:llä {id} ei löydy.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Tapahtui virhe. Lue lisää: " + ex.InnerException);
            }
        }
    }
}


    

