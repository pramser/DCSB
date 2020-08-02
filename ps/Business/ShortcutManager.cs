using ps.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ps.Business
{
    public class ShortcutManager
    {
        private ApplicationStateModel _applicationStateModel;
        private ConfigurationModel _configurationModel;
        private SoundManager _soundManager;

        public ShortcutManager(ApplicationStateModel applicationStateModel, ConfigurationModel configurationModel, SoundManager soundManager)
        {
            _applicationStateModel = applicationStateModel;
            _configurationModel = configurationModel;
            _soundManager = soundManager;
        }

        public void KeyUp(VKey key, List<VKey> pressedKeys)
        {
            if (_applicationStateModel.ModifiedBindable != null)
            {
                _applicationStateModel.ModifiedBindable.Keys.Clear();
                foreach (VKey pressedKey in pressedKeys)
                    _applicationStateModel.ModifiedBindable.Keys.Add(pressedKey);

                _applicationStateModel.BindKeysOpened = false;
                _applicationStateModel.ModifiedBindable = null;
            }
        }

        public void KeyDown(VKey key, List<VKey> pressedKeys)
        {
            Shortcut shortcut = ResolveShortcut(key, pressedKeys, new List<Shortcut>(){
                    _configurationModel.SoundShortcuts.Pause,
                    _configurationModel.SoundShortcuts.Continue,
                    _configurationModel.SoundShortcuts.Stop
                });
            if (shortcut != null && shortcut.Command.CanExecute(null))
            {
                shortcut.Command.Execute(null);
            }
        }

        public void KeyPress(VKey key, List<VKey> pressedKeys)
        {
            Sound sound = ResolveShortcut(key, pressedKeys, _configurationModel.SelectedPreset.SoundCollection.Where(x => x.Files.Count != 0));
            if (sound != null)
            {
                _configurationModel.SelectedPreset.SelectedSound = sound;
                _soundManager.Play(sound);
            }

            Preset preset = ResolveShortcut(key, pressedKeys, _configurationModel.PresetCollection);
            if (preset != null)
            {
                _configurationModel.SelectedPreset = preset;
            }
        }

        private T ResolveShortcut<T>(VKey key, IEnumerable<VKey> pressedKeys, IEnumerable<T> items) where T : IBindable
        {
            return items.Where(x => x.Keys.Contains(key) && x.Keys.All(y => pressedKeys.Contains(y))).OrderBy(x => x.Keys.Count).LastOrDefault();
        }
    }
}
