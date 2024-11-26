using NodeTraccia.Models;

namespace NodeTraccia.Repositories
{
    public abstract class CrudServiceBase<TEntity, TDto> where TEntity : BaseEntity
    {
        protected List<TEntity> _entities;
        public  TEntity Create(TDto dto)
        {
            var t = MapEntity(dto);
             _entities.Add(t);
            return Read(t.Id);
        }
        public TEntity Read(int id) 
        {  
            return _entities.FirstOrDefault(x=>x.Id.Equals(id)); 
        }
        public abstract List<TEntity> Read(string? ricerca =null);
        public abstract TEntity Update(int id, TDto dto);
        public abstract bool Delete(int id);
        protected abstract TEntity MapEntity(TDto dto);
        protected abstract TDto MapDto(TEntity entity);
    }
}
