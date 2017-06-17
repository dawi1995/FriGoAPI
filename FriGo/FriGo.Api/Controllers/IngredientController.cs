using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class IngredientController : BaseFriGoController
    {
        private readonly IIngredientService ingredientService;
        private readonly IUnitService unitService;

        public IngredientController(IMapper autoMapper, IValidatingService validatingService,
            IIngredientService ingredientService, IUnitService unitService) : base(autoMapper, validatingService)
        {
            this.ingredientService = ingredientService;
            this.unitService = unitService;
        }

        /// <summary>
        /// Returns all ingredients
        /// </summary>
        /// <returns>An array of ingredients</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<IngredientDto>))]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<Ingredient> ingredients = ingredientService.Get();
            IEnumerable<IngredientDto> ingredientDtos =
                AutoMapper.Map<IEnumerable<Ingredient>, IEnumerable<IngredientDto>>(ingredients);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientDtos);
        }

        /// <summary>
        /// Get one ingredient by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One ingredient</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientDto))]
        public virtual HttpResponseMessage Get(Guid id)
        {
            Ingredient ingredient = ingredientService.Get(id);
            IngredientDto ingredientDto = AutoMapper.Map<Ingredient, IngredientDto>(ingredient);

            return Request.CreateResponse(HttpStatusCode.OK, ingredientDto);
        }

        /// <summary>
        /// Create new ingredient
        /// </summary>
        /// <param name="createIngredient"></param>
        /// <returns>Created ingredient</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(IngredientDto), Description = "Ingredient created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        public virtual HttpResponseMessage Post(CreateIngredient createIngredient)
        {
            Ingredient ingredient = AutoMapper.Map<CreateIngredient, Ingredient>(createIngredient);
            ingredientService.Add(ingredient);

            Ingredient createdIngredient = ingredientService.Get(ingredient.Id);
            IngredientDto createdIngredientDto = AutoMapper.Map<Ingredient, IngredientDto>(createdIngredient);
            createdIngredientDto.Unit = unitService.Get(ingredient.UnitId);

            return Request.CreateResponse(HttpStatusCode.Created, createdIngredientDto);
        }

        /// <summary>
        /// Modify existing ingredient
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editIngredient"></param>
        /// <returns>Modified ingredient</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientDto), Description = "Ingredient updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public virtual HttpResponseMessage Put(Guid id, EditIngredient editIngredient)
        {
            Ingredient ingredient = ingredientService.Get(id);

            if (ingredient == null)
            {
                var notFoundError = new Error
                {
                    Code = (int) HttpStatusCode.NotFound,
                    Message = Properties.Resources.IngredientNotFoundMessage
                };

                return Request.CreateResponse(HttpStatusCode.NotFound, notFoundError);
            }
            AutoMapper.Map(editIngredient, ingredient);

            ingredientService.Edit(ingredient);

            IngredientDto ingredientDto = AutoMapper.Map<Ingredient, IngredientDto>(ingredient);
            return Request.CreateResponse(HttpStatusCode.OK, ingredientDto);
        }

        /// <summary>
        /// Delete ingredient
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Ingredient deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            ingredientService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
