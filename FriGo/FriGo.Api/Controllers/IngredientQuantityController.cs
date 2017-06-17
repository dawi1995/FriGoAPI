using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.IngredientQuantities;
using FriGo.Db.Models;
using FriGo.Db.Models.Authentication;
using FriGo.Db.Models.Ingredients;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace FriGo.Api.Controllers
{
    public class IngredientQuantityController : BaseFriGoController
    {
        private readonly IIngredientQuantityService ingredientQuantityService;
        private readonly IUserService userService;
        private readonly IIngredientService ingredientService;


        public IngredientQuantityController(IMapper autoMapper, IValidatingService validatingService,
            IIngredientQuantityService ingredientQuantityService, IUserService userService,
            IIngredientService ingredientService) : base(autoMapper, validatingService)
        {
            this.ingredientQuantityService = ingredientQuantityService;
            this.userService = userService;
            this.ingredientService = ingredientService;
        }

        /// <summary>
        /// Returns user's ingredients with quantities
        /// </summary>
        /// <returns>An array of all ingredients with quantities</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<IngredientQuantity>), Description = "Returns ingredients in user\'s fridge")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public HttpResponseMessage Get()
        {
            string userId = User.Identity.GetUserId();

            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityService.GetByUserId(userId));
        }

        /// <summary>
        /// Returns one ingredient with quantity by Id
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientQuantity), Description = "Return an ingredient in user's fridge with specified id")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public HttpResponseMessage Get(Guid id)
        {
            string userId = User.Identity.GetUserId();

            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityService.Get(userId, id));
        }

        /// <summary>
        /// Adds ingredient to user's fridge
        /// </summary>
        /// <param name="createIngredientQuantity"></param>
        /// <returns>Created ingredient with quantity</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(IngredientQuantity), Description = "Ingredient quantity created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        public HttpResponseMessage Post(CreateIngredientQuantity createIngredientQuantity)
        {
            string userId = User.Identity.GetUserId();
            User user =  userService.Get(userId);

            Ingredient ingredient = ingredientService.Get(createIngredientQuantity.IngredientId);

            IngredientQuantity ingredientQuantity = AutoMapper.Map<CreateIngredientQuantity, IngredientQuantity>(createIngredientQuantity);
            ingredientQuantity.Ingredient = ingredient;

            user.IngredientQuantities.Add(ingredientQuantity);

            userService.Edit(user);

            return Request.CreateResponse(HttpStatusCode.Created, ingredientQuantity);
        }

        /// <summary>
        /// Modify existing ingredient quantity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editIngredientQuantity"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientQuantity), Description = "Ingredient quantity updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public HttpResponseMessage Put(Guid id, EditIngredientQuantity editIngredientQuantity)
        {
            IngredientQuantity ingredientQuantity = ingredientQuantityService.Get(id);
            if (ingredientQuantity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new Error(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            string userId = User.Identity.GetUserId();
            if (!ingredientQuantityService.IsOwnedByUser(userId, id))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    new Error(HttpStatusCode.Forbidden,
                        Properties.Resources.IngredientQuantityPutForbiddenMessage));

            AutoMapper.Map(editIngredientQuantity, ingredientQuantity);

            ingredientQuantityService.Edit(ingredientQuantity);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantity);
        }

        /// <summary>
        /// Delete ingredient quantity
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Ingredient quantity deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public HttpResponseMessage Delete(Guid id)
        {
            IngredientQuantity ingredientQuantity = ingredientQuantityService.Get(id);
            if (ingredientQuantity == null)
                return Request.CreateResponse(HttpStatusCode.NotFound,
                    new Error(HttpStatusCode.NotFound, Properties.Resources.GenericNotFoundMessage));

            string userId = User.Identity.GetUserId();
            if (!ingredientQuantityService.IsOwnedByUser(userId, id))
                return Request.CreateResponse(HttpStatusCode.Forbidden,
                    new Error(HttpStatusCode.Forbidden,
                        Properties.Resources.IngredientQuantityDeleteForbiddenMessage));

            ingredientQuantityService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
