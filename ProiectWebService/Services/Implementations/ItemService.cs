using AutoMapper;
using ProiectWebData.Entities;
using ProiectWebData.Repositories.Interface;
using ProiectWebService.Dtos;
using ProiectWebService.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProiectWebService.Services.Implementations
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemsRepository _itemsRepository;

        public ItemService(IMapper mapper, IItemsRepository itemsRepository)
        {
            _mapper = mapper;
            _itemsRepository = itemsRepository;
        }

        public async Task<ServiceResponse<List<ItemsDto>>> GetAllItems()
        {
            var response = new ServiceResponse<List<ItemsDto>>();
            var result = await _itemsRepository.GetAllItems();
            if (result.Success)
            {
                response.Data = _mapper.Map<List<Items>, List<ItemsDto>>(result.Data);
            }
            else
            {
                response.Success = false;
                response.Message = "Bad Request";
            }

            return response;
        }
    }
}
