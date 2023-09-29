namespace tl2_tp4_2023_VelizMiguelC;
using System.Linq;

public class Cliente{
    private string nombre;
    private string direccion;
    private long telefono;
    private string datosRefDireccion;
    public string Nombre { get => nombre; set => nombre = value; }
    public string Direccion { get => direccion; set => direccion = value; }
    public long Telefono { get => telefono; set => telefono = value; }
    public string DatosRefDireccion { get => datosRefDireccion; set => datosRefDireccion = value; }
        public Cliente(string nombre, string direccion, long telefono, string datosRefDireccion)
    {
        Nombre = nombre;
        Direccion = direccion;
        Telefono = telefono;
        DatosRefDireccion = datosRefDireccion;
    }
    
}