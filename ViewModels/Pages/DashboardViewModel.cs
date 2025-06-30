using WPFBasic.Interfaces;

namespace WPFBasic.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IDateTime _iDateTime;

        [ObservableProperty]
        private string? text = string.Empty;


        [ObservableProperty]
        private int _counter = 0;

        [ObservableProperty]
        private string? currentTime = string.Empty;
        //ctor
        public DashboardViewModel(IDateTime dateTime)
        {
            this._iDateTime = dateTime;
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
            Console.WriteLine("onTextChanged!!");
        }

        [RelayCommand]
        private void onClickShowCurrentTime()
        {
            this.CurrentTime = this._iDateTime.GetCurrentTime().ToString();
        }
    }
}
