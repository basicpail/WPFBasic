using WPFBasic.Interfaces;
using WPFBasic.Models;

namespace WPFBasic.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IDateTime _iDateTime;
        private readonly IDatabase<GangnamguPopulation> _iDatabase;


        [ObservableProperty]
        private string? text = string.Empty;


        [ObservableProperty]
        private int _counter = 0;

        [ObservableProperty]
        private string? currentTime = string.Empty;
        //ctor 생성자 생성 쇼트키
        public DashboardViewModel(IDateTime dateTime, IDatabase<GangnamguPopulation> database)
        {
            this._iDateTime = dateTime;
            this._iDatabase = database;
        }

        [RelayCommand]
        private void OnCounterIncrement()
        {
            Counter++;
            this.Text = "Clicked!!";
        }

        [RelayCommand]
        private void onTextChanged()
        {
            var data = this._iDatabase.Get();
            Console.WriteLine("onTextChanged!!");
        }

        [RelayCommand]
        private void onClickShowCurrentTime()
        {
            this.CurrentTime = this._iDateTime.GetCurrentTime().ToString();
        }
    }
}
