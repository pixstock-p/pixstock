using System;

namespace Pixstock.Nc.Srv.Infra.Model
{
    public interface IFileMappingInfo : Pixstock.Base.Infra.Model.IFileMappingInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="workspace"></param>
        void SetWorkspace(IWorkspace workspace);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IWorkspace GetWorkspace();
    }
}