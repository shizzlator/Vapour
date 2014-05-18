using System.Collections.Generic;
using Vapour.Web.Models;

namespace Vapour.Web.WebDomain
{
    public interface IProjectsViewModelFactory
    {
		IEnumerable<ProjectViewModel> Create();
    }
}