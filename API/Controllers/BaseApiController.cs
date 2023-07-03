using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")] //localhost:4200/api/users[controller indicated the name of the present]
    public class BaseApiController:ControllerBase //ControllerBase->Provides functionality for Http requests 
    {
        
    }
}