﻿using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using DCSB.ViewModels;

namespace DCSB
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            DataContext = new ViewModel();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Windows.MessageBox.Show(e.ExceptionObject.ToString(), "Unhandled exception");
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (DataContext is ViewModel viewModel && viewModel.ConfigurationModel.MinimizeToTray && WindowState == WindowState.Minimized)
                Hide();

            base.OnStateChanged(e);
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ViewModel viewModel)
            {
                viewModel.WindowHandle = new WindowInteropHelper(this).Handle;
            }
        }
    }
}
