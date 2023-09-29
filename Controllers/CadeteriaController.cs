using EspacioDeArchivos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using tl2_tp4_2023_VelizMiguelC;

namespace tl2_tp4_2023_VelizMiguelC.Controllers;
[ApiController]
[Route("[controller]")]
public class CadeteriaController : ControllerBase
{
    private Cadeteria cadeteria;
    private readonly ILogger<CadeteriaController> _logger;

    public CadeteriaController(ILogger<CadeteriaController> logger)
    {
        _logger = logger;
        cadeteria = Cadeteria.Instance;
    }
    [HttpGet]
    public ActionResult <string> GetCadeteria(){
        return Ok(cadeteria);
    }
    [HttpGet]
    [Route("Pedidos")]
    public ActionResult <string> GetPedidos(){
        return Ok(cadeteria.Pedidos);
    }
    [HttpGet]
    [Route("Cadetes")]
    public ActionResult <string> GetCadetes(){
        return Ok(cadeteria.Cadetes);
    }
    [HttpGet]
    [Route("Informe")]

    public ActionResult <string> GetInforme(){
        var informe =new Informe(cadeteria.Cadetes,cadeteria.Pedidos);
        return Ok(informe);
    }
    [HttpPost("addPedido")]
    public ActionResult <string> addPedido(string nombre, string direccion, long telefono, string datosRef,  string observacion){
        var ped = cadeteria.TomarPedido(nombre,direccion,telefono,datosRef,observacion);
        if (ped != null)
        {
            return Accepted(ped);
        }
        return StatusCode(500,"No se pudo tomar pedido");
    }
    [HttpPut("AsignarPedido")]
    public ActionResult <string> AsignarPedido(int idPedido,int idCadete){
        var ped = cadeteria.AsignarPedido(idPedido,idCadete);
        if (ped != null)
        {
            return Ok("Se asigno el pedido");
        }
        return NotFound("Fallo de servidor");
    }
    [HttpPut("CambiarEstadoPedido")]
    public ActionResult <string> CambiarEstadoPedido(int idPedido,int NuevoEstado){
        if (cadeteria.CambiarEstadoPedido(idPedido,NuevoEstado))
        {
            return Accepted("El Estado del pedido se cambio");
        }
        return StatusCode(500,"No se pudo cambiar el estado del pedido");
    }
    [HttpPut("CambiarCadetePedido")]
    public ActionResult <string> CambiarCadetePedido(int idPedido,int idNuevoCadete){
        if (cadeteria.MoverPedido(idPedido,idNuevoCadete))
        {
            return Accepted("El Cambio de cadete del pedido se cambio");
        }
        return StatusCode(500,"No se pudo cambiar el cadete del pedido");
    }
}   