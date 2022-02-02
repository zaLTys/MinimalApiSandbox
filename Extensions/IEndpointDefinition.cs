﻿namespace MinimalApiSandbox.Extensions
{
    public interface IEndpointDefinition
    {
        void DefineServices(IServiceCollection services);
        void DefineEndpoints(WebApplication app);
    }
}
