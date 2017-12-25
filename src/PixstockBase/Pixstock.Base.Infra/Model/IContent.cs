namespace Pixstock.Base.Infra.Model
{
    public interface IContent
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        long Id { get; set; }

        string Name { get; set; }

        string IdentifyKey { get; set; }

        string ContentHash { get; set; }

        string ThumbnailKey { get; set; }

        string Caption { get; set; }

        string Comment { get; set; }

        bool ArchiveFlag { get; set; }

        bool ReadableFlag { get; set; }

        int StarRating { get; set; }
    }
}