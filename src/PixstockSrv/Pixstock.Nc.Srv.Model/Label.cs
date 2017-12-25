using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Pixstock.Base.Infra.Model;

namespace Pixstock.Nc.Srv.Model
{
    [Table("svp_Label")]
    public class Label : ILabel
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public Category Category { get; set; }

        public string Comment { get; set; }

        public string OwnerType { get; set; }

        [JsonIgnore]
        public List<Label2Content> Contents { get; set; }
    }
}