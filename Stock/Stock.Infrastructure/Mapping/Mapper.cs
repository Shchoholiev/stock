using AutoMapper;
using Stock.Application.DTOs;
using Stock.Core.Entities;

namespace Stock.Infrastructure.Mapping
{
    public class Mapper
    {
        private readonly IMapper _mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Item, ItemDTO>();
        }).CreateMapper();

        public IEnumerable<ItemDTO> Map(IEnumerable<Item> items)
        {
            return this._mapper.Map<IEnumerable<ItemDTO>>(items);
        }
    }
}
