using AutoMapper;
using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.IServices;
using enocaDotNetChallenge.Core.Models;
using enocaDotNetChallenge.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace enocaDotNetChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrierConfigurationsController : CustomBaseController
    {
        private readonly IService<CarrierConfigurations> _service;
        private readonly IMapper _mapper;

        public CarrierConfigurationsController(IService<CarrierConfigurations> carrierConfigurationsService, IMapper mapper)
        {
            _mapper = mapper;
            _service = carrierConfigurationsService;
        }

        [HttpGet]
        public async Task<IActionResult> AllCarrierConfigurations()
        {
            var carrierConfigrations = await _service.GetAllAsync();
            var carriersConfigurationsDtos = _mapper.Map<List<ResponseCarrierConfigurationsDTO>>(carrierConfigrations.ToList());

            return CreateActionResult(CustomResponseDTO<List<ResponseCarrierConfigurationsDTO>>.Success(200, carriersConfigurationsDtos, "All carrier configurations have listed"));
        }

        [HttpPost]
        public async Task<IActionResult> AddCarrierConfiguration(RequestCarrierConfigurationsDTO requestCarrierConfigurationsDTO)
        {
            var carrierConfiguration = await _service.AddAsync(_mapper.Map<CarrierConfigurations>(requestCarrierConfigurationsDTO));
            var carrierConfigurationDto = _mapper.Map<ResponseCarrierConfigurationsDTO>(carrierConfiguration);
            return CreateActionResult(CustomResponseDTO<ResponseCarrierConfigurationsDTO>.Success(201, carrierConfigurationDto, "Carrier configuration added"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarrierConfiguration(ResponseCarrierConfigurationsDTO carrierConfigurationsDto)
        {
            await _service.UpdateAsync(_mapper.Map<CarrierConfigurations>(carrierConfigurationsDto));
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(200, $"Carrier configuration with id {carrierConfigurationsDto.Id} has been updated"));
        }

        [ServiceFilter(typeof(NotFoundFilter<CarrierConfigurations>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCarrierConfiguration(int id)
        {
            var carrier = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(carrier);
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(200, $"Carrier configuration with id {id} has been removed"));
        }
    }
}

