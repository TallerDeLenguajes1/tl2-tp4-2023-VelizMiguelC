namespace tl2_tp4_2023_VelizMiguelC;
using System.Linq;
public class Cadete{
    private int id;
    private string nombre;
    private string direccion;
    private long telefono;
    public int Id { get => id; set => id = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public long Telefono { get => telefono; set => telefono = value; }
    public Cadete(int id, string nombre, string direccion, long telefono)
    {
        Id = id;
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
    }
    public int CantidadDePedidos(List<Pedido> ListaP,int op){
        int suma=0;
        switch (op)
        {
            case 1:
            suma = ListaP.Count(p=> p.Estado==Estado.Entregado && p.IdCadete==Id);
            break;
            case 2:
            suma = ListaP.Count(p=> p.Estado==Estado.SinEntregar && p.IdCadete==Id);
            break;
            case 3:
            suma = ListaP.Count(p=> p.Estado==Estado.Cancelado && p.IdCadete==Id);
            break;
            default:
            suma = ListaP.Count();
            break;
        }
        return suma;
    }
    public float JornalACobrar(List<Pedido> ListaP){
        float Sueldo=0;
        Sueldo = CantidadDePedidos(ListaP,1)*500;
        return Sueldo;
    }
}