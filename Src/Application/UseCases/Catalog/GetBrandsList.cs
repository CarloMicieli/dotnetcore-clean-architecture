﻿using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandsList;
using TreniniDotNet.Domain.Catalog.Brands;

namespace TreniniDotNet.Application.UseCases.Catalog
{
    public sealed class GetBrandsList : IGetBrandsListUseCase
    {
        private readonly IGetBrandsListOutputPort _outputPort;
        private readonly BrandService _brandService;

        public GetBrandsList(IGetBrandsListOutputPort outputPort, BrandService brandService)
        {
            _outputPort = outputPort;
            _brandService = brandService;
        }

        public async Task Execute(GetBrandsListInput input)
        {
            var brands = await _brandService.GetAllBrands();
            OutputPort.Standard(new GetBrandsListOutput(brands));
        }

        private IGetBrandsListOutputPort OutputPort => _outputPort;
    }
}
