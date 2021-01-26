using Dapper;
using InfoTrack.Persistence.Mappers;

namespace InfoTrack.Persistence
{
    public sealed class DapperConfiguration
    {
        public void ConfigureMappings() => SqlMapper.AddTypeHandler(new UriMapper());
    }
}
