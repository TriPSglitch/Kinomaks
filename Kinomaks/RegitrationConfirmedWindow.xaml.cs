﻿using System.Windows;

namespace Kinomaks
{
    /// <summary>
    /// Логика взаимодействия для RegitrationConfirmedWindow.xaml
    /// </summary>
    public partial class RegitrationConfirmedWindow : Window
    {
        public RegitrationConfirmedWindow()
        {
            InitializeComponent();
        }

        private void BackClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}