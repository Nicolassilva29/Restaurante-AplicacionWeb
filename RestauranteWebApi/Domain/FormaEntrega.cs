namespace Domain
{
    public class FormaEntrega
    {
        public int FormaEntregaId { get; set; }
        public string Descripcion {get; set; }
        public ICollection<Comanda> Comanda { get; set; }

    }
}
