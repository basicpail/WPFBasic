using Microsoft.EntityFrameworkCore.Storage;
using System.Windows.Media;
using Wpf.Ui.Abstractions.Controls;
using WPFBasic.Interfaces;
using WPFBasic.Models;

namespace WPFBasic.ViewModels.Pages
{
    /*
     * viewModel에서는 서비스에서 제공하는 함수들을 호출해서 사용한다.
     * 인터페이스를 이용하여 호출.
     * 생성자를 이용해서 생성할 때 주입.
     */
    public partial class DataViewModel : ObservableObject, INavigationAware
    {
        #region Feilds
        private bool _isInitialized = false;

        private readonly IDatabase<GangnamguPopulation> _database;
        #endregion

        #region Properties
        //DB Table의 내용을 DataGrid에 뿌리기 위한것이니까 IEnumerable로 선언하고 데이터타입은 GangnamguPopulation으로 한다.
        [ObservableProperty]
        private IEnumerable<GangnamguPopulation> _gangnamguPopulations;

        [ObservableProperty]
        private IEnumerable<string> _administrativeAgency;

        [ObservableProperty]
        private string? selectedAdministrativeAgency;

        [ObservableProperty]
        private int? selectedTotalPopulation;

        [ObservableProperty]
        private int? selectedMalePopulation;

        [ObservableProperty]
        private int? selectedFemalePopulation;

        [ObservableProperty]
        private double? selectedSexRatio;

        [ObservableProperty]
        private int? selectedNumberOfHouseholds;

        [ObservableProperty]
        private double? selectedNumberOfPeoplePerHousehold;

        [ObservableProperty]
        private int? selectedId;


        #endregion

        #region Constructor
        public DataViewModel(IDatabase<GangnamguPopulation> database)
        {
            this._database = database;
        }
        #endregion

        #region Commands
        [RelayCommand]
        private void OnSelectAdministrativeAgency()
        {
            var selectedData = this.SelectedAdministrativeAgency;
        }

        [RelayCommand]
        private void CreateNewData()
        {
            GangnamguPopulation gangnamguPopulation = new GangnamguPopulation();
            gangnamguPopulation.AdministrativeAgency = this.SelectedAdministrativeAgency;
            gangnamguPopulation.TotalPopulation = this.SelectedTotalPopulation;
            gangnamguPopulation.MalePopulation = this.SelectedMalePopulation;
            gangnamguPopulation.FemalePopulation = this.SelectedFemalePopulation; 
            gangnamguPopulation.SexRatio = this.SelectedSexRatio;
            gangnamguPopulation.NumberOfHouseholds = this.SelectedNumberOfHouseholds;
            gangnamguPopulation.NumberOfPeoplePerHousehold = this.SelectedNumberOfPeoplePerHousehold;

            this._database.Create(gangnamguPopulation);
        }

        [RelayCommand]
        private void ReadAllData()
        {
            this.GangnamguPopulations = this._database?.Get();
        }

        [RelayCommand]
        private void ReadDetailData()
        {
            var Data = this._database.GetDetail(this.SelectedId);
            //값이 변경되면 PropertyChanged 이벤트가 발생하고, UI에 바로바로 반영된다.

            this.SelectedAdministrativeAgency = Data?.AdministrativeAgency;
            this.SelectedTotalPopulation = Data?.TotalPopulation;
            this.SelectedMalePopulation = Data?.MalePopulation;
            this.SelectedFemalePopulation = Data?.FemalePopulation;
            this.SelectedSexRatio = Data?.SexRatio;
            this.SelectedNumberOfHouseholds = Data?.NumberOfHouseholds;
            this.SelectedNumberOfPeoplePerHousehold = Data?.NumberOfPeoplePerHousehold;
        }

        [RelayCommand]
        private void UpdateData()
        {
            var data = this._database.GetDetail(this.SelectedId);

            data.Id = this.SelectedId ?? 0; //ID는 null이 될 수 없으므로, null일 경우 0으로 설정
            data.AdministrativeAgency = this.SelectedAdministrativeAgency;
            data.TotalPopulation = this.SelectedTotalPopulation;
            data.MalePopulation = this.SelectedMalePopulation;
            data.FemalePopulation = this.SelectedFemalePopulation;
            data.SexRatio = this.SelectedSexRatio;
            data.NumberOfHouseholds = this.SelectedNumberOfHouseholds;
            data.NumberOfPeoplePerHousehold = this.SelectedNumberOfPeoplePerHousehold;

            this._database?.Update(data);

        }

        [RelayCommand]
        private void DeleteData()
        {
            this._database?.Delete(this.SelectedId);
        }
        #endregion

        #region Methods 
        public Task OnNavigatedToAsync()
        {
            if (!_isInitialized)
                //InitializeViewModel();
                InitializeViewModelAsync(); //비동기 초기화 메소드 호출

            return Task.CompletedTask;
        }

        public Task OnNavigatedFromAsync() => Task.CompletedTask;

        //private void InitializeViewModel()
        private async Task InitializeViewModelAsync() //async Task로 변경하여 비동기적으로 초기화 가능
        {
            //this.GangnamguPopulations = this._database.Get();
            //비동기로 데이터 가져오기
            this.GangnamguPopulations = await Task.Run(() => this._database?.Get());

            if (this.GangnamguPopulations != null)
            {
                this.AdministrativeAgency = this.GangnamguPopulations?.Select(c => c.AdministrativeAgency).ToList();
            }
            _isInitialized = true;
        }
        #endregion







        
    }
}
