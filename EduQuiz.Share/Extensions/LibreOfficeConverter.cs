using System.Diagnostics;

namespace EduQuiz.Share.Extensions
{

    public static class LibreOfficeConverter
    {
        public static void ConvertDocxToPdf(string docxPath, string outputDir)
        {
            if (!File.Exists(docxPath))
                throw new FileNotFoundException(docxPath);

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Program Files\\LibreOffice\\program\\soffice.exe",
                    Arguments =
                        $"--headless --nologo --nofirststartwizard " +
                        $"--convert-to pdf \"{docxPath}\" --outdir \"{outputDir}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                var error = process.StandardError.ReadToEnd();
                throw new Exception($"LibreOffice conversion failed: {error}");
            }
        }
    }
}
