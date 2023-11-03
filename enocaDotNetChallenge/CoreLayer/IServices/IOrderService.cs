using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace enocaDotNetChallenge.Core.IServices
{
    public interface IOrderService : IService<Orders>
    {
        Task<CustomResponseDTO<ResponseOrdersDTO>> CreateOrderAsync(RequestOrdersDTO requestOrdersDTO);
    }
}
