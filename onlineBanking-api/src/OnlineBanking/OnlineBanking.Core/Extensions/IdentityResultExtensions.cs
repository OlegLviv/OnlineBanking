using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace OnlineBanking.Core.Extensions
{
    public static class IdentityResultExtensions
    {
        public static string CreateErrorsString(this IdentityResult identityResult)
        {
            var stringBuilder = new StringBuilder(identityResult.Errors.Count());

            foreach (var identityResultError in identityResult.Errors)
            {
                stringBuilder.Append($"{identityResultError}\n");
            }

            return stringBuilder.ToString();
        }
    }
}
