namespace LogAnalysisApp.Models
{
    public class LogAnalysisResult
    {
        public int LoginCount { get; set; }
        public int LogoutCount { get; set; }
        public int DispositionCount { get; set; }
        public int BasicLicensesCount { get; set; }
        public int TelephonyLicensesCount { get; set; }
        public int DialerLicensesCount { get; set; }
        public int RecordingLicensesCount { get; set; }
        public int EmailLicensesCount { get; set; }
    }
}
