using ANSYS.Domain.Abstractions.Entities;

namespace ANSYS.Infrastructure.Context.Local
{
    public class DBContextLocal<T>
        where T : Entity
    {
        private IList<T> Database;

        public DBContextLocal()
        {
            Database = new List<T>();
        }

        public IEnumerable<T> GetData()
        {
            return Database;
        }

        public T? GetById(int id)
        {
            return Database.FirstOrDefault(e => e.Id!.Equals(id));
        }

        public bool SetData(T entity)
        {
            AutoIncrementId(entity);
            Database.Add(entity);
            return true;
        }

        public bool UpdateData(T entity)
        {
            this.RemoveData(entity.Id);
            this.SetData(entity);

            return true;
        }

        public bool RemoveData(int id)
        {
            var dataEntity = GetById(id);
            Database.ToList().Remove(dataEntity!);

            return true;
        }

        public bool AutoIncrementId(T entity)
        {
            //identificar o tipo
            var lasInsert = GetData().OrderBy(e => e.Id).LastOrDefault();

            if (lasInsert != null)
                entity.Id = lasInsert.Id + 1;
            else
                entity.Id = 1;

            return true;
        }
    }
}
