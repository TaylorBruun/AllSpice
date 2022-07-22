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

        public RecipesController(RecipesService recipesService)
        {
            _recipesService = recipesService;
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

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult<Recipe> Delete(int id)
        {
            try
            {
                Recipe recipe = _recipesService.GetById(id);
                _recipesService.Delete(id);
                return Ok(recipe);
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}