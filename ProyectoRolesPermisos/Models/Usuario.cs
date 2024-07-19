using System.ComponentModel.DataAnnotations;

namespace ProyectoRolesPermisos.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")] //Campo obligatorio, validacion del lado del servidor
        public string Nombre { get; set; }  //Atributo nombre de mi tabla contacto

        [Required(ErrorMessage = "El campo Correo es obligatorio")] //Campo obligatorio, validacion del lado del servidor
        public string Correo { get; set; }  //Atributo Correo de mi tabla contacto

        [Required(ErrorMessage = "El campo Clave es obligatorio")] //Campo obligatorio, validacion del lado del servidor
        public string Clave { get; set; }  //Atributo Clave de mi tabla contacto

        public string[] Roles { get; set; }
    }
}
