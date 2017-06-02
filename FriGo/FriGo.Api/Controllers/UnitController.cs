using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using FriGo.Db.DTO.Units;
using FriGo.Db.Models;
using FriGo.Db.Models.Ingredients;
using FriGo.ServiceInterfaces;
using Swashbuckle.Swagger.Annotations;

namespace FriGo.Api.Controllers
{
    public class UnitController : BaseFriGoController
    {
        private readonly IUnitService unitService;
        public UnitController(IMapper autoMapper,IUnitService unitService) : base(autoMapper)
        {
            this.unitService = unitService;
        }

        /// <summary>
        /// Returns all types of unit
        /// </summary>
        /// <returns>An array of units</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(IEnumerable<Unit>))]
        public virtual HttpResponseMessage Get()
        {
            IEnumerable<Unit> units = unitService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, units);
        }

        /// <summary>
        /// Get one unit by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>One type of unit</returns>
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Unit))]
        public virtual HttpResponseMessage Get(Guid id)
        {
            Unit unit = unitService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, unit);
        }

        /// <summary>
        /// Create new type of unit
        /// </summary>
        /// <param name="createUnit"></param>
        /// <returns>Created unit</returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.Created, Type = typeof(Unit), Description = "Unit created")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [Authorize]
        public virtual HttpResponseMessage Post(CreateUnit createUnit)
        {
            Unit unit = AutoMapper.Map<CreateUnit, Unit>(createUnit);
            unitService.Add(unit);

            Unit createdUnit = unitService.Get(unit.Id);

            return Request.CreateResponse(HttpStatusCode.Created, createdUnit);
        }

        /// <summary>
        /// Modify existing type of unit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editUnit"></param>
        /// <returns>Modified unit</returns>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.OK, Type = typeof(Unit), Description = "Unit updated")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Put(Guid id, EditUnit editUnit)
        {
            Unit unit = unitService.Get(id);

            if (unit == null)
            {
                var notFoundError = new Error
                {
                    Code = (int)HttpStatusCode.NotFound,
                    Message = Properties.Resources.UnitNotFoundMessage
                };

                return Request.CreateResponse(HttpStatusCode.NotFound, notFoundError);
            }
            AutoMapper.Map(editUnit, unit);
            unitService.Edit(unit);

            Unit editedUnit = unitService.Get(id);
            return Request.CreateResponse(HttpStatusCode.OK, editedUnit);
        }

        /// <summary>
        /// Delete type of unit
        /// </summary>
        /// <param name="id"></param>
        [Authorize]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Unit deleted")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, Type = typeof(Error), Description = "Forbidden")]
        [SwaggerResponse(HttpStatusCode.NotFound, Type = typeof(Error), Description = "Not found")]
        [Authorize]
        public virtual HttpResponseMessage Delete(Guid id)
        {
            unitService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
