namespace Application.Response
{
    public class ComandaResponse
    {
        public Guid Id {  get; set; }
        public List<MercaderiaComandaResponse> Mercaderias { get; set; }
        public FormaEntregaResponse FormaEntrega { get; set;}
        public double Total { get; set; }
        public string Fecha { get; set; }
    }
}
