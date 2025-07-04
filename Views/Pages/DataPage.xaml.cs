﻿using Wpf.Ui.Abstractions.Controls;
using WPFBasic.ViewModels.Pages;

namespace WPFBasic.Views.Pages
{
    public partial class DataPage : INavigableView<DataViewModel>
    {
        public DataViewModel ViewModel { get; }

        public DataPage(DataViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
