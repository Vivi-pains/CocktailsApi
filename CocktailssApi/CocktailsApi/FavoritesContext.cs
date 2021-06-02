using CocktailsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailsApi
{
    public class FavoritesContext : DbContext
    {
        public FavoritesContext(DbContextOptions<FavoritesContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Favorite> Favorites { get; set; }
    }
}