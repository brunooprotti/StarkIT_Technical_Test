using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using StarkIT.Application.Contracts.Persistence;
using StarkIT.Application.Features.Users.Queries.GetNamesList;
using StarkIT.Application.Features.Users.Queries.GetNamesListFiltered;
using StarkIT.Domain.Models;
using System.Collections.ObjectModel;
using System.Net;

namespace StarkIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NamesController> _logger;

        public NamesController(IMediator mediator, ILogger<NamesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<User>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNames([FromQuery] string name = null, string gender = null)
        {
            ICollection<User> listNames;

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(gender))
            {
                var query = new GetNamesListQuery();
                listNames = await _mediator.Send(query);
            }
            else
            {
                var query = new GetNamesListFilteredQuery(x =>
                    x.Name.ToUpper().StartsWith(name.ToUpper()) ||
                    x.Gender.GetDisplayName() == gender.ToUpper());

                listNames = await _mediator.Send(query);
            }

            return Ok(listNames);
        }

        //Si el requerimiento puede admitir un endpoint mas este seria el URL
        //[HttpGet("filtered")]
        //[ProducesResponseType(typeof(ICollection<User>),(int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetNamesFiltered([FromQuery] string name, string gender)
        //{
        //    var query = new GetNamesListFilteredQuery(x => 
        //        x.Name.ToUpper().StartsWith(name.ToUpper()) && 
        //        x.Gender.ToString() == gender.ToUpper());

        //    var listNames = await _mediator.Send(query);

        //    return Ok(listNames);
        //}
    }
}
