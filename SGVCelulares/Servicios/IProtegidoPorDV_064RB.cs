namespace Servicio_064RB
{
    public interface IProtegidoPorDV_064RB
    {
        string NombreTabla_064RB { get; }
        List<object> ObtenerRegistrosParaDV_064RB();
        string ObtenerIdParaDV_064RB(object registro);
    }
}
