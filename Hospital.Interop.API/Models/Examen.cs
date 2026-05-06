namespace Hospital.Interop.API.Models
{
    public class Examen
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Resultado { get; set; } = string.Empty;
    }
}
