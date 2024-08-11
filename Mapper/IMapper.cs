namespace api.Mapper
{
    public interface IMapper<D, E>
    {
        public D MapToDto(E value);
        public E MapToEntity(D value);
    }
}