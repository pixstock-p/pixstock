using System.Linq;
using Katalib.Nc.Entity.Repository;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Infra.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public CategoryRepository(IAppDbContext context)
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
        public static ICategory Load(IAppDbContext context, long id)
        {
            var repo = new CategoryRepository(context);
            return repo.Load(id);
        }

        public IQueryable<ICategory> FindChildren(ICategory parentCategory)
        {
            var set = _dbset
                .Include(prop => prop.ParentCategory)
                .Include(prop => prop.Labels)
                .Include(prop => prop.Contents);
            return set.Where(x => x.ParentCategory.Id == parentCategory.Id);
        }

        /// <summary>
        /// Categoryの読み込み
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ICategory Load(long id)
        {
            var set = _dbset
                .Include(prop => prop.ParentCategory)
                .Include(prop => prop.Labels)
                .Include(prop => prop.Contents);
            return set.Where(x => x.Id == id).FirstOrDefault();
        }

        public ICategory LoadByName(string categoryName)
        {
            var set = _dbset
                .Include(prop => prop.ParentCategory)
                .Include(prop => prop.Labels)
                .Include(prop => prop.Contents);
            var entity = set.Where(x => x.Name == categoryName).FirstOrDefault();
            return entity;
        }

        public ICategory LoadByName(string categoryName, ICategory parentCategory)
        {
            var set = _dbset
                .Include(prop => prop.ParentCategory)
                .Include(prop => prop.Labels)
                .Include(prop => prop.Contents);
            var entity = set.Where(x => x.Name == categoryName && x.ParentCategory.Id == parentCategory.Id).FirstOrDefault();
            return entity;
        }

        public ICategory LoadRootCategory()
        {
            return Load(1L);
        }

        public ICategory New()
        {
            var entity = new Category();
            return this.Add(entity);
        }
    }
}