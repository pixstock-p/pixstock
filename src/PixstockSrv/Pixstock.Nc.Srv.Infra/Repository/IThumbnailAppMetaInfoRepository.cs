using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Infra.Repository
{
    public interface IThumbnailAppMetaInfoRepository : IRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IAppMetaInfo New();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IAppMetaInfo Load(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyName"></param>
        /// <returns></returns>
        IAppMetaInfo LoadByKey(string keyName);
    }
}