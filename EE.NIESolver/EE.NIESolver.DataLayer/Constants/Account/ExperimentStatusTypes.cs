namespace EE.NIESolver.DataLayer.Constants.Account
{
    public enum ExperimentStatusTypes
    {
        /// <summary>
        /// Ожидает запуска
        /// </summary>
        Wait,

        /// <summary>
        /// В процессе
        /// </summary>
        InProcess,
        
        /// <summary>
        /// Ошибка
        /// </summary>
        Error,

        /// <summary>
        /// Завершён
        /// </summary>
        Complete
    }
}