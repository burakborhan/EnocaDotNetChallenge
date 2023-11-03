using AutoMapper;
using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.IServices;
using enocaDotNetChallenge.Core.Models;
using enocaDotNetChallenge.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace enocaDotNetChallenge.Controllers
{
    public class OrdersController : CustomBaseController
    {
        private readonly IService<Orders> _service;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IService<Orders> service, IMapper mapper)
        {
            _orderService = orderService;
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> AllOrders()
        {
            var orders = await _service.GetAllAsync();
            var ordersDtos = _mapper.Map<List<ResponseOrdersDTO>>(orders.ToList());

            return CreateActionResult(CustomResponseDTO<List<ResponseOrdersDTO>>.Success(200, ordersDtos, "All orders have listed"));
        }
        [HttpPost]
        public async Task<IActionResult> AddOrder ( RequestOrdersDTO requestOrdersDTO)
        {
            return CreateActionResult(await _orderService.CreateOrderAsync(requestOrdersDTO));
        }

        [ServiceFilter(typeof(NotFoundFilter<Orders>))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveOrder(int id)
        {
            var order = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(order);
            return CreateActionResult(CustomResponseDTO<NoContentDTO>.Success(200, $"Order with id {id} has been removed"));
        }
    }
}
