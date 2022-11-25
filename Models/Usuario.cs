using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_PROJECT_FP_DAM.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        [Column("ID_USUARIO")]
        public int Id_Usuario { get; set; }
        
        [Column("NOMBRE")]
        public string Nombre { get; set; }

        [Column("APELLIDOS")]
        public string Apellidos { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }

        [Column("PASSWORD")]
        public byte[] Password { get; set; }

        [Column("PROTECT")]
        public string Protect { get; set; }

        [Column("TIPOS")]
        public string Tipo { get; set; }
    }

}
