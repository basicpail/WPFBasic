using System.ComponentModel;
using System.Windows.Media;
using Wpf.Ui.Abstractions.Controls;
using WPFBasic.ViewModels.Pages;
using Wpf.Ui.Controls;

/**
 * code behind file
 * view는 viewmodel과 1:1로 매칭된다.
 * DashboardPage 생성자를 보면 DashboardViewModel을 가져와서 프로퍼티에 저절로 셋팅이 되도록 되어있다. (Q. 언제 DashboardPage 생성자가 호출 되는 건지?)
 * 그래서 페이지에서는 viewModel에 대한 즉, DashboardViewModel에 대한 데이터를 바인딩 하고 싶을때, 그냥 viewModel이라는 매개변수를 넘겨서 사용하면 된다.
 * Text="{Binding ViewModel.Counter, Mode=OneWay}" 여기서 ViewModel.~ 가 생성자의 viewModel 로(매개변수로)들어가는 것이다. (Q. 그럼 Binding 할 때 마다 생성자가 호출 된다고? 인스턴스가 만들어진다고?)
 * 이 가져온 viewModel을 조작하면 내가 원하는 동작을 만들 수 있다.
 * 
 * OneWay는 viewModel의 값이 view 전달되는 것이고, TwoWay는 view의 값이 바뀌면 viewModel의 값도 바뀌는것이다?
 * 단순한 버튼클릭 같은건 view에서 눌려졌다는걸 viewModel에 알려주는 것이기 때문에 OneWay로 충분하다. 쌍방향으로 통신할게 없다.
 * 하지만 입력된 텍스트를 viewModel에 전달하고, viewModel에서 변경된 텍스트를 다시 view로 전달하고 싶다면 TwoWay로 해야한다.
 * view 로직에서 처리할 때는 code behind에 작성한다. 
 * - (ex viewModel_PropertyChanged 메서드 선언하고, viewModel.PropertyChanged += viewModel_PropertyChanged; 로 처리한다.)
 * - e.PropertyName을 통해서 어떤 프로퍼티가 변경되었는지 확인할 수 있다. (viewMedel에서 처리하고 보내준 값을 view에서 받아서 사용할 수 있다.)
 * 
 * Q) Two Way와 Command의 차이?  - oneWay로 설정해도, viewModel.PropertyChanged 이벤트를 통해서 viewModel에서 변경된 값을 view로 전달할 수 있잖아? 그렇다면 twoWay는 왜 필요한거지?
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

            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            InitializeComponent();
        }

        private void ViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AdministrativeAgency":
                    this.loadingGrid.Visibility = Visibility.Collapsed;
                    this.dashboardGrid.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
