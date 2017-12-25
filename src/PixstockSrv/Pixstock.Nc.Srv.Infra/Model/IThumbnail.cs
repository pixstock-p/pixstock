namespace Pixstock.Nc.Srv.Infra.Model
{
    public interface IThumbnail : Pixstock.Base.Infra.Model.IThumbnail
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        byte[] BitmapBytes { get; set; }
    }
}