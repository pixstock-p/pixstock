using System.Linq;
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Infra.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICategoryRepository : IRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        ICategory New();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ICategory Load(long id);

        /// <summary>
        /// カテゴリ名に一致するエントリを検索
        /// </summary>
        /// <remarks>
        /// 制限事項として、このメソッドは同名のカテゴリが複数存在する場合ははじめの１つのみ返す。
        /// </remarks>
        /// <param name="categoryName">カテゴリ名</param>
        /// <returns></returns>
        ICategory LoadByName(string categoryName);

        /// <summary>
        /// カテゴリ名に一致するエントリを検索(親ディレクトリ指定版)
        /// </summary>
        /// <remarks>
        /// 制限事項として、このメソッドは同名のカテゴリが複数存在する場合ははじめの１つのみ返す。
        /// </remarks>
        /// <param name="categoryName">カテゴリ名</param>
        /// <param name="parentCategory">親ディレクトリ</param>
        /// <returns></returns>
        ICategory LoadByName(string categoryName, ICategory parentCategory);

        /// <summary>
        /// 指定した親カテゴリを持つカテゴリリストを取得する
        /// </summary>
        /// <remarks>
        /// 呼び出し元では、戻り値を使って子階層カテゴリを更に絞り込んでください。
        /// </remarks>
        /// <param name="parentCategory"></param>
        /// <returns>子階層カテゴリのクエリ</returns>
        IQueryable<ICategory> FindChildren(ICategory parentCategory);

        /// <summary>
        /// ルートカテゴリを取得
        /// </summary>
        /// <returns></returns>
        ICategory LoadRootCategory();
    }
}