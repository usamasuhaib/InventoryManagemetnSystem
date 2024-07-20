namespace InventoryManagementAPI.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var tenantId = context.User?.Claims.FirstOrDefault(c => c.Type == "TenantId")?.Value;

            if (tenantId != null)
            {
                context.Items["TenantId"] = tenantId;
            }

            await _next(context);
        }
    }
}
