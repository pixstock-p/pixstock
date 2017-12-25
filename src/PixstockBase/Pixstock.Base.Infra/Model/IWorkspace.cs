using System;

namespace Pixstock.Base.Infra.Model
{
    public interface IWorkspace
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        long Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string PhysicalPath { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DateTime? LastFullBuildDate { get; set; }
    }
}