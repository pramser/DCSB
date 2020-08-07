using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ps.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ps.ViewModels
{
    public class PresetViewModel : ObservableObject
    {
        public PresetViewModel(ApplicationStateModel applicationStateModel, ConfigurationModel configurationModel)
        {
            ApplicationStateModel = applicationStateModel;
            ConfigurationModel = configurationModel;

            _selectedSounds = new ObservableCollection<Sound>();
        }

        public ApplicationStateModel ApplicationStateModel { get; }
        public ConfigurationModel ConfigurationModel { get; }

        private Preset _selectedPreset;
        public Preset SelectedPreset
        {
            get { return _selectedPreset; }
            set
            {
                _selectedPreset = value;
                RaisePropertyChanged("SelectedPreset");
            }
        }

        private ObservableCollection<Sound> _selectedSounds;
        public ObservableCollection<Sound> SelectedSounds
        {
            get { return _selectedSounds; }
            set
            {
                _selectedSounds = value;
                RaisePropertyChanged("SelectedSounds");
            }
        }

        public ICommand AddPresetCommand
        {
            get { return new RelayCommand(AddPreset); }
        }
        private void AddPreset()
        {
            Preset preset = new Preset() { Name = "New Preset" };
            ConfigurationModel.PresetCollection.Add(preset);
            SelectedPreset = preset;
        }

        public ICommand RemovePresetCommand
        {
            get { return new RelayCommand(RemovePreset); }
        }
        private void RemovePreset()
        {
            if (SelectedPreset != null)
            {
                if (ConfigurationModel.PresetCollection.Count == 1)
                {
                    AddPreset();
                    ConfigurationModel.PresetCollection.Remove(SelectedPreset);
                }
                else
                {
                    if (SelectedPreset == ConfigurationModel.SelectedPreset)
                    {
                        ConfigurationModel.SelectedPresetIndex = 0;
                    }
                    ConfigurationModel.PresetCollection.Remove(SelectedPreset);
                }
            }
        }

        public ICommand ClonePresetCommand
        {
            get { return new RelayCommand(ClonePreset); }
        }
        private void ClonePreset()
        {
            if (SelectedPreset != null)
            {
                Preset preset = (Preset)SelectedPreset.Clone();
                ConfigurationModel.PresetCollection.Add(preset);
                SelectedPreset = preset;
            }
        }

        public ICommand BindKeysCommand
        {
            get { return new RelayCommand<IBindable>(BindKeys); }
        }
        public void BindKeys(IBindable bindable)
        {
            if (bindable != null)
            {
                ApplicationStateModel.ModifiedBindable = bindable;
                ApplicationStateModel.BindKeysOpened = true;
            }
        }

        public ICommand CancelBindKeysCommand
        {
            get { return new RelayCommand(CancelBindKeys); }
        }
        public void CancelBindKeys()
        {
            ApplicationStateModel.BindKeysOpened = false;
            ApplicationStateModel.ModifiedBindable = null;
        }

        public ICommand ClearKeysCommand
        {
            get { return new RelayCommand(ClearKeys); }
        }
        private void ClearKeys()
        {
            ApplicationStateModel.BindKeysOpened = false;
            ApplicationStateModel.ModifiedBindable.Keys.Clear();
            ApplicationStateModel.ModifiedBindable = null;
        }

        public ICommand AddSoundCommand
        {
            get { return new RelayCommand(AddSound); }
        }
        private void AddSound()
        {
            if (SelectedPreset != null)
            {
                Sound sound = new Sound();
                SelectedPreset.SoundCollection.Add(sound);
                SelectedSounds.Clear();
                SelectedSounds.Add(sound);
                ApplicationStateModel.ModifiedSound = sound;
                ApplicationStateModel.SoundOpened = true;
            }
        }

        public ICommand RemoveSoundsCommand
        {
            get { return new RelayCommand(RemoveSounds); }
        }
        private void RemoveSounds()
        {
            RemoveItemsFromList(SelectedSounds, SelectedPreset.SoundCollection);
        }

        public ICommand OpenSoundCommand
        {
            get { return new RelayCommand(OpenSound); }
        }
        private void OpenSound()
        {
            if (SelectedSounds.Count > 0)
            {
                ApplicationStateModel.ModifiedSound = SelectedSounds[0];
                ApplicationStateModel.SoundOpened = true;
            }
        }

        private void RemoveItemsFromList<T>(IList<T> items, IList<T> list)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                list.Remove(items[i]);
            }
        }
    }
}
