using NAudio.Vorbis;

namespace ps.SoundPlayer
{
    public class OggFileReader : VorbisWaveReader, IAudioReader
    {
        public OggFileReader(string fileName) : base(fileName) { }
    }
}
