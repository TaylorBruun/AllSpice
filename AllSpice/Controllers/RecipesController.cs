using System.Collections.Generic;
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

    public class RecipesController : ControllerBase
    {
        private readonly RecipesService _recipesService;
        private readonly IngredientsService _ingredientsService;
        private readonly StepsService _stepsService;

        public RecipesController(RecipesService recipesService, IngredientsService ingredientsService, StepsService stepsService)
        {
            _recipesService = recipesService;
            _ingredientsService = ingredientsService;
            _stepsService = stepsService;
        }

        [HttpGet]
        public ActionResult<List<Recipe>> Get()
        {
            try
            {
                List<Recipe> recipes = _recipesService.GetAll();
                return Ok(recipes);
            }
            catch (System.Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }
        [HttpGet("{id}")]
        public ActionResult<Recipe> Get(int id)
        {
            try
            {
                Recipe recipe = _recipesService.GetById(id);
                return Ok(recipe);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Recipe>> CreateAsync([FromBody] Recipe recipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                recipeData.CreatorId = userInfo.Id;
                Recipe newRecipe = _recipesService.Create(recipeData);
                newRecipe.CreatorId = userInfo.Id;
                return Ok(newRecipe);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> EditAsync(int id,[FromBody] Recipe updatedRecipeData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                updatedRecipeData.Id = id;
                updatedRecipeData.CreatorId = userInfo.Id;
                Recipe updatedRecipe = _recipesService.Update(updatedRecipeData);
                return Ok(updatedRecipe);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }


        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Recipe>> Delete(int id)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                string userId = userInfo.Id;
                Recipe recipe = _recipesService.GetById(id);
                _recipesService.Delete(id, userId);
                return Ok(recipe);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

[HttpGet("{id}/ingredients")]
    public ActionResult<List<Ingredient>> GetIngredientsByRecipe(int id)
        {
            try
            {
                List<Ingredient> ingredients = _ingredientsService.GetIngredientsByRecipe(id);
                return Ok(ingredients);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
[HttpGet("{id}/steps")]
    public ActionResult<List<Step>> GetStepsByRecipe(int id)
        {
            try
            {
                List<Step> steps = _stepsService.GetStepsByRecipe(id);
                return Ok(steps);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

     
    }
}