namespace tl2_tp4_2023_VelizMiguelC;
using System.Linq;

public class Pedido{
    private int numero;
    private string observacion;
    private Estado estado;
    private Cliente client;
    private int idCadete;
    public int Numero { get => numero; set => numero = value; }
    public string Observacion { get => observacion; set => observacion = value; }
    public Estado Estado { get => estado; set => estado = value; }
    public Cliente Client { get => client; set => client = value; }
    public int IdCadete { get => idCadete; set => idCadete = value; }

    public Pedido(int numero, string observacion,Cliente client)
    {
        Numero = numero;
        Observacion = observacion;
        Estado = Estado.SinEntregar;
        Client = client;
        IdCadete = 0;
    }
    public void EntregarPedido(){
        Estado = Estado.Entregado;
    }
    public void CancelarPedido(){
        Estado = Estado.Cancelado;
    }
}