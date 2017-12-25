namespace Pixstock.Nc.Srv.Infra.Model
{
    public interface IWorkspace : Pixstock.Base.Infra.Model.IWorkspace
    {
        /// <summary>
        /// 指定した文字列からワークスペースのパス部分を削除した文字列を返します。
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        string TrimWorekspacePath(string path);
    }
}