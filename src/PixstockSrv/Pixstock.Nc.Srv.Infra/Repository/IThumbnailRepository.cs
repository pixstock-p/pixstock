using System.Linq;
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Infra.Repository
{
    public interface IThumbnailRepository : IRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IThumbnail Load(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IQueryable<IThumbnail> FindByKey(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(IThumbnail entity);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IThumbnail New();
    }
}