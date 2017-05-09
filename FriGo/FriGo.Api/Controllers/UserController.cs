using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Social;
using FriGo.Db.Models;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class UserController : BaseFriGoController
    {
        public UserController(IMapper autoMapper) : base(autoMapper)
        {
        }

        /// <summary>
        /// Get user info
        /// </summary>
        /// <param name="id"></param>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(UserDto))]
        public virtual HttpResponseMessage Get(Guid id)
        {   
            throw new NotImplementedException();
        }      
    }
}
