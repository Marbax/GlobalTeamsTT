using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SecretSanta.BLL;
using SecretSanta.Domain.Models;
using SecretSanta.Domain.Repos.Abstract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecretSanta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecretSantaController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IRepository<User> _users; // TODO: just for REST, move into uow later
        private readonly ILogger<SecretSantaController> _logger;

        public SecretSantaController(IUnitOfWork uow, IRepository<User> users, ILogger<SecretSantaController> logger)
        {
            _uow = uow;
            _users = users;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            try
            {
                var data = await _users.Get();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetSantas")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<SecretSantaPair>>> GetSantas()
        {
            try
            {
                var data = await _uow.GetSecretSantas();
                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpGet("GetById/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> GetById(int id)
        {
            try
            {
                var data = await _users.Get(id);
                return Ok(data);
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<User>> Add(User item)
        {
            try
            {
                var addedUser = await _uow.AddUser(item);
                return StatusCode(201, addedUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(User item)
        {
            try
            {
                await _users.Update(item);
                return Ok();
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _users.Delete(id);
                return Ok();
            }
            catch (IndexOutOfRangeException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }
    }
}
