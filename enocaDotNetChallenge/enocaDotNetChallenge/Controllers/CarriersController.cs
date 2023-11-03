using AutoMapper;
using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.IServices;
using enocaDotNetChallenge.Core.Models;
using enocaDotNetChallenge.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace enocaDotNetChallenge.Controllers
{

    public class CarriersController : CustomBaseController
    {
        private readonly IService<Carriers> _service;
        private readonly IMapper _mapper;

        public CarriersController(IService<Carriers> carrierService, IMapper mapper)
        {
            _mapper = mapper;
            _service = carrierService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var carriers = await _service.GetAllAsync();
            var carriersDtos = _mapper.Map<List<ResponseCarriersDTO>>(carriers.ToList());

            return CreateActionResult(CustomResponseDTO<List<ResponseCarriersDTO>>.Success(200, carriersDtos, "All carriers have listed"));
        }


        [HttpPost]
        public async Task<IActionResult> AddCarrier(RequestCarriersDTO requestCarriersDTO)
        {
            var carrier = await _service.AddAsync(_mapper.Map<Carriers>(requestCarriersDTO));
            var carriersDto = _mapper.Map<ResponseCarriersDTO>(carrier);
            return CreateActionResult(CustomResponseDTO<ResponseCarriersDTO>.Success(201, carriersDto, "Carrier added"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarrier(ResponseCarriersDTO carrierDto)
        {
            await _service.UpdateAsync(_mapper.Map<Carriers>(carrierDto));
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(200,$"Carrier with id {carrierDto.Id} has been updated"));
        }

        [ServiceFilter(typeof(NotFoundFilter<Carriers>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCarrier(int id)
        {
            var carrier = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(carrier);
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(200, $"Carrier with id {id} has been removed"));
        }
    }
}
    
