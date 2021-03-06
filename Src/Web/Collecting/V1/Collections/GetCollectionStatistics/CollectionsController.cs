﻿using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;
using TreniniDotNet.Web.Collecting.V1.Collections.Common.ViewModels;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionStatistics
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class CollectionsController : UseCaseController<GetCollectionStatisticsRequest, GetCollectionStatisticsPresenter>
    {
        public CollectionsController(IMediator mediator, GetCollectionStatisticsPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpGet("{id}/statistics")]
        [ProducesResponseType(typeof(CollectionStatisticsView), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(Guid id)
        {
            var request = new GetCollectionStatisticsRequest
            {
                Id = id,
                Owner = CurrentUser
            };

            return HandleRequest(request);
        }
    }
}
