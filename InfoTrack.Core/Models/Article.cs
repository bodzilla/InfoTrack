﻿using System;
using InfoTrack.Core.Contracts;

namespace InfoTrack.Core.Models
{
    public sealed class Article : IEntity
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateTime EntityCreated { get; set; }

        public Search Search { get; set; }

        public string Title { get; set; }

        public int Rank { get; set; }

        public Uri Url { get; set; }
    }
}
