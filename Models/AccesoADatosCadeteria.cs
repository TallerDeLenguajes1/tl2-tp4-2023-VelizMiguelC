using System.Text.Json;
namespace tl2_tp4_2023_VelizMiguelC;

public class AccesoADatosCadeteria
{
    public Cadeteria Obetener(){
        Cadeteria Cadet = null;

        if (File.Exists("Cadeteria.json"))
        {
            string json = File.ReadAllText("Cadeteria.json");
            Cadet = JsonSerializer.Deserialize<Cadeteria>(json);
        }else
        {
            System.Console.WriteLine("No existe Cadeteria.json");
        }
        return Cadet;
    }
}