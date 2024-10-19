using System.Diagnostics;

namespace CrispyTool.Video2Audio.src
{
    /// <summary>
    /// 音声抽出クラス
    /// author: CrispyCrack
    /// date: 2024/10/19
    /// </summary>
    public static class AudioExtractor
    {
        /// <summary>
        /// 音声を抽出する
        /// </summary>
        /// <param name="inputFilePath">抽出元パス</param>
        /// <param name="outputFilePath">保存先パス</param>
        /// <param name="progress">進行度</param>
        public static void ExtractAudio(string inputFilePath, string outputFilePath, IProgress<int> progress)
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

                // 進行度バー
                for (int i = 0; i <= 100; i += 10)
                {
                    progress.Report(i);
                    Thread.Sleep(500);
                }

                process.WaitForExit();
            }

            MessageBox.Show("Audio extraction completed.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
