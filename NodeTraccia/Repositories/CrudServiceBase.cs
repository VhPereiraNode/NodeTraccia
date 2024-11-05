namespace NodeTraccia.Repositories
{
    public abstract class CrudServiceBase<TEntity, TDto>
    {
        public abstract TDto Create(TDto dto);
        public abstract TDto Read(int id);
        public abstract List<TDto> Read(string? ricerca =null);
        public abstract TDto Update(int id, TDto dto);
        public abstract bool Delete(int id);
        protected abstract TEntity MapEntity(TDto dto);
        protected abstract TDto MapDto(TEntity entity);
    }
}
