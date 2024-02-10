
namespace Coddinggurrus.Infrastructure.Helpers
{
    public class CommonHelpers
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetRandomNumber()
        {
            return Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 19);
        }
    }
}
