using AutoMapper;
using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.IRepositories;
using enocaDotNetChallenge.Core.IServices;
using enocaDotNetChallenge.Core.IUnitOfWorks;
using enocaDotNetChallenge.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace enocaDotNetChallenge.Service.Services
{
    public class OrderService : Service<Orders>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Carriers> _carriersRepository;
        private readonly IGenericRepository<CarrierConfigurations> _carrierConfigurationRepository;

        private readonly IMapper _mapper;
        public OrderService(IUnitOfWork unitOfWork, IGenericRepository<Orders> genericRepository, IGenericRepository<Carriers> carriersRepository,
            IGenericRepository<CarrierConfigurations> carrierConfigurationRepository,
            IMapper mapper) : base(unitOfWork, genericRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _carriersRepository = carriersRepository;
            _carrierConfigurationRepository = carrierConfigurationRepository;
        }

        public async Task<CustomResponseDTO<ResponseOrdersDTO>> CreateOrderAsync(RequestOrdersDTO requestOrdersDTO)
        {
            int orderDesi = requestOrdersDTO.OrderDesi;

            var responseOrderDTO = new ResponseOrdersDTO();
            var orderCost = new ResponseCarrierConfigurationsDTO();
            var carriers = _carriersRepository.GetAll();
            var carrierConfigurations = _carrierConfigurationRepository.GetAll();

            var minMaxDesiList = carrierConfigurations.GroupBy(c => c.Id)
                                          .Select(g => new
                                          {
                                              CarrierId = g.Key,
                                              MinDesi = g.Min(c => c.CarrierMinDesi),
                                              MaxDesi = g.Max(c => c.CarrierMaxDesi),
                                              Cost = g.Min(c => c.CarrierCost),
                                          })
                                          .ToList();

            bool isDesiInRange = minMaxDesiList.Any(c => orderDesi >= c.MinDesi && orderDesi <= c.MaxDesi);

            if (isDesiInRange)
            {
                var orderCarrierConfig = minMaxDesiList.OrderBy(c => c.Cost).FirstOrDefault(c => orderDesi >= c.MinDesi && orderDesi <= c.MaxDesi);

                var carrier = carriers.FirstOrDefault(c => c.Id == orderCarrierConfig.CarrierId);
                //var orderCarrier = carrier != null ? _mapper.Map<Carriers>(carrier) : null;
                var carrierConfig = carrierConfigurations.FirstOrDefault(c => c.Id == orderCarrierConfig.CarrierId);
                var orderCarrierCost = carrierConfig.CarrierCost;
                var orderCarrierId = carrierConfig.CarrierId;

                var order = new Orders()
                {
                    CarrierId = orderCarrierId,
                    OrderCarrierCost = orderCarrierCost,
                    OrderDate = DateTime.Now,
                    OrderDesi = orderDesi
                };

                var mapOrder = _mapper.Map<Orders>(order);

                var createdOrder = await AddAsync(order);

                var orderDto = _mapper.Map<ResponseOrdersDTO>(createdOrder);

                return CustomResponseDTO<ResponseOrdersDTO>.Success(201, orderDto, "Order created");

            }
            else
            {
                var minDesi = minMaxDesiList.OrderBy(c => c.MinDesi).FirstOrDefault(c => orderDesi <= c.MinDesi);
                var maxDesi = minMaxDesiList.OrderByDescending(c => c.MaxDesi).FirstOrDefault(c => orderDesi >= c.MaxDesi);
                if (minDesi != null && orderDesi < minDesi.MinDesi)
                {
                    var closestCarrier = carrierConfigurations
                .OrderBy(c => c.CarrierMinDesi)
                .FirstOrDefault();
                    var carrierList = carriers.Where(c => c.Id == closestCarrier.CarrierId).ToList();
                    var minPlusDesiAndClosestCarrier = carrierList.OrderBy(c => c.CarrierPlusDesiCost).FirstOrDefault();
                    var sub = Math.Abs(orderDesi - closestCarrier.CarrierMinDesi);
                    var carrierPlusDesiCost = minPlusDesiAndClosestCarrier.CarrierPlusDesiCost;
                    var closestCarrierCost = closestCarrier.CarrierCost;
                    var extraCost = sub * carrierPlusDesiCost;
                    var cost = extraCost + closestCarrier.CarrierCost;

                    var order = new Orders()
                    {
                        CarrierId = minPlusDesiAndClosestCarrier.Id,
                        OrderCarrierCost = cost,
                        OrderDate = DateTime.Now,
                        OrderDesi = orderDesi
                    };

                    var mapOrder = _mapper.Map<Orders>(order);

                    var createdOrder = await AddAsync(order);

                    var orderDto = _mapper.Map<ResponseOrdersDTO>(createdOrder);

                    return CustomResponseDTO<ResponseOrdersDTO>.Success(201, orderDto, "Order created");
                }
                if (maxDesi != null && orderDesi > maxDesi.MaxDesi)
                {
                    var closestCarrier = carrierConfigurations
                .OrderByDescending(c => c.CarrierMaxDesi)
                .FirstOrDefault();
                    var carrierList = carriers.Where(c => c.Id == closestCarrier.CarrierId).ToList();
                    var minPlusDesiAndClosestCarrier = carrierList.OrderBy(c => c.CarrierPlusDesiCost).FirstOrDefault();
                    var sub = Math.Abs(orderDesi - closestCarrier.CarrierMaxDesi);
                    var carrierPlusDesiCost = minPlusDesiAndClosestCarrier.CarrierPlusDesiCost;
                    var closestCarrierCost = closestCarrier.CarrierCost;
                    var extraCost = sub * carrierPlusDesiCost;
                    var cost = extraCost + closestCarrier.CarrierCost;


                    var order = new Orders()
                    {
                        CarrierId = minPlusDesiAndClosestCarrier.Id,
                        OrderCarrierCost = cost,
                        OrderDate = DateTime.Now,
                        OrderDesi = orderDesi
                    };

                    var mapOrder = _mapper.Map<Orders>(order);

                    var createdOrder = await AddAsync(order);

                    var orderDto = _mapper.Map<ResponseOrdersDTO>(createdOrder);

                    return CustomResponseDTO<ResponseOrdersDTO>.Success(201, orderDto, "Order created");
                }

            }


            return CustomResponseDTO<ResponseOrdersDTO>.Success(201, "Order created");
        }


    }
}
