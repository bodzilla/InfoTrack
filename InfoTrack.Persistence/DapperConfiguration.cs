using Dapper;
using InfoTrack.Persistence.Mappers;

namespace InfoTrack.Persistence
{
    public sealed class DapperConfiguration
    {
        public DapperConfiguration() => SqlMapper.AddTypeHandler(new UriMapper());
    }
}
