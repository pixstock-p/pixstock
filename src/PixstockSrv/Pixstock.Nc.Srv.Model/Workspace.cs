using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Pixstock.Nc.Srv.Infra.Model;

namespace Pixstock.Nc.Srv.Model
{
    [Table("svp_Workspace")]
    public class Workspace : IWorkspace
    {
        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string PhysicalPath { get; set; }

        public DateTime? LastFullBuildDate { get; set; }

        public string TrimWorekspacePath(string path)
        {
            var escaped = Regex.Escape(this.PhysicalPath);
            Regex re = new Regex("^" + escaped + @"[/\\]*", RegexOptions.Singleline);
            string key = re.Replace(path, "");

            return key;
        }

    }
}