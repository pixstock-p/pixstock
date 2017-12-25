using System.Linq;
using Katalib.Nc.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway.Repository
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(IAppDbContext context)
            : base((DbContext)context)
        {
        }

        /// <summary>
        /// エンティティの読み込み(静的メソッド)
        /// </summary>
        /// <remarks>
        /// エンティティの読み込みをワンライナーで記述できます。
        /// </remarks>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static IContent Load(IAppDbContext context, long id)
        {
            var repo = new ContentRepository(context);
            return repo.Load(id);
        }

        /// <summary>
        /// Contentの読み込み
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IContent Load(long id)
        {
            var set = _dbset
                .Include(prop => prop.Category)
                .Include(prop => prop.FileMappingInfo);
            return set.Where(x => x.Id == id).FirstOrDefault();
        }

        public IContent New()
        {
            var entity = new Content();
            return this.Add(entity);
        }
    }
}