﻿using System;
using System.Data;
using Dapper;
using TreniniDotNet.Domain.Catalog.Scales;
using TreniniDotNet.Domain.Catalog.ValueObjects;

namespace TreniniDotNet.Infrastructure.Persistence.TypeHandlers
{
    public class ScaleIdTypeHandler : SqlMapper.TypeHandler<ScaleId?>
    {
        public override ScaleId? Parse(object value)
        {
            if (value is null)
                return null;

            if (Guid.TryParse(value.ToString(), out var id))
            {
                return new ScaleId(id);
            }

            return ScaleId.Empty;
        }

        public override void SetValue(IDbDataParameter parameter, ScaleId? value)
        {
            parameter.Value = value?.ToGuid();
        }
    }
}
