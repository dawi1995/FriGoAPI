using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models;
using FriGo.Db.Models.Social;
using Swashbuckle.Swagger.Annotations;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using Microsoft.AspNet.Identity;

namespace FriGo.Api.Controllers
{
    public class ImageController : BaseFriGoController
    {
        private readonly IImageService imageService;

        public ImageController(IMapper autoMapper, IValidatingService validatingService,
            IImageService imageService) : base(autoMapper, validatingService)
        {
            this.imageService = imageService;
        }

        /// <summary>
        /// Get image
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Binary stream of image</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(HttpResponseMessage))]
        [AllowAnonymous]
        public virtual HttpResponseMessage Get(Guid id)
        {
            var returnMessage = new HttpResponseMessage();

            Image image = imageService.Get(id);

            returnMessage.Content = new ByteArrayContent(image != null ? image.ImageBytes : new byte[]{});
            returnMessage.Content.Headers.ContentType = new MediaTypeHeaderValue(Properties.Resources.PngMediaHeader);

            return returnMessage;
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
