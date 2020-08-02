using NAudio.Vorbis;

namespace ps.Audio
{
    public class OggFileReader : VorbisWaveReader, IAudioReader
    {
        public OggFileReader(string fileName) : base(fileName) { }
    }
}
