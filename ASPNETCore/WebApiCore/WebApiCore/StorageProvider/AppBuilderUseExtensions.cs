using WebApi;

namespace Microsoft.AspNetCore.Builder
{
    public static class AppBuilderUseExtensions
    {
        public static IApplicationBuilder AddOneDriveStorage(this IApplicationBuilder app, string key, string clientId, string scope, string username, string password)
        {
            return app;
        }
    }
}