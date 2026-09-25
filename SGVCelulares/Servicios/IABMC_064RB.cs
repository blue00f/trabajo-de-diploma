namespace Servicio_064RB
{
    public interface IABMC_064RB<T>
    {
        void Agregar_064RB(T obj);
        void Borrar_064RB(T obj);
        void Modificar_064RB(T obj);
        List<T> Consultar_064RB();
        T ConsultarPorId_064RB(T obj);
    }
}
