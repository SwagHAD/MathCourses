using AutoMapper;

namespace Application.Mapping.Base
{
    public interface IMapWith<T>
    {
        void Mapping(Profile profile)
            => profile.CreateMap(typeof(T), GetType());
    }
}
