using MinhaMoto.Models;
using MinhaMoto.Pages;
using MinhaMoto.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace MinhaMoto.ViewModel
{
    [QueryProperty(nameof(Veiculo), "IdVeiculo")]
    [QueryProperty(nameof(IdServicoClicado) , "servicoClicado")]
    public class RegistrarServicoViewModel : INotifyPropertyChanged
    {
        private Servico _IdServicoClicado;
        public Servico IdServicoClicado
        {
            get => _IdServicoClicado;
            set
            { 
                DefinirPropriedade(ref _IdServicoClicado, value);
                IdServico = value.Id;
                Descricao = value.Descricao;
                IdVeiculo = value.IdVeiculo;
                Data = value.Data;
                BotaoAtivo = true;

            }

        }
        private string _descricao;
        public string Descricao
        {
            get => _descricao;
            set => DefinirPropriedade(ref _descricao, value);
        }
        private Veiculo _veiculo;

        private DateTime _data;
        public DateTime Data
        {
            get => _data;
            set => DefinirPropriedade(ref _data, value);
        }
        public Veiculo Veiculo
        {
            get => _veiculo;
            set
            {
                DefinirPropriedade(ref _veiculo, value);
                IdVeiculo = value.Id;
            }
        }
        private int _IdVeiculo;
        public int IdVeiculo
        {
            get => _IdVeiculo;
            set => DefinirPropriedade(ref _IdVeiculo, value);
        }

        private int _idServico;
        public int IdServico
        {
            get => _idServico;
            set => DefinirPropriedade(ref _idServico, value);
        }

        private bool _botaoAtivo;
        public bool BotaoAtivo
        {
            get => _botaoAtivo;
            set => DefinirPropriedade(ref _botaoAtivo, value);
        }
       
        public ServicoService _servicoService;
        private MaterialServicoService _materialService;
        public ObservableCollection<MaterialServico> ListaMaterial { get; set; } = new();

        // A tela de Lançar material ja manda direto pro Banco e a de registrar Servico e para ver os materias registrados em determinado Serviço
        public RegistrarServicoViewModel(ServicoService ServicoService ,MaterialServicoService materialService)
        {
            _servicoService = ServicoService;
            _materialService = materialService;
            SalvarServicoCommand = new Command(async () => await SalvarServico());
            GoToCadastrarMaterialCommand = new Command(async () => await GoToCadastrarMaterial());
            ExcluirCommad = new Command(async () => await Excluir());
            Data = DateTime.Now;
            BotaoAtivo = false;
        }

        #region Commads
        public ICommand GoToCadastrarMaterialCommand { get; }
        public ICommand ExcluirCommad { get;  }
        public ICommand SalvarServicoCommand { get; }
        #endregion

        #region Metodos
        public async Task Excluir()
        {
            var resposta = await Shell.Current.DisplayAlert("Confirmação", "Tem certeza que deseja excluir esse Serviço", "Sim", "Não");
            if(resposta)
            {
                await _servicoService.Deletar(new Servico { Id = IdServico, IdVeiculo = IdVeiculo, Descricao = Descricao, Data = Data });
                await _materialService.DeletarTodos(IdServico);
                await Shell.Current.GoToAsync("..");
            }
        }
        public async Task CarregarDados()
        {
            ListaMaterial.Clear();
            var dados = await _materialService.GetAll();
            var dadosFiltrado = dados.Where(x => x.IdServico == IdServico);
            foreach(var item in dadosFiltrado)
            {
                ListaMaterial.Add(item);
            }
        }
        public async Task SalvarServico()
        {
            if(string.IsNullOrWhiteSpace(Descricao)|| IdVeiculo == 0)
            {
                await Shell.Current.DisplayAlert("Erro ao Salvar","Por favor Preencha todos os Campos", "OK");
                return;
            }

            if(IdServico ==0)
            {
                try
                {
                    await _servicoService.Salvar(new Servico { Descricao = Descricao, IdVeiculo = IdVeiculo , Data = Data});
                    await Shell.Current.DisplayAlert("Sucesso", "Serviço salvo com sucesso!", "OK");
                }
                catch(Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro ao Salvar", ex.Message, "OK");
                }
                
            }
            else
            {
                await _servicoService.Salvar(new Servico { Id = IdServico , Descricao = Descricao ,Data = Data, IdVeiculo = IdVeiculo });
                await Shell.Current.DisplayAlert("Sucesso", "Serviço Atualizado com sucesso!", "OK");
            }

            var ultimoid = await _servicoService.GetUltimoId();
            IdServico = ultimoid;
            BotaoAtivo = true;
        }
        public async Task GoToCadastrarMaterial()
        {
            if (IdServico == 0)
            {
                await Shell.Current.DisplayAlert("Atenção", "Você precisa salvar um Serviço primeiro\nPara adicionar Materiais", "ok");
                return;
            }
            
            await Shell.Current.GoToAsync($"{nameof(Tela_RegistrarMeterial)}?Id={IdServico}");
        }
        #endregion

        #region INotifyProperty
        public event PropertyChangedEventHandler? PropertyChanged;
        public void DefinirPropriedade<T>(ref  T CampoPrivado, T Value, [CallerMemberName] string? Propriedade = null)
        {
            if (Equals(CampoPrivado, Value)) return;
            CampoPrivado = Value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(Propriedade));
        }
        #endregion
    }
}
