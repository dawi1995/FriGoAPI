using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models;
using FriGo.Db.Models.Social;
using Swashbuckle.Swagger.Annotations;
using FriGo.Db.Models.Recipes;
using Microsoft.AspNet.Identity;

namespace FriGo.Api.Controllers
{
    public class ImageController : BaseFriGoController
    {
        public ImageController(IMapper autoMapper) : base(autoMapper)
        {
        }

        /// <summary>
        /// Get image
        /// </summary>
        /// <param name="imageId"></param>
        /// <returns>Rating of a recipe</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(HttpResponseMessage))]
        [SwaggerResponse(HttpStatusCode.NotFound, Description = "Not found")]
        public virtual HttpResponseMessage Get(Guid imageId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Upload image 
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.Created,Description = "Image uploaded")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [Authorize]
        public virtual HttpResponseMessage Post()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete image
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [SwaggerResponseRemoveDefaults]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Image deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
