namespace SondaMQ.AdministradorBus
{
    public interface ContratoSuscriptor
    {
        string Mensaje { get; set; }
        string Usuario { get; set; }
        bool IsOk { get; set; }
        string filtro { get; set; }
    }
}
