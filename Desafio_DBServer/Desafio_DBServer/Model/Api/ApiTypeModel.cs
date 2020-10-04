using System.Collections.Generic;

namespace Desafio_DBServer.Model.Api
{
    public class ApiTypeModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<Pokemon> pokemon { get; set; }
    }

    public class Pokemon
    {
        public SimplePokemonModel pokemon { get; set; }
        public int slot { get; set; }
    }
}
