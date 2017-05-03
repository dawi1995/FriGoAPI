using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.IngredientQuantities;
using FriGo.Db.Models;
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


        public IngredientQuantityController(IMapper autoMapper, IIngredientQuantityService ingredientQuantityService, IUserService userService)
            : base(autoMapper)
        {
            this.ingredientQuantityService = ingredientQuantityService;
            this.userService = userService;

        }

        /// <summary>
        /// Returns user's ingredients with quantities
        /// </summary>
        /// <returns>An array of all ingredients with quantities</returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<IngredientQuantity>), Description = "Returns ingredients in user\'s fridge")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityService.Get());
        }

        /// <summary>
        /// Returns one ingredient with quantity by Id
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientQuantity), Description = "Return an ingredient in user's fridge with specified id")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public HttpResponseMessage Get(Guid id)
        {
            return Request.CreateResponse(HttpStatusCode.OK, ingredientQuantityService.Get(id));
        }

        /// <summary>
        /// Adds ingredient to user's fridge
        /// </summary>
        /// <param name="createIngredientQuantity"></param>
        /// <returns>Created ingredient with quantity</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(IngredientQuantity), Description = "Ingredient quantity created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [Authorize]
        public HttpResponseMessage Post(CreateIngredientQuantity createIngredientQuantity)
        {
            string id;
            id = User.Identity.GetUserId();
            id = RequestContext.Principal.Identity.GetUserId();
            throw new NotImplementedException();
        }

        /// <summary>
        /// Modify existing ingredient quantity
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editIngredientQuantity"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientQuantity), Description = "Ingredient quantity updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public HttpResponseMessage Put(Guid id, EditIngredientQuantity editIngredientQuantity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete ingredient quantity
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Ingredient quantity deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public HttpResponseMessage Delete(Guid id)
        {
            ingredientQuantityService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
