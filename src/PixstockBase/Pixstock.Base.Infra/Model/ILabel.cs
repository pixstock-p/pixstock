namespace Pixstock.Base.Infra.Model
{
    public interface ILabel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string Name { get; set; }

        string OwnerType { get; set; }

        string Comment { get; set; }
    }
}