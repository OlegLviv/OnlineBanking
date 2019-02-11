using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnlineBanking.Filters.ResourceFilters
{
    public class ModelStateFilter : ResultFilterAttribute
    {
        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var sb = new StringBuilder(context.ModelState.ErrorCount);

                context
                    .ModelState
                    .Values
                    .SelectMany(x => x.Errors, (entry, error) => error.ErrorMessage)
                    .ToList()
                    .ForEach(er => sb.Append(er));

                context.Result = new BadRequestObjectResult(sb.ToString());
            }

            await next();
        }
    }
}
