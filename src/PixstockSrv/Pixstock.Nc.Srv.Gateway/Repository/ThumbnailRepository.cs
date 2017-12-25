using System.Linq;
using Katalib.Nc.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway.Repository
{
    public class ThumbnailRepository : GenericRepository<Thumbnail>, IThumbnailRepository
    {
        public ThumbnailRepository(IThumbnailDbContext context) : base((DbContext)context)
        {
        }

        public void Delete(IThumbnail entity)
        {
            this.Delete((Thumbnail)entity);
        }

        public IQueryable<IThumbnail> FindByKey(string key)
        {
            return _dbset.Where(x => x.ThumbnailKey == key);
        }

        public IThumbnail Load(long id)
        {
            return _dbset.Where(x => x.Id == id).FirstOrDefault();
        }

        public IThumbnail New()
        {
            var entity = new Thumbnail();
            return this.Add(entity);
        }
    }
}