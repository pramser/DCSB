using NAudio.Wave;

namespace ps.SoundPlayer
{
    internal class FileReader : AudioFileReader, IAudioReader
    {
        public FileReader(string fileName) : base(fileName) { }
    }
}
