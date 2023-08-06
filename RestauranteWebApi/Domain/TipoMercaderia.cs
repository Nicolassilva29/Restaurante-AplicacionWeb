namespace Domain
{
    public class TipoMercaderia 
    {
        public int TipoMercaderiaId { get; set; }
        public string Descripcion { get; set; }
        public ICollection<Mercaderia> Mercaderia { get; set; }
    }
}