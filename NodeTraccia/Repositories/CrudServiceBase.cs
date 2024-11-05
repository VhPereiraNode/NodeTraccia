using NodeTraccia.Models;

namespace NodeTraccia.Repositories
{
    public abstract class CrudServiceBase<TEntity, TDto> where TEntity : BaseEntity

    {
             
        public virtual TEntity Create(TDto dto)
        {
            var entity = MapEntity(dto);
            entity = AddEntity(entity);
            return Read(entity.Id);
        }

        public TEntity? Read(int id)
        {
            return GetEntityById(id);
        }

        public List<TEntity> Read(string? ricerca = null)
        {
            if (ricerca != null) {

                return GetByString(ricerca);
            }
           return GetAll();
        }
        public TEntity Update(int id, TDto dto)
        {
            var entity = GetEntityById(id);
            if (entity is not null)
            {
                UpdateEntity(entity, dto);
            }
            return Read(entity.Id);
        }

        public bool Delete(int id)
        {
            var entity = GetEntityById(id);
            if (entity.Id > 0)
            {
                Remove(entity.Id);
                entity = GetEntityById(id);
                if (entity is null)
                {
                    return true;
                }
                return false;
            }
            return false;
            
        }
        protected abstract TEntity AddEntity (TEntity entity);
        protected abstract TEntity? GetEntityById (int id);
        protected abstract void Remove (int id);
        protected abstract TEntity UpdateEntity (TEntity entity, TDto dto);
        protected abstract List<TEntity> GetByString (string ricerca);
        protected abstract List<TEntity> GetAll();
        protected abstract TEntity MapEntity(TDto dto);
        protected abstract TDto MapDto(TEntity entity);
    }
}
