using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Extensions;
using StarkIT.Application.Features.Name.Commands.CreateNewName;
using StarkIT.Application.Features.Name.Queries.GetNamesList;
using StarkIT.Application.Features.Name.Queries.GetNamesListFiltered;
using StarkIT.Domain.Models;
using System.Net;

namespace StarkIT.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NamesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<NamesController> _logger;
        private readonly IMapper _mapper;

        public NamesController(IMediator mediator, ILogger<NamesController> logger, IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ICollection<Names>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetNames([FromQuery] string? name = null, string? gender = null)
        {
            ICollection<Names> listNames;

            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(gender))
            {
                var query = new GetNamesListQuery();
                listNames = await _mediator.Send(query);
            }
            else
            {
                var query = new GetNamesListFilteredQuery(x =>
                    x.Name.ToUpper().StartsWith(name!.ToUpper()) ||
                    x.Gender.GetDisplayName() == gender!.ToUpper());

                listNames = await _mediator.Send(query);
            }

            return Ok(listNames);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateName(CreateNewNameCommand newNameObject) 
        {
            var result = await _mediator.Send(newNameObject);

            return Ok();
        }

        //Si el requerimiento puede admitir un endpoint mas este seria el URL
        //[HttpGet("filtered")]
        //[ProducesResponseType(typeof(ICollection<Names>),(int)HttpStatusCode.OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> GetNamesFiltered([FromQuery] string name, string gender)
        //{
        //    var query = new GetNamesListFilteredQuery(x => 
        //        x.Names.ToUpper().StartsWith(name.ToUpper()) && 
        //        x.Gender.ToString() == gender.ToUpper());

        //    var listNames = await _mediator.Send(query);

        //    return Ok(listNames);
        //}
    }
}
