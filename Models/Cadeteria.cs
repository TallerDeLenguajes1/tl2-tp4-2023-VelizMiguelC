namespace tl2_tp4_2023_VelizMiguelC;
using System.Linq;
using EspacioDeArchivos;
using Microsoft.VisualBasic;

public enum Estado{
    Entregado,
    SinEntregar,
    Cancelado,
}
public class Cadeteria{
    private string nombre;
    private long telefono;
    private List<Cadete> cadetes;
    private List<Pedido> pedidos;
    private static Cadeteria instance;
    private Cadeteria()
    {
        // Inicializa las propiedades si es necesario.
        nombre = "Mi Cadetería";
        telefono = 0;
        // cadetes = new List<Cadete>();
        // pedidos = new List<Pedido>();
        // Cadetes.Add(new Cadete(1,"Ramiro","BdRS",3814159593));
        // Cadetes.Add(new Cadete(2,"Miguel","SMdT",3814650223));
        // Cadetes.Add(new Cadete(3,"Jose","YB",3816312527));
        // TomarPedido("Juan","SMdT",1234,"casa verde","fragil");
        // TomarPedido("Guille","SMdT",4321,"reja roja","no fragil");
        // TomarPedido("Gaby","SMdT",4567,"edificio rosa","fragil");
    }
        public static Cadeteria Instance
    {
        get
        {
            // Crear la instancia Cadeteria si aún no existe.
            if (instance == null)
            {
                var json = new AccesoJSON();
                instance = json.LeerCadeteria();
                instance.cadetes = json.LeerCadetes();
            }
            return instance;
        }
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public long Telefono { get => telefono; set => telefono = value; }
    public List<Cadete> Cadetes { get => cadetes; set => cadetes = value; }
    public List<Pedido> Pedidos { get => pedidos; set => pedidos = value; }
    public Cadeteria(string nombre, long telefono)
    {
        Nombre = nombre;
        Telefono = telefono;
        Cadetes = new List<Cadete>();
        Pedidos = new List<Pedido>();
    }
    public Informe GetInforme(){
    return new Informe(Cadetes,Pedidos);
    }   
    public Pedido TomarPedido(string nombre, string direccion, long telefono, string datosRef,  string observacion) {
        var cliente = new Cliente(nombre, direccion, telefono,datosRef);
        var pedido = new Pedido(Pedidos.Count(),observacion,cliente);
        Pedidos.Add(pedido);
        return pedido;
    }
    public Pedido AsignarPedido(int idPedido,int idCadete){
        var ped = ObtenerPedido(idPedido);
        var cad= ObtenerCadete(idCadete);
        if (ped != null && cad != null)
        {
            ped.IdCadete=idCadete;
        }
        return ped;
    }
    public bool MoverPedido(int NumeroPedido,int id){
        var Pedido = Pedidos.FirstOrDefault(p=>p.Numero == NumeroPedido);
        if(Pedido != null){
            Pedido.IdCadete=id;
            return true;
        }
        return false;
    }
    public float PedPromedioCad(){
        return Pedidos.Count()/Cadetes.Count();
    }
    public float TotalAPagar(){
        float Total=0;
        foreach (var c in cadetes)
        {
            Total = Total + c.JornalACobrar(Pedidos);
        }
        return Total;
    }
    public float JornalACobrar(int idCadete){
        var cad = cadetes.FirstOrDefault(c => c.Id==idCadete);
        if (cad != null)
        {
            return cad.JornalACobrar(pedidos);
        }
        return 0;
    }
    public Pedido ObtenerPedido(int idPedido){
        var ped = Pedidos.FirstOrDefault(p => p.Numero == idPedido);
        return ped;
    }
    public Cadete ObtenerCadete(int idCadete){
        var cad = Cadetes.FirstOrDefault(c=>c.Id==idCadete);
        return cad;
    }
    public bool CambiarEstadoPedido(int idPedido,int Op){
        var ped = ObtenerPedido(idPedido);
        if (ped != null)
        {
            ped.CambiarEstadoPedido(Op);
            return true;
        }
        return false;
    }
}
