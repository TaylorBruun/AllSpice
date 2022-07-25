using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{

    [ApiController]
    [Route("api/[controller]")]

    public class StepsController : ControllerBase
    {
        private readonly StepsService _stepsService;

        public StepsController(StepsService stepsService)
        {
            _stepsService = stepsService;
        }

        [HttpGet("{id}")]
        public ActionResult<Step> Get(int id)
        {
            try
            {
                Step step = _stepsService.GetById(id);
                return Ok(step);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Step>> CreateAsync([FromBody] Step stepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Step newStep = _stepsService.Create(stepData);
                return Ok(newStep);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Step>> EditAsync(int id, [FromBody] Step updatedStepData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updatedStepData.Id = id;
                Step updatedStep = _stepsService.Update(updatedStepData);
                return Ok(updatedStep);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Step> Delete(int id)
        {
            try
            {
                Step step = _stepsService.GetById(id);
                _stepsService.Delete(id);
                return Ok(step);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }



    }
}