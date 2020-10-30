namespace Agenda.Api.Models
{
    public class Agenda
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string persona { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
        public string fecha_inicio { get; set; }
        public string fecha_fin { get; set; }
        public bool status { get; set;}
    }
}
