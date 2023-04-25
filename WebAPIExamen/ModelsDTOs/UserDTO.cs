using WebAPIExamen.Models;

namespace WebAPIExamen.ModelsDTOs
{
    public class UserDTO
    {
        public int UsuarioID { get; set; }
        public string UsuarioNombre { get; set; } = null!;
        public string PrimerNombre { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string? Numero { get; set; }
        public string Contrasennia { get; set; } = null!;
        public int contadordemalas { get; set; }
        public string correorespaldo { get; set; } = null!;
        public string? trabajoDescripcion { get; set; }

        public int estadoId { get; set; }
        public int CiudadId { get; set; }
        public int  RoleId { get; set; }

        public string Ciudad { get; set; } = null!;
        public string roles { get; set; } = null!;
        public string estados { get; set; } = null!;


    }


    
}
