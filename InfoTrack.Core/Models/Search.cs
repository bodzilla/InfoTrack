using System;
using InfoTrack.Core.Contracts;
using InfoTrack.Core.Enums;
using InfoTrack.Core.Helpers;

namespace InfoTrack.Core.Models
{
    public sealed class Search : IEntity
    {
        private string _query;
        private string _url;

        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateTime EntityCreated { get; set; }

        public SearchEngine SearchEngine { get; set; }

        public string Query
        {
            get => _query;
            set => _query = value.Trim();
        }

        public string Url
        {
            get => _url;
            set
            {
                _url = value;
                Uri = new Uri(UriHelper.ToUri(_url));
            }
        }

        public Uri Uri { get; private set; }
    }
}
