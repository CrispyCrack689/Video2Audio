using System.Diagnostics;

namespace CrispyTool.Video2Audio.src
{
    /// <summary>
    /// �������o�N���X
    /// author: CrispyCrack
    /// date: 2024/10/19
    /// </summary>
    public static class AudioExtractor
    {
        /// <summary>
        /// �����𒊏o����
        /// </summary>
        /// <param name="inputFilePath">���o���p�X</param>
        /// <param name="outputFilePath">�ۑ���p�X</param>
        public static void ExtractAudio(string inputFilePath, string outputFilePath)
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = "ffmpeg",
                Arguments = $"-i \"{inputFilePath}\" -q:a 0 -map a \"{outputFilePath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = new Process())
            {
                process.StartInfo = startInfo;
                process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);
                process.ErrorDataReceived += (sender, e) => Console.WriteLine(e.Data);

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            }

            MessageBox.Show("Audio extraction completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
