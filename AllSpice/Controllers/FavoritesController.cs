using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly FavoritesService _favoritesService;

        public FavoritesController(FavoritesService favoritesService)
        {
            _favoritesService = favoritesService;
        }

        [HttpPost]
        public async Task<ActionResult<Favorite>> Create([FromBody] Favorite favoriteData)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                favoriteData.AccountId = userInfo.Id;
                Favorite newFavorite = _favoritesService.Create(favoriteData);
                return Ok(newFavorite);
            }
            catch (System.Exception exception)
            {

                return BadRequest(exception.Message);
            }
        }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Favorite>> Delete(int id)
    {
        try
        {
            Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
            _favoritesService.Delete(id, userInfo.Id);
            return Ok("Delete Successful");
        }
        catch (System.Exception exception)
        {
            
            return BadRequest(exception.Message);
        }
    }
    }
}