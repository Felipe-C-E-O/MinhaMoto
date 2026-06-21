using MinhaMoto.Models;
using MinhaMoto.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MinhaMoto.ViewModel
{
    [QueryProperty(nameof(IdServico),"Id")]
    public class RegistrarMaterialViewModel : INotifyPropertyChanged
    {

        
        private int _idServico;
        public int IdServico
        {
            get => _idServico;
            set 
                { 
                    DefinirPropriedade(ref _idServico, value);
                    LabelText = $"Ordem de Servico : {value}";
            }
        }
        private string _labelTex;
        public string LabelText
        {
            get => _labelTex;
            set => DefinirPropriedade(ref _labelTex, value);
        }
        private string _nome;
        public string Nome
        {
            get => _nome;
            set => DefinirPropriedade(ref _nome, value);
        }
        private int? _quantidade;
        public int? Quantidade
        {
            get => _quantidade;
            set => DefinirPropriedade(ref _quantidade, value);
        }
        public ObservableCollection<MaterialServico> ListaDeMaterial { get; set; } = new();

        private MaterialServicoService _materialService;
        public RegistrarMaterialViewModel(MaterialServicoService materialService)
        {
            _materialService = materialService;
            SalvarCommand = new Command(async () => await Salvar());
            AdicionarCommand = new Command(async () => await Adicionar());
            FecharCommand = new Command(async () => await Fechar());
        }

        #region Commands
        public ICommand SalvarCommand { get; }
        public ICommand AdicionarCommand { get; }
        public ICommand FecharCommand { get; }
        #endregion
        #region Metodos
        public async Task Fechar()
        {
            await Shell.Current.GoToAsync("..");
        }
        public async Task Salvar()
        {
            if (ListaDeMaterial == null) return;
            await _materialService.SalvarTodos(ListaDeMaterial.ToList());
            await Shell.Current.GoToAsync("..");

        }
        public async Task Adicionar()
        {
            if(IdServico == 0)
            {
                await Shell.Current.DisplayAlert("Erro", "ID_Serviço Zerado", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(Nome))
            {
                await Shell.Current.DisplayAlert("Erro", "por favor Preencha o Nome", "OK");
                return;
            }
            if (Quantidade == 0)
            {
                await Shell.Current.DisplayAlert("Erro", "A quantidade tem que ser maior que 0", "OK");
                return;
            }

            var material = new MaterialServico { IdServico = IdServico, Nome = Nome, Quantidade = Quantidade?? 0 };
            ListaDeMaterial.Add(material);
            Nome = "";
            Quantidade = null;
        }
        #endregion


        #region INotifyProperty
        public event PropertyChangedEventHandler? PropertyChanged;
        public void DefinirPropriedade<T>(ref T CampoPrivado ,T Value , [CallerMemberName] string? Propriedade = null)
        {
            if (Equals(CampoPrivado, Value)) return;
            CampoPrivado = Value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propriedade));
        }
        #endregion
    }
}
