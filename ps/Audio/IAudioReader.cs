using NAudio.Wave;
using System;

namespace ps.Audio
{
    public interface IAudioReader : IDisposable
    {
        WaveFormat WaveFormat { get; }
        long Position { get; set; }

        int Read(float[] buffer, int offset, int count);
    }
}
