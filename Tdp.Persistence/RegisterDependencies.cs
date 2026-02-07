using Microsoft.Extensions.DependencyInjection;
using Tdp.Application.Contracts.Application;
using Tdp.Application.Contracts.Persistence;
using Tdp.Persistence.Data;
using Tdp.Persistence.Repositories;

namespace Tdp.Persistence
{
    public static class RegisterDependencies
    {
        public static IServiceCollection AddPersistanceDependencies(this IServiceCollection services)
        => services.AddSingleton<Database>()
                   .AddScoped<IBoardRepository, BoardRepository>()
                   .AddScoped<IIssuesRepository, IssuesRepository>()
                   .AddScoped<IProjectRepository, ProjectRepository>()
                   .AddScoped<ISubtaskRepository, SubtaskRepository>()
                   .AddScoped<ITaskRepository, TaskRepository>()
                   .AddScoped<IUserRepository, UserRepository>();

    }
}
