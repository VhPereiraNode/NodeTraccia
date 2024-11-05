
using Microsoft.AspNetCore.Mvc;
using NodeTraccia.Dtos;
using NodeTraccia.Services;

namespace NodeTraccia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        // Iniezione della dipendenza tramite il costruttore:
        // Il controller riceve un'istanza del servizio `ProductService` tramite dependency injection,
        // permettendo di utilizzare le funzionalità di `ProductService` senza doverne gestire manualmente l'istanza.
        // Questo approccio facilita il test del controller e il rispetto del principio di inversione delle dipendenze.
        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// Aggiunge un nuovo Prduct all'interno del context 
        /// </summary>
        /// <param name="product">Oggetto con i campi necessari per la creazione </param>
        /// <returns>IActionResult</returns>
        /// <response code="201">In caso la creazione sia fatta con successo</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddProduct(ProductDto product)
        {
            try
            {            
                var result = _productService.Create(product);
                return CreatedAtAction(nameof(GetProductById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Per estrarre un Product in base all'Id
        /// </summary>
        /// <param name="id">Parametro necessario per cercare il prodotto specifico</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso l'estrazione sia fatta con successo</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetProductById(int id)
        {
            try
            {
                var result = _productService.Read(id);
                if (result.Id == 0)
                {
                    return NotFound();
                }
                return Ok(result);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Per estrare una lista di prodotti in base al nome fornito, 
        /// altrimenti tutti in caso non ci sia un parametro 
        /// </summary>
        /// <param name="nome">Parametro per la ricerca</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso l'estrazione sia fatta con successo</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetListProduct(string? nome = null)
        {
            try
            {
                var result = _productService.Read(nome);
                if (result.Count == 0)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Modifica un Product con i campi forniti 
        /// </summary>
        /// <param name="id">Parametro per identificate il prodotto che si vuole modificare</param>
        /// <param name="product">Oggetto con i campi che si intendono modificare</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso la modifica sia stata fatta con successo</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateProduct(int id, ProductDto product)
        {
            try
            {
                if (_productService.Read(id).Id == 0)
                {
                    return NotFound();
                }
                return Ok(_productService.Update(id, product));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancella un prodotto in base all'Id
        /// </summary>
        /// <param name="id">Parametro per identificare quale prodotto si intende modificare</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso la cancellazione sia stata fatta con successo</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteProduct(int id) {

            try
            {
                if (_productService.Read(id).Id == 0)
                {
                    return NotFound();
                }
                return Ok(_productService.Delete(id));                

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
