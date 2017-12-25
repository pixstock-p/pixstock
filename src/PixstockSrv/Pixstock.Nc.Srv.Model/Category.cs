using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Pixstock.Nc.Srv.Infra.Model;
using Newtonsoft.Json;

namespace Pixstock.Nc.Srv.Model
{
    [Table("svp_Category")]
    public class Category : ICategory
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public Category ParentCategory { get; set; }

        [JsonIgnore]
        public List<Label> Labels { get; set; }

        [JsonIgnore]
        public List<Content> Contents { get; set; }

        public void SetParentCategory(ICategory category) => this.ParentCategory = (Category)category;

        public ICategory GetParentCategory() => this.ParentCategory;

        public List<IContent> GetContentList() => this.Contents.Select(p => (IContent)p).ToList();

    }
}
