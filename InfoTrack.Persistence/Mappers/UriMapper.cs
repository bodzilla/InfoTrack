using System;
using System.Data;
using Dapper;

namespace InfoTrack.Persistence.Mappers
{
    public sealed class UriMapper : SqlMapper.TypeHandler<Uri>
    {
        public override void SetValue(IDbDataParameter parameter, Uri value) => parameter.Value = value.ToString();

        public override Uri Parse(object value) => new Uri((string)value);
    }
}
