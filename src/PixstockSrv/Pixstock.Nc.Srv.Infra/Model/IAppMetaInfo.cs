namespace Pixstock.Nc.Srv.Infra.Model
{
    public interface IAppMetaInfo
    {
        long Id { get; set; }

        string Key { get; set; }

        string Value { get; set; }
    }
}