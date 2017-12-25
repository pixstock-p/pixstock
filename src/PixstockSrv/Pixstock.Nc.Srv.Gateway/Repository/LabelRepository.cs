using System.Linq;
using Katalib.Nc.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway.Repository
{
    public class LabelRepository : GenericRepository<Label>
    {
        public LabelRepository(IAppDbContext context)
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
        public static Label Load(IAppDbContext context, long id)
        {
            var repo = new LabelRepository(context);
            return repo.Load(id);
        }

        /// <summary>
        /// エンティティの読み込み
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Label Load(long id)
        {
            var set = _dbset
                .Include(prop => prop.Category)
                .Include(prop => prop.Contents)
                    .ThenInclude(content => content.Content);
            var entity = set.Where(x => x.Id == id).FirstOrDefault();
            return entity;
        }
    }
}