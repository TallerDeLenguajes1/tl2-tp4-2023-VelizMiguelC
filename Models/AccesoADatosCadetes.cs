using System.Text.Json;
namespace tl2_tp4_2023_VelizMiguelC;

public class AccesoADatosCadetes
{
    public List<Cadete> Obetener(){
        List<Cadete> Cadet = null;

        if (File.Exists("Cadetes.json"))
        {
            string json = File.ReadAllText("Cadetes.json");
            Cadet = JsonSerializer.Deserialize<List<Cadete>>(json);
        }else
        {
            System.Console.WriteLine("No existe Cadetes.json");
        }
        return Cadet;
    }
    public void GuardarCadete(List<Cadete> Cadetes){
        var json = JsonSerializer.Serialize(Cadetes);
        File.WriteAllText("Cadetes.json",json);
    }
}