using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models;
using FriGo.Db.Models.Social;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;
using FriGo.Db.Models.Recipes;
using Microsoft.AspNet.Identity;

namespace FriGo.Api.Controllers
{
    public class CommentController : BaseFriGoController
    {
        private readonly ICommentService commentService;
        private readonly IRecipeService recipeService;
        public CommentController(IMapper autoMapper, ICommentService commentService, IRecipeService recipeService) : base(autoMapper)
        {
            this.commentService = commentService;
            this.recipeService = recipeService;
            
        }

        /// <summary>
        /// Get comments of recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>Rating of a recipe</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<CommentDto>))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Not found")]
        public virtual HttpResponseMessage Get(Guid recipeId)
        {
            Recipe recipe = recipeService.Get(recipeId);
            if (recipe == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);

            ICollection<CommentDto> commentDto = AutoMapper.Map<ICollection<Comment>, ICollection<CommentDto>>(recipe.Comments);

            return Request.CreateResponse(HttpStatusCode.OK, commentDto);
        }

        /// <summary>
        /// Edit comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editComment"></param>
        /// <returns></returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(CommentDto), Description = "Comment updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Put(Guid id, EditComment editComment)
        {
            Comment comment = commentService.Get(id);
            if (comment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            if (comment.User.Id != User.Identity.GetUserId())
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            comment.Text = editComment.Text;
            commentService.Edit(comment);
            CommentDto commentDto = AutoMapper.Map<Comment, CommentDto>(comment);
            return Request.CreateResponse(HttpStatusCode.OK, commentDto);
        }

        /// <summary>
        /// Comment recipe
        /// </summary>
        /// <param name="recipeId"></param>
        /// <param name="createComment"></param>
        /// <returns></returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(CommentDto), Description = "Comment created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [Authorize]
        public virtual HttpResponseMessage Post(Guid recipeId, CreateComment createComment)
        {
            Recipe recipe = recipeService.Get(recipeId);
            if (recipe == null) 
                return Request.CreateResponse(HttpStatusCode.NotFound);
            Comment comment = AutoMapper.Map<CreateComment, Comment>(createComment);
            recipe.Comments.Add(comment);
            commentService.Add(comment);
            CommentDto commentDto = AutoMapper.Map<Comment, CommentDto>(comment);
            return Request.CreateResponse(HttpStatusCode.Created, commentDto);

        }

        /// <summary>
        /// Delete comment
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Comment deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            Comment comment = commentService.Get(id);
            if (comment == null)
                return Request.CreateResponse(HttpStatusCode.NotFound);
            if (comment.User.Id != User.Identity.GetUserId())
            {
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
            commentService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.NoContent);

        }
    }
}
