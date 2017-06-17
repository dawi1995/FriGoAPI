using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    [SwaggerResponseRemoveDefaults]
    [Authorize]
    public abstract class BaseFriGoController : ApiController
    {
        protected readonly IMapper AutoMapper;
        protected readonly IValidatingService ValidatingService;

        protected BaseFriGoController(IMapper autoMapper, IValidatingService validatingService)
        {
            AutoMapper = autoMapper;
            ValidatingService = validatingService;
        }
    }
}
