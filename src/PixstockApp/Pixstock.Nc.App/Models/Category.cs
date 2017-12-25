using System.Collections.Generic;
using Pixstock.Base.Infra.Model;

namespace Pixstock.Nc.App.Models
{
    public class Category : ICategory
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public List<Content> Contents { get; set; }
    }
}