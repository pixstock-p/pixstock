namespace Pixstock.Base.Infra.Model
{
    public interface ICategory
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
    }
}