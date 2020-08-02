using NAudio.Wave;

namespace ps.Audio
{
    internal class FileReader : AudioFileReader, IAudioReader
    {
        public FileReader(string fileName) : base(fileName) { }
    }
}
