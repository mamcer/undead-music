using System.Data.Entity;

namespace Undead.Music.Data
{
    public interface IEntities
    {
        DbSet<T> GetSet<T>() where T : class;

        EntityState GetState(object entity);

        void SetModified(object entity);
    }
}