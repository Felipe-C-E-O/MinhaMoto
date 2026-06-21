using MinhaMoto.Models;
using MinhaMoto.Repositorio;

namespace MinhaMoto.Services
{
    public class VeiculoServices
    {
        private readonly DataBase _dataBase;
        public VeiculoServices(DataBase database)
        {
            _dataBase = database;
        }

        public async Task<int> Salvar(Veiculo veiculo)
        {
            if (veiculo.Id == 0)
            {
                await _dataBase.Init();
                return await _dataBase._connection.InsertAsync(veiculo);
            }
            else
            {
                await _dataBase.Init();
                return await _dataBase._connection.UpdateAsync(veiculo);
            }
        }

        public async Task<List<Veiculo>> GetAll()
        {
            await _dataBase.Init();
            return await _dataBase._connection.Table<Veiculo>().ToListAsync();
        }

        public async Task<int> DeletarVeiculo(Veiculo veiculo)
        {
            await _dataBase.Init();
            return await _dataBase._connection.DeleteAsync(veiculo);
        }
    }
}
