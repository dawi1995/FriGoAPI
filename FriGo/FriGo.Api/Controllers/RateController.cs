using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;
using Microsoft.AspNet.Identity;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Recipes;
using System.Linq;

namespace FriGo.Api.Controllers
{
    public class RateController : BaseFriGoController
    {
        private readonly IRateService rateService;
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;

        public RateController(IMapper autoMapper, IValidatingService validatingService, IRateService rateService, 
            IRecipeService recipeService, IUserService userService) : base(autoMapper, validatingService)
        {
            this.rateService = rateService;
            this.recipeService = recipeService;
            this.userService = userService;
        }


        /// <summary>
        /// Rate recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="rateRecipe"></param>
        /// <returns></returns>
        [Route("api/Rate/{recipeId}")]
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(decimal), Description = "Rate")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Put(Guid recipeId, RateRecipe rateRecipe)
        {
            var user = userService.Get(User.Identity.GetUserId());
            var recipe = recipeService.Get(recipeId);
            var ratesForUser = recipeService.GetRatingByUser(user, recipe);
            if (recipe == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (ratesForUser == null)
            {
                Rate rate = AutoMapper.Map<RateRecipe, Rate>(rateRecipe);
                rate.Recipe = recipe;
                rate.User = user;
                recipe.Rates.Add(rate);
                return Request.CreateResponse(HttpStatusCode.OK, rate);
            }


            var ratesById = rateService.GetByRecipeId(recipeId);
            var rateById = ratesById.Where(rate => rate.User.Id == User.Identity.GetUserId()).First();
            if (rateById != null)
            {
                rateById.Rating = rateRecipe.Rate;
                rateService.Edit(rateById);
                return Request.CreateResponse(HttpStatusCode.OK, rateById);
            }


            return Request.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
