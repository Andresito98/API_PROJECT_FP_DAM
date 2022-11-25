using API_PROJECT_FP_DAM.DataContext;
using API_PROJECT_FP_DAM.Helpers;
using API_PROJECT_FP_DAM.Models;

namespace API_PROJECT_FP_DAM.Repositories
{
    public class UserRepository
    {
        private UserContext context;

        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        private int GetMaxIdUsuario()
        {
            if (this.context.Usuarios.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Usuarios.Max(z => z.Id_Usuario) + 1;
            }
        }

        private bool ExisteEmail(string email)
        {
            var consulta = from datos in this.context.Usuarios
                           where datos.Email == email
                           select datos;
            if(consulta.Count() > 0 )
            {
                //El email existe en la base de datos
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegistrarUsuario(string email, string password, string nombre, string apellidos, string tipo)
        {
            bool ExisteEmail = this.ExisteEmail(email);
            if (ExisteEmail)
            {
                return false;
            }
            else
            {
                int idusuario = this.GetMaxIdUsuario();
                Usuario usuario = new Usuario();
                usuario.Id_Usuario = idusuario;
                usuario.Email = email;
                usuario.Nombre = nombre;
                usuario.Apellidos = apellidos;
                usuario.Tipo = tipo;
                //GENERAMOS UN SALT ALEATORIO PARA CADA USUARIO
                usuario.Protect = HelperCryptography.GenerateProtect();
                //GENERAMOS SU PASSWORD CON EL SALT
                usuario.Password = HelperCryptography.EncriptarPassword(password, usuario.Protect);
                this.context.Usuarios.Add(usuario);
                this.context.SaveChanges();

                return true;
            }
            
        }

        public Usuario LogInUsuario(string email, string password)
        {
            Usuario usuario = this.context.Usuarios.SingleOrDefault(x => x.Email == email);
            if (usuario == null)
            {
                return null;
            }
            else
            {
                //Debemos comparar con la base de datos el password haciendo de nuevo el cifrado con cada salt de usuario
                byte[] passUsuario = usuario.Password;
                string protect = usuario.Protect;
                //Ciframos de nuevo para comparar
                byte[] temporal = HelperCryptography.EncriptarPassword(password, protect);

                //Comparamos los arrays para comprobar si el cifrado es el mismo
                bool respuesta = HelperCryptography.compareArrays(passUsuario, temporal);
                if(respuesta == true)
                {
                    return usuario;
                }
                else
                {
                    //Contraseña incorrecta
                    return null;
                }
            }
        }

        public List<Usuario> GetUsuarios()
        {
            var consulta = from datos in this.context.Usuarios
                           select datos;
            return consulta.ToList();
        }
    }
}
