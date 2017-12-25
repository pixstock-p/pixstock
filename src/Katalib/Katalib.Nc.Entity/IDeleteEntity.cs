namespace Katalib.Nc.Entity
{
    public interface IDeleteEntity
    {
         void OnDelete(KatalibDbContext context);
    }
}