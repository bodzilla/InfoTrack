using System;

namespace InfoTrack.Core.Contracts
{
    public interface IEntity
    {
        public int Id { get; set; }

        public DateTime EntityCreated { get; set; }
    }
}
