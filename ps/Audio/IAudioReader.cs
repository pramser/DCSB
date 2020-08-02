using NAudio.Wave;

namespace ps.Audio
{
    public interface IAudioReader
    {
        WaveFormat WaveFormat { get; }
        long Position { get; set; }

        int Read(float[] buffer, int offset, int count);
        void Dispose();
    }
}
