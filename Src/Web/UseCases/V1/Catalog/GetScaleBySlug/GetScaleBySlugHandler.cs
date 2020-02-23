﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TreniniDotNet.Application.Boundaries.Catalog.GetScaleBySlug;

namespace TreniniDotNet.Web.UseCases.V1.Catalog.GetScaleBySlug
{
    public class GetScaleBySlugHandler : AsyncRequestHandler<GetScaleBySlugRequest>
    {
        private readonly IGetScaleBySlugUseCase _useCase;

        public GetScaleBySlugHandler(IGetScaleBySlugUseCase useCase)
        {
            _useCase = useCase;
        }

        protected override Task Handle(GetScaleBySlugRequest request, CancellationToken cancellationToken)
        {
            return _useCase.Execute(new GetScaleBySlugInput(request.Slug));
        }
    }
}