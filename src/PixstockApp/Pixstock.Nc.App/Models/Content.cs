using Pixstock.Base.Infra.Model;

namespace Pixstock.Nc.App.Models
{
    public class Content : IContent
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string IdentifyKey { get; set; }

        public string ContentHash { get; set; }

        public string ThumbnailKey { get; set; }

        public string Caption { get; set; }

        public string Comment { get; set; }

        public bool ArchiveFlag { get; set; }

        public bool ReadableFlag { get; set; }

        public int StarRating { get; set; }
    }
}