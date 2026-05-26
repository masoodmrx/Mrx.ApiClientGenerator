using System.Threading.Tasks;
using Mrx.ApiClientGenerator.Core;
using Mrx.ApiClientGenerator.Dart.NetFramework;
using Mrx.ApiClientGenerator.Models;

namespace Mrx.ApiClientGenerator.Helpers
{
    public static class ApiClientGeneratorHelper
    {
        public static async Task<bool> Start(ProfileModel model)
        {
            return await ApiClientGeneratorService.StartAsync(
                model,
                m => DartApiClientGeneratorHelper.StartAsync(m.Url, m.GeneratePath, m.ApiName));
        }
    }
}
