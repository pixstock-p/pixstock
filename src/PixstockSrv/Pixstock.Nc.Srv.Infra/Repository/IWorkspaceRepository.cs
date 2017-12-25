
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Infra.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IWorkspaceRepository : IRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IWorkspace Load(long id);
    }
}