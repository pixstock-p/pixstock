using System;

namespace Pixstock.Nc.Srv.Ext.FullBuild.Model
{
    /// <summary>
    /// フルビルド対象情報エンティティ
    /// </summary>
    public class FullBuildFileInfo
    {
        public long Id { get; set; }

        public string AbsoluteFilePath { get; set; }

        public string CategoryName { get; set; }

        public DateTime? FullBuildDate { get; set; }
    }
}
