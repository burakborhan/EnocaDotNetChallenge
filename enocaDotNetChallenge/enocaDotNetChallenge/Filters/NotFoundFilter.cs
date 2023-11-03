using enocaDotNetChallenge.Core.DTO_s;
using enocaDotNetChallenge.Core.IServices;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using enocaDotNetChallenge.Core.Models;

namespace enocaDotNetChallenge.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> service;

        public NotFoundFilter(IService<T> service)
        {
            this.service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }
            else
            {
                context.Result = new NotFoundObjectResult(CustomResponseDTO<NoContentDTO>.Fail($"{typeof(T).Name}({id}) not found", 404));
            }
        }
    }
}
