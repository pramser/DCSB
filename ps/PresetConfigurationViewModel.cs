using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ps.Models;
using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace ps.ViewModels
{
    public class PresetConfigurationViewModel : ObservableObject
    {
        public PresetConfigurationViewModel(ApplicationStateModel applicationStateModel, ConfigurationModel configurationModel)
        {
            LeftPresetViewModel = new PresetViewModel(applicationStateModel, configurationModel);
            RightPresetViewModel = new PresetViewModel(applicationStateModel, configurationModel);
        }

        public PresetViewModel LeftPresetViewModel { get; }
        public PresetViewModel RightPresetViewModel { get; }

        public ICommand CopySoundsRightCommand
        {
            get { return new RelayCommand(CopySoundsRight); }
        }
        private void CopySoundsRight()
        {
            if (RightPresetViewModel.SelectedPreset != null)
                CopyItemsToList(LeftPresetViewModel.SelectedSounds, RightPresetViewModel.SelectedPreset.SoundCollection);
        }

        public ICommand CopySoundsLeftCommand
        {
            get { return new RelayCommand(CopySoundsLeft); }
        }
        private void CopySoundsLeft()
        {
            if (LeftPresetViewModel.SelectedPreset != null)
                CopyItemsToList(RightPresetViewModel.SelectedSounds, LeftPresetViewModel.SelectedPreset.SoundCollection);
        }

        private void CopyItemsToList<T>(IEnumerable<T> items, IList<T> list)
        {
            foreach (ICloneable item in items)
            {
                list.Add((T)item.Clone());
            }
        }
    }
}
