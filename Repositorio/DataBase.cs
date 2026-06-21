using MinhaMoto.Models;
using SQLite;

namespace MinhaMoto.Repositorio
{
    public class DataBase
    {
        public SQLiteAsyncConnection _connection;
        private bool Iniciado = false;
        public DataBase()
        {
            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "AppMinhaMoto.db3"));

        }

        public async Task Init()
        {
            if (Iniciado) return;
            await _connection.CreateTableAsync<Veiculo>();
            await _connection.CreateTableAsync<Veiculo>();
            await _connection.CreateTableAsync<Servico>();
            await _connection.CreateTableAsync<MaterialServico>();
            Iniciado = true;

        }
    }
}
