using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Infra.Repository
{
    public interface IFileMappingInfoRepository : IRepositoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IFileMappingInfo Load(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IFileMappingInfo New();
    }
}