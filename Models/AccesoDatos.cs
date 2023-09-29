
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;
using System.Text.Json.Serialization;
using tl2_tp4_2023_VelizMiguelC;

namespace EspacioDeArchivos
{
    public abstract class AccesoADatos{
        public abstract bool Existe(string Nombre);
        public abstract Cadeteria LeerCadeteria();
        public abstract List<Cadete> LeerCadetes();
        public void GuardarResumen(Cadeteria Cadeteria1){
            try
            {
                using (StreamWriter arch = new StreamWriter("Resumen.txt"))
                {
                    arch.WriteLine(Cadeteria1    + "RESUMEN");
                    arch.WriteLine("Fecha:" + DateTime.Now.ToString("dddd d 'de' MMMM 'de' yyyy"));
                    arch.WriteLine("PE: pedidos entregados, PSE: pedidos sin entregar, PC: pedidos cancelados, PT: pedidos totales.");
                    foreach (var c in Cadeteria1.Cadetes)
                    {
                        var id = c.Id;
                        var nombre = c.Nombre;
                        var PedidosC = c.CantidadDePedidos(Cadeteria1.Pedidos,3);
                        var PedidosSE = c.CantidadDePedidos(Cadeteria1.Pedidos,2);
                        var PedidosE = c.CantidadDePedidos(Cadeteria1.Pedidos,1);
                        var CantPed = c.CantidadDePedidos(Cadeteria1.Pedidos,0);
                        var Pago = Cadeteria1.JornalACobrar(id);
                        arch.WriteLine(id+"| "+nombre+", PedidosE"+PedidosC+" PedidosSE:"+PedidosSE+" PedidosC:"+PedidosC+" PT:"+CantPed+", JORNAL: "+Pago);
        
                    }
                    var NumeroPed = Cadeteria1.Pedidos.Count();
                    arch.WriteLine("Total de pedidos: "+NumeroPed+"  Total a pagar: "+Cadeteria1.TotalAPagar());
                }
                // Console.WriteLine("Arreglo de cadenas escrito en el archivo correctamente.");    
            }
            catch (IOException e)
            {
                System.Console.WriteLine($"Ocurrio un error al escribir en el archivo: {e.Message}");
            }
        }
        public void EscribirResumen(){
            if (Existe("Resumen"))
            {
                using (StreamReader read = new StreamReader("Resumen.txt")){
                    while (!read.EndOfStream)
                    {
                        System.Console.WriteLine(read.ReadLine());
                    }
                }
            }
        }
    }
    public class AccesoJSON : AccesoADatos
    {
        public override bool Existe(string nombre){
            return File.Exists(nombre+".json") ||File.Exists(nombre+".txt");
        }
        public override Cadeteria LeerCadeteria()
        {
            Cadeteria cadeteria = null;
            if(Existe("Cadeteria.json")){
                string json = File.ReadAllText("Cadeteria.json");
                cadeteria = JsonSerializer.Deserialize<Cadeteria>(json);
            }else
            {
                Console.WriteLine("No existe el json Cadeteria");
            }
            return cadeteria;
        }
        public override List<Cadete> LeerCadetes(){
        if (Existe("Cadetes.json")){
            string json = File.ReadAllText("Cadetes.json");
            return JsonSerializer.Deserialize<List<Cadete>>(json);
        } else {
            Console.WriteLine("No existe el json");
        }
        return null;
        }
        public void GuardarJSON(string nombre, Cadeteria cadeteria){
            var json = JsonSerializer.Serialize(cadeteria);
            File.WriteAllText(nombre + ".json",json);
        }
        public void GuardarJSON(string nombre, List<Cadete> cadetes) {
            var json = JsonSerializer.Serialize(cadetes);
            File.WriteAllText(nombre+".json",json);
        }
    }
        public class AccesoCSV : AccesoADatos {
            public override bool Existe(string nombre){
            return File.Exists(nombre+".csv")||File.Exists(nombre+".txt");
        }
        public override Cadeteria LeerCadeteria(){
            using(TextFieldParser ruta = new TextFieldParser("Cadeteria.csv")){
                ruta.TextFieldType = FieldType.Delimited;
                ruta.SetDelimiters(",",";");
                while(!ruta.EndOfData){
                    string[] colum = ruta.ReadFields();
                    if(colum.Count()==2){
                        var nom = colum[0];
                        int tel; 
                        int.TryParse(colum[1],out tel);
                        var Cdria = new Cadeteria(nom,tel);
                        return Cdria;
                    }else{
                        System.Console.WriteLine("no tiene el formato adecuado");
                    }
                }
            }
            return null;
        }
        public override List<Cadete> LeerCadetes(){
            using(TextFieldParser ruta = new TextFieldParser("Cadetes.csv")){
                ruta.TextFieldType = FieldType.Delimited;
                var Cadts = new List<Cadete>();
                ruta.SetDelimiters(",",";");
                while(!ruta.EndOfData){
                    string[] filas = ruta.ReadFields();
                    if(filas.Count()==4){
                        int id, tel; 
                        var nom = filas[1];
                        var dire = filas[2];
                        int.TryParse(filas[0],out id);
                        int.TryParse(filas[3],out tel);
                        var cad = new Cadete(id,nom,dire,tel);
                        Cadts.Add(cad);
                    }else{
                        System.Console.WriteLine("no tiene el formato adecuado");
                    }
                }
                return Cadts;
            }
        }  
    }
}
    