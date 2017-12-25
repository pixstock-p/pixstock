using System;
using System.Collections.Generic;

namespace Pixstock.Nc.Srv.Ext
{
    /// <summary>
    /// 拡張機能のエントリポイント
    /// </summary>
    public interface IExtentionMetaInfo
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        List<Type> Cutpoints();
    }
}