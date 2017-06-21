using System;
using System.Collections.Generic;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using System.Linq;
using FriGo.Db.Models.Ingredients;

namespace FriGo.Services
{
    public class SearchEngine : ISearchEngine
    {
        public IEnumerable<Recipe> RawData { get; }

        public IEnumerable<Recipe> ProcessedRecipes { get; private set; }

        public SearchEngine(IEnumerable<Recipe> recipes)
        {
            RawData = recipes;
            ProcessedRecipes = recipes;
        }
        public void FilterByName(string nameSearchQuery)
        {
            if (nameSearchQuery != null)
                ProcessedRecipes = ProcessedRecipes.Where(x => x.Title.Contains(nameSearchQuery));
        }

        public void FilterByTag(Tag[] tags)
        {
            if(tags != null)
                ProcessedRecipes = ProcessedRecipes.Where(x => x.Tags.Select(y => y.Name)
                                     .Intersect(tags.Select(z => z.Name))
                                     .Any());
        }

        public void SortByField(string field, bool descending)
        {
            try
            {
                foreach (var Property in typeof(Recipe).GetType().GetProperties())
                {
                    if (field.ToLower() == Property.Name.ToLower())
                    {
                        Func<Recipe, object> key = x => Property.GetValue(x, null);
                        ProcessedRecipes = descending ? ProcessedRecipes.OrderByDescending(key) : ProcessedRecipes.OrderBy(key);
                    }
                }
                ProcessedRecipes = ProcessedRecipes.OrderBy(x => x.Rates);
            }
            catch (ArgumentException)
            {
                var checkProperty = typeof(Recipe).GetProperty(field,
                      System.Reflection.BindingFlags.Instance |
                      System.Reflection.BindingFlags.Public |
                      System.Reflection.BindingFlags.NonPublic).PropertyType;

                if (checkProperty == typeof(ICollection<IngredientQuantity>))
                    ProcessedRecipes = SortByFitness();
            }
        }

        private IEnumerable<Recipe> SortByFitness()
        {
            throw new NotImplementedException();
        }
    }
}
