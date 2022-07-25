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

    public class IngredientsController : ControllerBase
    {
        private readonly IngredientsService _ingredientsService;

        public IngredientsController(IngredientsService ingredientsService)
        {
            _ingredientsService = ingredientsService;
        }

        [HttpGet("{id}")]
        public ActionResult<Ingredient> Get(int id)
        {
            try
            {
                Ingredient ingredient = _ingredientsService.GetById(id);
                return Ok(ingredient);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Ingredient>> CreateAsync([FromBody] Ingredient ingredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Ingredient newIngredient = _ingredientsService.Create(ingredientData);
                return Ok(newIngredient);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Ingredient>> EditAsync(int id, [FromBody] Ingredient updatedIngredientData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updatedIngredientData.Id = id;
                Ingredient updatedIngredient = _ingredientsService.Update(updatedIngredientData);
                return Ok(updatedIngredient);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Ingredient> Delete(int id)
        {
            try
            {
                Ingredient ingredient = _ingredientsService.GetById(id);
                _ingredientsService.Delete(id);
                return Ok(ingredient);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }



    }
}