using System.Collections.Generic;

namespace CocktailsApi.Models
{
    public class FilterCocktails
    {
        public Drink[] drinks { get; set; }
        
        }

    public class Drink
    {
        public string strDrink { get; set; }
        public string strDrinkThumb { get; set; }
        public string idDrink { get; set; }
    }

}