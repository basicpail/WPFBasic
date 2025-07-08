using System.ComponentModel;
using Wpf.Ui.Abstractions.Controls;
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

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;

            InitializeComponent();
        }

        //ViewModel에서 DB데이터를 불러들이고, 프로퍼티가 변경되었을 때, PropertyChanged이벤트가 발생하고 아래 함수가 호출된다. 이 함수는 view에서 처리할 로직이다.
        //LoadingControl의 Visibility를 조정하여 로딩 중과 완료 후의 UI를 변경한다.
        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AdministrativeAgency":
                    this.searchGridLoadingControl.Visibility = Visibility.Collapsed;
                    this.searchGrid.Visibility = Visibility.Visible;
                    break;

                case "GangnamguPopulations":
                    this.dgGridLoadingControl.Visibility = Visibility.Collapsed;
                    this.dgGrid.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
