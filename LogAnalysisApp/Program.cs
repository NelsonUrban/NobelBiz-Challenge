using System.Text.RegularExpressions;
using LogAnalysisApp.Models;

class Program
{
    static async Task Main()
    {
        try
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files\\EvoServer01.LOG");
            var analisysResult = await ProcessLogAnalisysAsync(filePath);

            // Print Results
            Console.WriteLine($"Login count: =>  {analisysResult.LoginCount}");
            Console.WriteLine($"License are being used By Type : => Basic Licenses: {analisysResult.BasicLicensesCount}, Telephony Licenses: {analisysResult.TelephonyLicensesCount}, Dialer Licenses: {analisysResult.DialerLicensesCount}, Recording Licenses: {analisysResult.RecordingLicensesCount}, Email Licenses: {analisysResult.EmailLicensesCount}");
            Console.WriteLine($"Logout count: => {analisysResult.LogoutCount}");
            Console.WriteLine($"Disposition of transaction, or where the idFinal was set to count: => {analisysResult.DispositionCount}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static async Task<LogAnalysisResult> ProcessLogAnalisysAsync(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentNullException(nameof(filePath));

        LogAnalysisResult analisysResult = new();
        string[] lines = await GetFileLinesAsync(filePath);

        Parallel.ForEach(lines, line =>
        {
            switch (line)
            {
                case var _ when line.Contains("Login"):
                    analisysResult.LoginCount++;
                    break;

                case var _ when line.Contains("Detailed Agent Licenses"):
                    Match basicMatch = Regex.Match(line, @"Basicas: (\d+)");
                    Match telephonyMatch = Regex.Match(line, @"Telefonia: (\d+)");
                    Match dialerMatch = Regex.Match(line, @"Marcador: (\d+)");
                    Match recordingMatch = Regex.Match(line, @"Grabacion: (\d+)");
                    Match emailMatch = Regex.Match(line, @"Email: (\d+)");

                    if (basicMatch.Success) analisysResult.BasicLicensesCount += int.Parse(basicMatch.Groups[1].Value);
                    if (telephonyMatch.Success) analisysResult.TelephonyLicensesCount += int.Parse(telephonyMatch.Groups[1].Value);
                    if (dialerMatch.Success) analisysResult.DialerLicensesCount += int.Parse(dialerMatch.Groups[1].Value);
                    if (recordingMatch.Success) analisysResult.RecordingLicensesCount += int.Parse(recordingMatch.Groups[1].Value);
                    if (emailMatch.Success) analisysResult.EmailLicensesCount += int.Parse(emailMatch.Groups[1].Value);
                    break;

                case var _ when line.Contains("El cliente ha cerrado la conexi"):
                    analisysResult.LogoutCount++;
                    break;

                case var _ when line.Contains("idFinal"):
                    analisysResult.DispositionCount++;
                    break;

                default:
                    break;
            }
        });

        return analisysResult;
    }

    private static async Task<string[]> GetFileLinesAsync(string path)
    {
        return await File.ReadAllLinesAsync(path);
    }
}