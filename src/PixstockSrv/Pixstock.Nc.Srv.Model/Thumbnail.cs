using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pixstock.Base.Infra;
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Model
{
    [Table("svp_Thumbnail")]
    public class Thumbnail : IThumbnail
    {
        [Key]
        public long Id { get; set; }

        public string ThumbnailKey { get; set; }

        public byte[] BitmapBytes { get; set; }

        public string MimeType { get; set; }

        public ThumbnailType ThumbnailType { get; set; }
    }
}