﻿using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.GetBrandBySlug;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public sealed class GetBrandBySlugOutputPort : OutputPortTestHelper<GetBrandBySlugOutput>, IGetBrandBySlugOutputPort
    {
        private MethodInvocation<string> BrandNotFoundMethod { set; get; }

        public GetBrandBySlugOutputPort()
        {
            this.BrandNotFoundMethod = MethodInvocation<string>.NotInvoked(nameof(BrandNotFound));
        }

        public void BrandNotFound(string message)
        {
            this.BrandNotFoundMethod = this.BrandNotFoundMethod.Invoked(message);
        }

        public void ShouldHaveBrandNotFoundMessage(string expectedMessage)
        {
            this.BrandNotFoundMethod.ShouldBeInvokedWithTheArgument(expectedMessage);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    BrandNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
