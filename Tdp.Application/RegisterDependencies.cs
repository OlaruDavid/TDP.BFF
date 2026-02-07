using Microsoft.Extensions.DependencyInjection;
using Tdp.Application.Contracts.Application;
using Tdp.Application.Implementations;

namespace Tdp.Application
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        => services.AddScoped<IAuthService, AuthService>()
                   .AddScoped<IBoardService, BoardService>()
                   .AddScoped<IIssuesService, IssuesService>()
                   .AddScoped<IProjectPermissionService, ProjectPermissionService>()
                   .AddScoped<IProjectService, ProjectService>()
                   .AddScoped<ISubtaskService, SubtaskService>()
                   .AddScoped<ITaskService, TaskService>();
    }
}
