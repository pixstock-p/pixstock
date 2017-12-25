using System.Linq;
using Katalib.Nc.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway.Repository
{
    public class WorkspaceRepository : GenericRepository<Workspace>, IWorkspaceRepository
    {
        public WorkspaceRepository(IAppDbContext context) : base((DbContext)context)
        {
        }

        /// <summary>
        /// Workpaceの読み込み
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IWorkspace Load(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }
    }
}