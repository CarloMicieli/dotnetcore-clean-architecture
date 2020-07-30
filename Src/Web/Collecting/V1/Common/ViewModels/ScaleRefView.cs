﻿using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Collecting.Shared;

namespace TreniniDotNet.Web.Collecting.V1.Common.ViewModels
{
    public sealed class ScaleRefView
    {
        private readonly Scale _scale;

        public ScaleRefView(Scale scale)
        {
            _scale = scale;
        }

        public string Slug => _scale.Slug;
        public string Value => _scale.Name;
    }
}
