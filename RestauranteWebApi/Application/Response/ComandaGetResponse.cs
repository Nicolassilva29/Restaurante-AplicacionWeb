namespace Application.Response
{
    public class ComandaGetResponse
    {
        public Guid Id { get; set; }
        public List<MercaderiaGetResponse> Mercaderias { get; set; }
        public FormaEntregaResponse FormaEntrega { get; set;}
        public double Total { get; set; }
        public string Fecha { get; set; }

    }
}
