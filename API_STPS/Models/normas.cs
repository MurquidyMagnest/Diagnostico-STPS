namespace API_STPS.Models
{
    public class Normas
    {
        public int Id {  get; set; }
        public string? categoria_noms { get; set; }
        public string? nombre_noms { get; set; }
        public string? descripcion { get; set; }
    }

    public class Incisos_normas
    {
        public int? Id { get; set; }
        public int? id_noms { get; set; }
        public string? inciso_noms { get; set; }
        public string? descripcion { get; set; }
        public string? comprobacion { get; set; }
        public string? criterio_acepton { get; set; }
        public string? observacion { get; set; }


    }
}
