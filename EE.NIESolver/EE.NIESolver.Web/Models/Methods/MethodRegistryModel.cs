namespace EE.NIESolver.Web.Models.Methods
{
    public class MethodRegistryModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MethodType { get; set; }
        public int ExperimentCount { get; set; }
        public bool CanEdit { get; set; }
    }
}