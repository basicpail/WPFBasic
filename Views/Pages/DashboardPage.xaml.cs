using System.ComponentModel;
using System.Windows.Media;
using Wpf.Ui.Abstractions.Controls;
using WPFBasic.ViewModels.Pages;

/**
 * code behind file
 * view는 viewmodel과 1:1로 매칭된다.
 * DashboardPage 생성자를 보면 DashboardViewModel을 가져와서 프로퍼티에 저절로 셋팅이 되도록 되어있다.
 * 그래서 페이지에서는 vieModel에 대한 즉, DashboardViewModel에 대한 데이터를 바인딩 하고 싶을때, 그냥 viewModel이라는 매개변수를 넘겨서 사용하면 된다.
 * Text="{Binding ViewModel.Counter, Mode=OneWay}" 여기서 ViewModel.~ 가 생성자의 viewModel 로(매개변수로)들어가는 것이다.
 * 이 가져온 viewModel을 조작하면 내가 원하는 동작을 만들 수 있다.
 * 
 * OneWay는 view만 전달하는것, 내가 갑이고 viewModel이 을이다.
 * 단순한 버튼클릭 같은건 view에서 눌려졌다는걸 viewModel에 알려주는 것이기 때문에 OneWay로 충분하다. 쌍방향으로 통신할게 없다.
 * 하지만 입력된 텍스트를 viewModel에 전달하고, viewModel에서 변경된 텍스트를 다시 view로 전달하고 싶다면 TwoWay로 해야한다.
 * 
 * viewModel에서 어떤 프로퍼티가 변경되었을 때, view에 알려주고 싶다면 PropertyChanged 이벤트를 사용한다.
 * St는 viewModel에서 ObservableProperty로 선언되어 있기 때문에, St가 변경되면 PropertyChanged 이벤트가 자동으로 발생한다.
 * 
 * UI변경과 관련된 것만 코드바하인드에 들어가야한다.
 * 
 * 이런식으로 viewModel과 code behind를 연결하고 view의 로직까지 처리할 수 있다는 것을 알아두어라.
  */
namespace WPFBasic.Views.Pages
{
    public partial class DashboardPage : INavigableView<DashboardViewModel>
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage(DashboardViewModel viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            viewModel.PropertyChanged += viewModel_PropertyChanged;

            InitializeComponent();
        }

        private void viewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Text":
                    this.btnClickMe.Background = new SolidColorBrush(Colors.LightGreen); ;
                    break;
            }
            //throw new NotImplementedException();
        }
    }
}
