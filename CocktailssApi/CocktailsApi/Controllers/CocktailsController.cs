using CocktailsApi.Clients;
using CocktailsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CocktailsApi.Models.FilterCocktails;

namespace CocktailsApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CocktailsController : ControllerBase
    {
        private readonly ILogger<CocktailsController> _logger;
        private readonly CocktailsClient _cocktailsClient;
        private FavoritesContext _favoritesContext;

        public CocktailsController(ILogger<CocktailsController> logger, CocktailsClient cocktailsClient, FavoritesContext favoritesContext)
        {
            _logger = logger;
            _cocktailsClient = cocktailsClient;
            _favoritesContext = favoritesContext;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("search")]

        public async Task<Cocktails> GetCocktailByName([FromQuery] string name)
        {
            //string name = "Snowday";

            var cocktail = await _cocktailsClient.GetCocktailByName(name);

            return cocktail;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("filter")]

        public async Task<FilterCocktails> GetCocktailsByIngridients([FromQuery] string str)
        {
            var ingridients_names = str.Split(',');

            Stack<FilterCocktails> cocktails = new Stack<FilterCocktails>();

            foreach (string ingridient in ingridients_names)
            {
                var temp = await _cocktailsClient.GetCocktailsByIngridient(ingridient.Trim());
                cocktails.Push(temp);
            }

            for (int i = 1; i < ingridients_names.Length; i = i + 1)
            {
                var temp1 = cocktails.Pop();
                var temp2 = cocktails.Pop();

                if (temp1 == null || temp2 == null)
                    return null;

                List<Drink> result = new List<Drink>();

                foreach (var i1 in temp1.drinks)
                {
                    foreach (var i2 in temp2.drinks)
                    {
                        if (i1.idDrink == i2.idDrink)
                        {
                            if (!result.Contains(i1))
                                result.Add(i1);
                            break;
                        }
                    }
                }

                cocktails.Push(new FilterCocktails { drinks = result.ToArray() });
            }

            return cocktails.Pop();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("random")]

        public async Task<Cocktails> GetRandomCocktail()
        {
            var cocktail = await _cocktailsClient.GetRandomCocktail();

            return cocktail;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpGet("getfavorites")]

        public IEnumerable<Favorite> Get([FromQuery] int user)
        {
            return _favoritesContext.Favorites.Where(u => u.UserId == user);
        }
       
        //////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpPost("addtofavorite")]
        public async Task AddToFavorite([FromBody] Favorite fav)
        {
            await _favoritesContext.Favorites.AddAsync(fav);
            await _favoritesContext.SaveChangesAsync();
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////

        [HttpDelete("deletefromfavorites")]
        public async Task Delete([FromQuery] Favorite favorite)
        {
            var fav = _favoritesContext.Favorites.FirstOrDefault(u => u.UserId == favorite.UserId && u.CocktailName == favorite.CocktailName);
            if (fav != null)
            {
                _favoritesContext.Remove(fav);
                await _favoritesContext.SaveChangesAsync();
            }
        }


    }
}
