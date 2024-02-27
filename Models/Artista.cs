using System;
using System.Collections.Generic;

#nullable disable

namespace ArtistasPraiaGrande.Models
{
    public partial class Artista
    {
        public int IdArtista { get; set; }
        public string NomeCompleto { get; set; }
        public string NomeSocial { get; set; }
        public string NomeArtistico { get; set; }
        public byte Ativo { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
