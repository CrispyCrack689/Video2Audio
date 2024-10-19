namespace CrispyTool.Video2Audio.src
{
    /// <summary>
    /// ファイル操作ユーティリティクラス
    /// author: CrispyCrack
    /// date: 2024/10/19
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// ヘッダーのバイナリがMP4形式かどうかチェック
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>MP4ファイルならtrue</returns>
        public static bool IsMp4File(string filePath)
        {
            byte[] mp4Header = [0x66, 0x74, 0x79, 0x70]; // 4~8バイト目が"ftyp"であればMP4ファイル
            var buffer = new byte[4];

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(4, SeekOrigin.Begin); // 最初の4バイトをスキップ
                fs.Read(buffer, 0, 4);
            }

            return buffer.SequenceEqual(mp4Header);
        }
    }
}
