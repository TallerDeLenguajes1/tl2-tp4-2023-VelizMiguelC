namespace tl2_tp4_2023_VelizMiguelC;
using System.Linq;
using Microsoft.VisualBasic;

public enum Estado{
    Entregado,
    SinEntregar,
    Cancelado,
}
public class Cadeteria{
    private string nombre;
    private long telefono;
    private static Cadeteria instance;
    private AccesoADatosCadeteria accesoADatosCadeteria;
    private AccesoADatosCadetes accesoADatosCadetes;
    private AccesoADatosPedidos accesoADatosPedidos;
    private Cadeteria()
    {
        // Inicializa las propiedades si es necesario.
        accesoADatosCadeteria = new AccesoADatosCadeteria();
        accesoADatosCadetes = new AccesoADatosCadetes();
        accesoADatosPedidos = new AccesoADatosPedidos();
        instance = accesoADatosCadeteria.Obetener();
    }
        public static Cadeteria Instance
    {
        get
        {
            // Crear la instancia Cadeteria si aún no existe.
            if (instance == null)
            {
                instance = new Cadeteria();
            }
            return instance;
        }
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public long Telefono { get => telefono; set => telefono = value; }
    public Cadeteria(string nombre, long telefono)
    {
        Nombre = nombre;
        Telefono = telefono;    
    }
    public Informe GetInforme(){
        var cad = accesoADatosCadetes.Obetener();
        var ped = accesoADatosPedidos.Obetener();
        return new Informe(cad,ped);
    }   
    public List<Pedido> GetPedidos(){
        return accesoADatosPedidos.Obetener();
    }
    public List<Cadete> GetCadetes(){
        return accesoADatosCadetes.Obetener();
    }
    public Pedido TomarPedido(string nombre, string direccion, long telefono, string datosRef,  string observacion) {
        var cliente = new Cliente(nombre, direccion, telefono,datosRef);
        var pedidos = accesoADatosPedidos.Obetener() ?? new List<Pedido>();
        var pedido = new Pedido(pedidos.Count, observacion, cliente);
        pedidos.Add(pedido);
        accesoADatosPedidos.GuardarPedido(pedidos);
        return pedido;
    }
    public Cadete addCadete(int id, string nombre, string direccion, long telefono){
        var cadete = new Cadete(id,nombre,direccion,telefono);
        var Cadetes = accesoADatosCadetes.Obetener();
        Cadetes.Add(cadete);
        accesoADatosCadetes.GuardarCadete(Cadetes);
        return cadete;
    }
    public Pedido AsignarPedido(int idPedido,int idCadete){
        var pedidos = accesoADatosPedidos.Obetener();
        var ped = pedidos.FirstOrDefault(p => p.Numero == idPedido);
        var cad= ObtenerCadete(idCadete);
        if (ped != null && cad != null)
        {
            ped.IdCadete=idCadete;
            accesoADatosPedidos.GuardarPedido(pedidos);
        }
        return ped;
    }
    public bool MoverPedido(int NumeroPedido,int id){
        var Pedidos = accesoADatosPedidos.Obetener();
        var Pedido = Pedidos.FirstOrDefault(p=>p.Numero == NumeroPedido);
        if(Pedido != null){
            Pedido.IdCadete=id;
            accesoADatosPedidos.GuardarPedido(Pedidos);
            return true;
        }
        return false;
    }
    public float PedPromedioCad(){
        var cad = accesoADatosCadetes.Obetener();
        var ped = accesoADatosPedidos.Obetener();
        return ped.Count()/cad.Count();
    }
    public float TotalAPagar(){
        float Total=0;
        var cadetes = accesoADatosCadetes.Obetener();
        var Pedidos = accesoADatosPedidos.Obetener();
        foreach (var c in cadetes)
        {
            Total = Total + c.JornalACobrar(Pedidos);
        }
        return Total;
    }
    public float JornalACobrar(int idCadete){
        var cadetes = accesoADatosCadetes.Obetener();
        var pedidos = accesoADatosPedidos.Obetener();
        var cad = cadetes.FirstOrDefault(c => c.Id==idCadete);
        if (cad != null)
        {
            return cad.JornalACobrar(pedidos);
        }
        return 0;
    }
    public Pedido ObtenerPedido(int idPedido){
        var pedidos = accesoADatosPedidos.Obetener();
        var ped = pedidos.FirstOrDefault(p => p.Numero == idPedido);
        return ped;
    }
    public Cadete ObtenerCadete(int idCadete){
        var Cadetes = accesoADatosCadetes.Obetener();
        var cad = Cadetes.FirstOrDefault(c=>c.Id==idCadete);
        return cad;
    }
    public bool CambiarEstadoPedido(int idPedido,int Op){
        var pedidos = accesoADatosPedidos.Obetener();
        var ped = pedidos.FirstOrDefault(p => p.Numero == idPedido);
        //var ped = ObtenerPedido(idPedido);
        if (ped != null)
        {
            ped.CambiarEstadoPedido(Op);
            accesoADatosPedidos.GuardarPedido(pedidos);
            return true;
        }
        return false;
    }
}
