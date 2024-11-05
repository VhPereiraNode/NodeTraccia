using Microsoft.AspNetCore.Mvc;
using NodeTraccia.Dtos;
using NodeTraccia.Services;

namespace NodeTraccia.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        // Iniezione della dipendenza tramite il costruttore:
        // Il controller riceve un'istanza del servizio `UserService` tramite dependency injection,
        // permettendo di utilizzare le funzionalità di `UserService` senza doverne gestire manualmente l'istanza.
        // Questo approccio facilita il test del controller e il rispetto del principio di inversione delle dipendenze.
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Aggiunge uno User all'interno del context
        /// </summary>
        /// <param name="userDto">Oggetto con i campi necessari per la creazione</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">In caso l'inserimento sia fatta con successo</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AddUser(UserDto userDto)
        {
            try
            {
                var result = _userService.Create(userDto);
                return CreatedAtAction(nameof(GetUserById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Prende i dati di uno User in base all'Id che viene fornito
        /// </summary>
        /// <param name="id">Id necessario per estrarre i dati</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso l'estrazione sia fatta con successo</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var result = _userService.Read(id);
                if (result.Id==0)
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
        /// Fornisce una lista di tutti gli User in caso non ci siano parametri, 
        /// altrimenti in base al parametrom di ricerca
        /// </summary>
        /// <param name="ricerca">Parametro necessario per estrarre i dati</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso l'estrazione sia fatta con successo</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetListUser(string? ricerca = null)
        {
            try
            {
                var result = _userService.Read(ricerca);
                if (result.Count==0)
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
        /// Modifica lo User con i campi forniti
        /// </summary>
        /// <param name="id"> L'id per indicare quale utente si voglia modificare</param>
        /// <param name="userDto">I campi dell'oggetto che si andrà a modificare</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso la modifica sia fatta con successo</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateUser(int id, UserDto userDto)
        {
            try
            {                
                if (_userService.Read(id).Id == 0)
                {
                    return NotFound();
                }
                var result = _userService.Update(id, userDto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                {
                    return BadRequest(ex.Message);
                }
            }
        }

        /// <summary>
        /// Cancella un user in base all'Id fornito
        /// </summary>
        /// <param name="id">Parametro per indicare quale User si intende cancellare</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">In caso la cancellazione sia fatta con successo</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {                
                if (_userService.Read(id).Id == 0)
                {
                    return NotFound();
                }                
                var result = _userService.Delete(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
