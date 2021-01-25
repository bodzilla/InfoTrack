using System;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Enums;

namespace InfoTrack.Core.Models
{
    public sealed class Search : IEntity
    {
        private string _query;

        /// <inheritdoc />
        public int Id { get; set; }

        public SearchEngine SearchEngine { get; set; }

        public string Query
        {
            get => _query;
            set => _query = value.Trim();
        }

        public Uri Uri { get; set; }
    }
}
