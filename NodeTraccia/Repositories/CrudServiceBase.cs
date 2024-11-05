namespace NodeTraccia.Repositories
{
    public abstract class CrudServiceBase<TEntity, TDto>
    {
        public abstract TEntity Create(TDto dto);
        public abstract TEntity Read(int id);
        public abstract List<TEntity> Read(string? ricerca =null);
        public abstract TEntity Update(int id, TDto dto);
        public abstract bool Delete(int id);
        protected abstract TEntity MapEntity(TDto dto);
        protected abstract TDto MapDto(TEntity entity);
    }
}
