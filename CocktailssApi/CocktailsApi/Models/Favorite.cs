//using CocktailsApi.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace CocktailsApi.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CocktailName { get; set; }
        /*public async Task DbAdd()
        {
            using (var context = new MyDbContext())
            {
                context.Favorites.Add(this);
                await context.SaveChangesAsync();
            }
        }*/

    }
}