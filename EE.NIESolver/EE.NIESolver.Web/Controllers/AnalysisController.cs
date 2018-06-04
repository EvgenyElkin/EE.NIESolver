using System.Linq;
using EE.NIESolver.DataLayer.Constants;
using EE.NIESolver.DataLayer.Entities.Solver;
using EE.NIESolver.DataLayer.Repositories;
using Microsoft.AspNetCore.Mvc;


namespace EE.NIESolver.Web.Controllers
{
    public class AnalysisController : ControllerBase
    {
        private readonly IDataRepository _repository;

        public AnalysisController(IDataRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public JsonResult SetupErrorBasedReport(int experimentId)
        {
            var runs = _repository.Query<DbExperimentResult>()
                    .Where(x => x.ExperimentId == experimentId)
                    //.GroupBy(x => new {x.Parameters, x.RunnerType.Name})
                    .AsEnumerable()
                    .Select(x => new
                    {
                        x.Id,
                        Label = string.Join(", ", x.Parameters.Select(p => $"{p.Code}: {p.Value}")),
                        RunnerName = x.RunnerType.Name,
                    })
                    .ToArray();
            return runs.Any()
                ? JsonResult(true, runs)
                : JsonResult(false);
        }
    }
}
