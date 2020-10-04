using Desafio_DBServer.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;

namespace Desafio_DBServer.Model.Api
{
    public class ApiPokemonModel 
    {
        
        public string Id { get; set; }

        public int Height { get; set; }
        public string HeightString
        {
            get { return (Height / 10m).ToString("0.0") + "m"; }
        }
        public string IdString
        {
            get
            {
                return "Nº" + Convert.ToInt32(Id).ToString("0000");
            }
        }
        public string Name { get; set; }
        public Sprites Sprites { get; set; }
        public List<PokemonType> Types { get; set; }
        public int Weight { get; set; }
        public string WeightString
        {
            get { return (Weight / 10m).ToString("0.0") + "kg"; }
        }
    }

    public class Sprites
    {
        public string back_default { get; set; }
        public object back_female { get; set; }
        public string back_shiny { get; set; }
        public object back_shiny_female { get; set; }
        public string front_default { get; set; }
        public string front_female { get; set; }
        public string front_shiny { get; set; }
        public string front_shiny_female { get; set; }
        public Other other { get; set; }
    }

    public class Other
    {
        public DreamWorld dream_world { get; set; }
        public OfficialArtwork officialArtwork { get; set; }
    }

    public class DreamWorld
    {
        public string front_default { get; set; }
        public object front_female { get; set; }
    }
    public class OfficialArtwork
    {
        public string front_default { get; set; }
    }

    public class PokemonType
    {
        public int slot { get; set; }
        public PokemonType2 type { get; set; }
    }

    public class PokemonType2
    {
        public string name { get; set; }
        public string url { get; set; }
    }
}
