namespace Api.Base.Logging
{
    public partial class ActionAudit
    {
        public int LogLevel { get; set; } = (int)Enums.LogLevel.Info;
        public int LogType { get; set; } = (int)Enums.LogType.Request;
        public string LogSource { get; set; }
        public string TraceId { get; set; }
    }
}
