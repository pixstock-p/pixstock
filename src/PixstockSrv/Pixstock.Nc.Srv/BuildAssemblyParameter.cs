using System.Collections.Generic;
using Pixstock.Nc.Common;

namespace Pixstock.Nc.Srv
{
    public class BuildAssemblyParameter : IBuildAssemblyParameter
    {
        private readonly Dictionary<string, string> _Params = new Dictionary<string, string>();

        public Dictionary<string, string> Params => _Params;

        public BuildAssemblyParameter()
        {
            BuildParams();
        }

        private void BuildParams()
        {
            if (!_Params.ContainsKey("ApplicationDirectoryPath"))
            {
                this.Params.Add("ApplicationDirectoryPath", @"Pixstock.Srv");
            }
        }
    }
}