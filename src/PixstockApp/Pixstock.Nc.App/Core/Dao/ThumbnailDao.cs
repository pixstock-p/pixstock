using Pixstock.Nc.App.Models;

namespace Pixstock.Nc.App.Core.Dao
{
    public class ThumbnailDao
    {
        public Thumbnail LoadByThumbnailKey(string thumbnailKey)
        {
            return new Thumbnail { ThumbnailSourceUri = "cli/Sample01/ThumbnailImageFile/" + thumbnailKey };
        }
    }
}