using System.Collections.Generic;
using InfoTrack.Core.Models;

namespace InfoTrack.WebApp.ViewModels
{
    public sealed class ResultViewModel
    {
        public Search Search { get; set; }

        public IEnumerable<Article> Articles { get; set; }
    }
}
