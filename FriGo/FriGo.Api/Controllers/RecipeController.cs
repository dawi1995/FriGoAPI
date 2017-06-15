﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models;
using FriGo.Db.DTO;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;
using System.Linq;
using FriGo.Db.Models.Ingredients;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace FriGo.Api.Controllers
{
    public class RecipeController : BaseFriGoController
    {
        private readonly IRecipeService recipeService;
        private readonly IUserService userService;
        private readonly IFitnessService fitnessService;
        private readonly IRecipeNoteService recipeNoteService;


        public RecipeController(IMapper autoMapper, IRecipeService recipeService, IUserService userService,
            IFitnessService fitnessService, IRecipeNoteService recipeNoteService) : base(autoMapper)
        {
            this.recipeService = recipeService;
            this.userService = userService;
            this.fitnessService = fitnessService;
            this.recipeNoteService = recipeNoteService;
        }

        /// <summary>
        /// Get one recipe by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One type of unit</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeDto))]
        public virtual HttpResponseMessage Get(Guid id)
        {
            Recipe recipeResult = recipeService.Get(id);
            if (recipeResult != null)
            {
                RecipeDto returnRecipe = AutoMapper.Map<Recipe, RecipeDto>(recipeResult);
                returnRecipe.Notes = recipeNoteService.Get(id, new Guid(User.Identity.GetUserId()));

                return Request.CreateResponse(HttpStatusCode.OK, returnRecipe);
            }
            else
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, $"Recipe {id} do not exist");
        }

        /// <summary>
        /// Get recipe by parameters
        /// </summary>
        /// <param name="page">Number of page</param>
        /// <param name="perPage">Count per page</param>
        /// <param name="sortField">Sorting by field</param>
        /// <param name="fitness">Sorting by fitness</param>
        /// <param name="descending">Sorting order</param>
        /// <param name="nameSearchQuery">Search by name</param>
        /// <param name="tagQuery">Search by tags</param>
        /// <returns></returns>

        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeDto))]
        public virtual HttpResponseMessage Get(Tag[] tagQuery, int page = 1, int perPage = 10, string sortField = null, decimal fitness=0,
            bool descending = false, string nameSearchQuery = null)
        {
            //if (IsEmpty(tagQuery))
            //    tagQuery = TakeAllTags().ToArray();
            if (recipeService.Engine.RawData != null)
            {
                recipeService.Engine.FilterByName(nameSearchQuery);
                recipeService.Engine.FilterByTag(tagQuery);
                recipeService.Engine.SortByField(sortField, descending);
                fitnessService.EngineFitness.SortByFitness(fitness); //do implementacji sortowanie

                IEnumerable<Recipe> recipeResults = recipeService.Engine.ProcessedRecipes
                                                        .Skip((page - 1) * perPage).Take(perPage).ToList();

                if (recipeResults.Any())
                {
                    IEnumerable<RecipeDto> returnRecipes = AutoMapper.Map<IEnumerable<Recipe>, IEnumerable<RecipeDto>>(recipeResults).ToList();
                    returnRecipes.ForEach(recipe => recipe.Notes = recipeNoteService.Get(recipe.Id, new Guid(User.Identity.GetUserId())));

                    return Request.CreateResponse(HttpStatusCode.OK, returnRecipes);
                }
                return Request.CreateResponse(HttpStatusCode.NoContent);
            }
            return Request.CreateResponse(HttpStatusCode.InternalServerError);
        }
        private bool IsEmpty(Tag[] tagQuery)
        {
            return tagQuery.Count() > 0;
        }
        private ICollection<Tag> TakeAllTags()
        {
            throw new NotImplementedException();
        }
        

        

        /// <summary>
        /// Create new recipe
        /// </summary>
        /// <param name="id"></param>
        /// <param name="createRecipe"></param>
        /// <returns>Created unit</returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(RecipeDto), Description = "Recipe created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [Authorize]
        public virtual IHttpActionResult Post(Guid id, CreateRecipe createRecipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Recipe newRecipe = AutoMapper.Map<CreateRecipe,Recipe>(createRecipe);

            var uid = User.Identity.GetUserId();
            FriGo.Db.Models.Authentication.User user = userService.Get(uid);

            if (user == null)
                return Unauthorized();

            newRecipe.User = user;
            recipeService.Add(newRecipe);

            return Created("",newRecipe);
        }

        /// <summary>
        /// Delete recipe
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Recipe deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            var uid = User.Identity.GetUserId();
            FriGo.Db.Models.Authentication.User user = userService.Get(uid);
            Recipe recipe = recipeService.Get(id);

            if(recipe == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            if (recipe.User != user)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            recipeService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (result.Succeeded) return null;
            if (result.Errors != null)
            {
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, so just return an empty BadRequest.
                return BadRequest();
            }

            return BadRequest(ModelState);
        }
        /* Upload file example
         * 
        public async Task<byte[]> UploadImage()
        {
            if (!Request.Content.IsMimeMultipartContent())
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);

            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);

            if (provider.Contents.Count == 0) return null;

            var file = provider.Contents[0];
            var filename = file.Headers.ContentDisposition.FileName.Trim('\"');
            var buffer = await file.ReadAsByteArrayAsync();
            return buffer;
        }
        */
    }

}
