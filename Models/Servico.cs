using SQLite;

namespace MinhaMoto.Models
{
    public class Servico
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        [Indexed]
        public int IdVeiculo { get; set; }

    }
}
