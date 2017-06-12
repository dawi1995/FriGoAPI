using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.Models.Recipes;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class TagController : BaseFriGoController
    {
        private readonly ITagService tagService;

        public TagController(IMapper autoMapper, ITagService tagService) : base(autoMapper)
        {
            this.tagService = tagService;
        }

        /// <summary>
        /// Returns all tags
        /// </summary>
        /// <returns>An array of tags</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Tag>))]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<Tag> tags = tagService.Get();

            return Request.CreateResponse(HttpStatusCode.OK, tags);
        }       
    }
}
