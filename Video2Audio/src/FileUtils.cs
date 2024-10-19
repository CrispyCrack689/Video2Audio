namespace CrispyTool.Video2Audio.src
{
    /// <summary>
    /// �t�@�C�����샆�[�e�B���e�B�N���X
    /// author: CrispyCrack
    /// date: 2024/10/19
    /// </summary>
    public static class FileUtils
    {
        /// <summary>
        /// �w�b�_�[�̃o�C�i����MP4�`�����ǂ����`�F�b�N
        /// </summary>
        /// <param name="filePath">�t�@�C���p�X</param>
        /// <returns>MP4�t�@�C���Ȃ�true</returns>
        public static bool IsMp4File(string filePath)
        {
            byte[] mp4Header = [0x66, 0x74, 0x79, 0x70]; // 4~8�o�C�g�ڂ�"ftyp"�ł����MP4�t�@�C��
            var buffer = new byte[4];

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                fs.Seek(4, SeekOrigin.Begin); // �ŏ���4�o�C�g���X�L�b�v
                fs.Read(buffer, 0, 4);
            }

            return buffer.SequenceEqual(mp4Header);
        }
    }
}
