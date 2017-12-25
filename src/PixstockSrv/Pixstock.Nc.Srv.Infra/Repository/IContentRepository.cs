
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Infra.Repository
{
    public interface IContentRepository : IRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IContent New();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IContent Load(long id);
    }
}