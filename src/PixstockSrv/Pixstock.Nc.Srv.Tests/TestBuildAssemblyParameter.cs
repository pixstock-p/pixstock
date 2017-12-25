using System.IO;

namespace Pixstock.Nc.Srv.Tests
{
    public class TestBuildAssemblyParameter : BuildAssemblyParameter
    {
        public TestBuildAssemblyParameter(){
            this.Params["TestMode"] = "true";
        }

        public void SetThread(int num)
        {
            this.Params["ApplicationDirectoryPath"] = Path.Combine("Pixstock.Srv.xUnit", @"Pixstock.Srv_" + num);
        }
    }
}