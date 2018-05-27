using System.Linq;
using EE.NIESolver.DataLayer.Entities.Solver;
using Microsoft.EntityFrameworkCore;

namespace EE.NIESolver.DataLayer.QueryHelpers
{
    public static class ExperimentQueryHelper
    {
        public static IQueryable<DbExperiment> WithAllProperties(this IQueryable<DbExperiment> query) => query
                .Include(x => x.Method)
                .ThenInclude(x => x.MethodType)
                .Include(x => x.Values)
                .ThenInclude(x => x.Parameter)
                .ThenInclude(x => x.ParameterType)
                .Include(x => x.Results);
    }
}
