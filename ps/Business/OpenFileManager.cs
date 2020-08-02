using Microsoft.Win32;

namespace ps.Business
{
    public class OpenFileManager
    {
        public string[] OpenSoundFiles()
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = "Choose sound file/s",
                Filter = "sound files (*.wma; *.mp3; *.wav; *.ogg; *.m4a; *.aiff; *.flac)|*.wma;*.mp3;*.wav;*.ogg;*.m4a;*.aiff;*.flac",
                AddExtension = true,
                RestoreDirectory = true,
                Multiselect = true
            };

            bool? result = fileDialog.ShowDialog();

            if (result.HasValue && result.Value)
            {
                return fileDialog.FileNames;
            }
            return null;
        }
    }
}
