namespace tl2_tp4_2023_VelizMiguelC;

public class Informe
{
    private List<CadeteInforme> cadetes;
    private int PedPromedioCad;
    private float montoTotal;
    public Informe(List<Cadete> ListC,List<Pedido> ListP){
        var CadInforme = new List<CadeteInforme>();
        float monto = 0;
        foreach (var c in ListC)
        {
            CadInforme.Add(new CadeteInforme(c,ListP));
            monto = monto + c.JornalACobrar(ListP);
        } 
        PedPromedioCad = ListP.Count()/ListC.Count();
        montoTotal = monto;
    }
    public List<CadeteInforme> Cadetes { get => cadetes; set => cadetes = value; }
    public int PedPromedioCad1 { get => PedPromedioCad; set => PedPromedioCad = value; }
    public float MontoTotal { get => montoTotal; set => montoTotal = value; }

}
public class CadeteInforme{
    private string nombre;
    private int pedidosEntregados;
    private int pedidosSinEntregar;
    private int pedidosCancelados;
    private float sueldo;

    public CadeteInforme(Cadete cad,List<Pedido> ListP)
    {
        this.nombre = cad.Nombre;
        this.pedidosEntregados = cad.CantidadDePedidos(ListP,1);
        this.pedidosSinEntregar = cad.CantidadDePedidos(ListP,2);
        this.pedidosCancelados = cad.CantidadDePedidos(ListP,3);
        this.sueldo = cad.JornalACobrar(ListP);
    }

    public string Nombre { get => nombre; set => nombre = value; }
    public int PedidosEntregados { get => pedidosEntregados; set => pedidosEntregados = value; }
    public int PedidosSinEntregar { get => pedidosSinEntregar; set => pedidosSinEntregar = value; }
    public int PedidosCancelados { get => pedidosCancelados; set => pedidosCancelados = value; }
    public float Sueldo { get => sueldo; set => sueldo = value; }
    public int CantidadTotalDePedidos(){
        return pedidosCancelados + pedidosEntregados + pedidosSinEntregar;
    }
}