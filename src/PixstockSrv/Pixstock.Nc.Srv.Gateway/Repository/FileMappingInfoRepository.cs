using System.Linq;
using Katalib.Nc.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway.Repository
{
    public class FileMappingInfoRepository : GenericRepository<FileMappingInfo>, IFileMappingInfoRepository
    {
        public FileMappingInfoRepository(IAppDbContext context)
            : base((DbContext)context)
        {
        }

        public IFileMappingInfo Load(long id)
        {
            var set = _dbset
                .Include(prop => prop.Workspace);
            return set.Where(x => x.Id == id).FirstOrDefault();
        }

        public IFileMappingInfo New()
        {
            var entity = new FileMappingInfo();
            return this.Add(entity);
        }
    }
}