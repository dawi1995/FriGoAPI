using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Ingredients;
using FriGo.Db.DTO.Recipes;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using Microsoft.AspNet.Identity;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class RecipeNoteController : BaseFriGoController
    {
        private readonly IRecipeNoteService recipeNoteService;

        public RecipeNoteController(IMapper autoMapper, IRecipeNoteService recipeNoteService) : base(autoMapper)
        {
            this.recipeNoteService = recipeNoteService;
        }

        /// <summary>
        /// Returns all notes
        /// </summary>
        /// <returns>An array of user's notes</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<RecipeNote>))]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<RecipeNote> recipeNotes = recipeNoteService.Get();
            recipeNotes = recipeNotes.Where(recipeNote => recipeNote.UserId == new Guid(User.Identity.GetUserId()));

            return Request.CreateResponse(HttpStatusCode.OK, recipeNotes);
        }

        /// <summary>
        /// Get one note by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One note</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IngredientDto))]
        [SwaggerResponse(HttpStatusCode.Forbidden)]
        public virtual HttpResponseMessage Get(Guid id)
        {
            RecipeNote recipeNote = recipeNoteService.Get(id);
            bool isNoteByLoggedUser = recipeNote.UserId == new Guid(User.Identity.GetUserId());

            return isNoteByLoggedUser
                ? Request.CreateResponse(HttpStatusCode.OK, recipeNote)
                : Request.CreateResponse(HttpStatusCode.Forbidden);
        }

        /// <summary>
        /// Create new note for given recipe
        /// </summary>
        /// <param name="createRecipeNote"></param>
        /// <returns>Created ingredient</returns>
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(RecipeNote), Description = "Note created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        public virtual HttpResponseMessage Post(CreateRecipeNote createRecipeNote)
        {
            RecipeNote recipeNote = AutoMapper.Map<CreateRecipeNote, RecipeNote>(createRecipeNote);
            recipeNote.UserId = new Guid(User.Identity.GetUserId());
            recipeNoteService.Add(recipeNote);

            return Request.CreateResponse(HttpStatusCode.Created, recipeNote);
        }

        /// <summary>
        /// Modify existing text in a note
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editRecipeNote"></param>
        /// <returns>Modified note</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(RecipeNote), Description = "Recipe note updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(Error), Description = "You can edit only your notes")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public virtual HttpResponseMessage Put(Guid id, EditRecipeNote editRecipeNote)
        {
            RecipeNote recipeNote = recipeNoteService.Get(id);

            if (recipeNote == null)
            {
                var notFoundError = new Error
                {
                    Code = (int) HttpStatusCode.NotFound,
                    Message = string.Format(Properties.Resources.EntityNotFound, nameof(RecipeNote))
                };

                return Request.CreateResponse(HttpStatusCode.NotFound, notFoundError);
            }
            if (recipeNote.UserId != new Guid(User.Identity.GetUserId()))
                Request.CreateResponse(HttpStatusCode.Forbidden);

            AutoMapper.Map(editRecipeNote, recipeNote);

            recipeNoteService.Edit(recipeNote);

            return Request.CreateResponse(HttpStatusCode.OK, recipeNote);
        }

        /// <summary>
        /// Delete note
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Note deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.Forbidden, Type = typeof(Error), Description = "You can remove only your notes")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            RecipeNote recipeNote = recipeNoteService.Get(id);
            if (recipeNote.UserId != new Guid(User.Identity.GetUserId()))
                Request.CreateResponse(HttpStatusCode.Forbidden);

            recipeNoteService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
