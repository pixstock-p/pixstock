using System.ComponentModel.DataAnnotations.Schema;
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Model
{
    [Table("svp_AppMetaInfo")]
    public class AppMetaInfo : IAppMetaInfo
    {
        public long Id { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }
    }
}