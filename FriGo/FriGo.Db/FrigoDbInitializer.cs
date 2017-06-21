using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using FriGo.Db.Models.Ingredients;
using Image = FriGo.Db.Models.Image;

namespace FriGo.Db
{
    public class FrigoDbInitializer : DropCreateDatabaseIfModelChanges<FrigoContext>
    {
        private string[] SplitList(string list)
        {
            return list.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        private IEnumerable<Image> CreateImages()
        {
            var imageConverter = new ImageConverter();
            var randomUserId = new Guid();

            var testImage = new Image
            {
                Id = new Guid(new string(Properties.Resources.DiGgeRetsae.ToCharArray().Reverse().ToArray())),
                ImageBytes = Convert.FromBase64String(Properties.Resources.Base64Image),
                UserId = randomUserId
            };
            var dessertImage = new Image
            {
                Id = new Guid(Properties.Resources.DessertImageId),
                ImageBytes = imageConverter.ConvertTo(Properties.Resources.dessert, typeof(byte[])) as byte[],
                UserId = randomUserId
            };
            var mainCourseImage = new Image
            {
                Id = new Guid(Properties.Resources.MainCourseImageId),
                ImageBytes = imageConverter.ConvertTo(Properties.Resources.main, typeof(byte[])) as byte[],
                UserId = randomUserId
            };
            var appetizer = new Image
            {
                Id = new Guid(Properties.Resources.AppetizerImageId),
                ImageBytes = imageConverter.ConvertTo(Properties.Resources.appetizer, typeof(byte[])) as byte[],
                UserId = randomUserId
            };

            return new List<Image>{testImage, dessertImage, mainCourseImage, appetizer};
        }

        private IEnumerable<Ingredient> CreateIngredients(IEnumerable<Unit> units)
        {
            string[] ingredientList = SplitList(Properties.Resources.IngredientList);

            List<Ingredient> ingredients =
                ingredientList.Select(
                        ingredientName =>
                            new Ingredient
                            {
                                Id = Guid.NewGuid(),
                                Name = ingredientName,
                                UnitId = units.FirstOrDefault().Id
                            })
                    .ToList();

            return ingredients;
        }

        private IEnumerable<Unit> CreateUnits()
        {
            string[] unitList = SplitList(Properties.Resources.UnitList); 

            List<Unit> units = unitList.Select(unitName => new Unit {Id = Guid.NewGuid(), Name = unitName}).ToList();

            return units;
        }

        protected override void Seed(FrigoContext context)
        {
            List<Unit> units = CreateUnits().ToList();
            context.Set<Unit>().AddRange(units);

            context.Set<Ingredient>().AddRange(CreateIngredients(units));

            context.Set<Image>().AddRange(CreateImages());

            base.Seed(context);
        }
    }
}