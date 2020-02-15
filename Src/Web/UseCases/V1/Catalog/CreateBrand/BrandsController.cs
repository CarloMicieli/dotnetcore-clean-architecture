﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.CreateBrand
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly CreateBrandPresenter _presenter;

        public BrandsController(IMediator mediator, CreateBrandPresenter presenter)
        {
            _mediator = mediator;
            _presenter = presenter;
        }

        /// <summary>
        /// Create a new brand
        /// </summary>
        /// <response code="200">The new brand was created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Error.</response>
        /// <param name="request">The request to create a new brand.</param>
        /// <returns>The newly created brand.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateBrandResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateBrandRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _mediator.Send(request);
            return _presenter.ViewModel;
        }
    }
}