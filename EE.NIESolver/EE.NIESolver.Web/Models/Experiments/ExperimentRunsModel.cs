namespace EE.NIESolver.Web.Models.Experiments
{
    public class ExperimentRunsModel
    {
        public int ExperimentId { get; set; }
        public int[] RunnerIds { get; set; }
        public ExperimentRunModel[] Runs { get; set; }
    }
}