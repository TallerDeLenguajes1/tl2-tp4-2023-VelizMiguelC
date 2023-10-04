using System.Text.Json;
namespace tl2_tp4_2023_VelizMiguelC;

public class AccesoADatosPedidos
{
    public List<Pedido> Obetener(){
        List<Pedido> Ped = null;

        if (File.Exists("Pedidos.json"))
        {
            string json = File.ReadAllText("Pedidos.json");
            Ped = JsonSerializer.Deserialize<List<Pedido>>(json);
        }else
        {
            System.Console.WriteLine("No existe Pedidos.json");
        }
        return Ped;
    }
    public void GuardarPedido(List<Pedido> ListP){
        var json = JsonSerializer.Serialize(ListP);
        File.WriteAllText("Pedidos.json",json);
    }
}