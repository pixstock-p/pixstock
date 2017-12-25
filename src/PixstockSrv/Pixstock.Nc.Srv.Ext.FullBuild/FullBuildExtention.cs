using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using LiteDB;
using Pixstock.Nc.Srv.Ext;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Ext.FullBuild.Model;
using Pixstock.Nc.Srv.Infra.Repository;
using NLog;
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Ext.FullBuild
{
    /// <summary>
    /// 
    /// </summary>
    public class FullBuildExtention : IExtentionMetaInfo
    {

        public List<Type> Cutpoints()
        {
            return new List<Type>(new Type[] { typeof(StartCutpoint) });
        }
    }
}