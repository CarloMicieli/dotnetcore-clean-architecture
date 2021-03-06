﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TreniniDotNet.Application.Catalog.Brands.EditBrand;
using TreniniDotNet.SharedKernel.Slugs;
using TreniniDotNet.Web.Infrastructure.UseCases;

namespace TreniniDotNet.Web.Catalog.V1.Brands.EditBrand
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class BrandsController : UseCaseController<EditBrandRequest, EditBrandPresenter>
    {
        public BrandsController(IMediator mediator, EditBrandPresenter presenter)
            : base(mediator, presenter)
        {
        }

        [HttpPut("{slug}")]
        [ProducesResponseType(typeof(EditBrandOutput), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> EditBrand(string slug, EditBrandRequest request)
        {
            request.BrandSlug = Slug.Of(slug);
            return HandleRequest(request);
        }
    }
}
