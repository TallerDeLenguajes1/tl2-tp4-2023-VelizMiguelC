using EspacioDeArchivos;
using Microsoft.AspNetCore.Mvc;
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
    
}   