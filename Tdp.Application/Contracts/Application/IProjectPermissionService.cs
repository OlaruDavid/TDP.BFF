using System;
using System.Collections.Generic;
using System.Text;

namespace Tdp.Application.Contracts.Application
{
    public interface IProjectPermissionService
    {
        Task<bool> IsOwner(Guid userId, Guid projectId);
    }
}
