using System.Collections.Generic;

namespace Pixstock.Nc.Srv.Infra.Model
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICategory : Pixstock.Base.Infra.Model.ICategory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="category"></param>
         void SetParentCategory(ICategory category);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         ICategory GetParentCategory();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
         List<IContent> GetContentList();
    }
}