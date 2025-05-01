using AP.Data.dtos;
using AP.Data.dtos.responses;
using AP.Data.entites;
using AP.Generic.Services;
using AP.Utils.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AP.Presentation.Controllers;


/// <summary> 
///     Login ve JWT işlemlerini yöneten controller
/// </summary>
public sealed class LoginController : ControllerBase
{

    private readonly IUnitOfWork _unitOfWork;

    private readonly IJWTProvider jwt;

    public LoginController(IJWTProvider _jwt, IUnitOfWork unitOfWork)
    {
        jwt = _jwt;
        _unitOfWork = unitOfWork;
    }


    [HttpPost("login/")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
    {
        Func<IQueryable<Users>, IIncludableQueryable<Users, object>> includDe =
a => a.Where(x => x.UserName == dto.UserName && x.Password == dto.Password)
.Include(i => i.AP_UserRole);

        Users kullanici = _unitOfWork.Repository.GetMultiple<Users>(true, includDe).FirstOrDefault();

        if (kullanici != null)
        {
            var token = jwt.CreateTokenAsync(kullanici);
            return Ok(token);
        }
        else
        {
            return NotFound(new LoginResponse(false, "Kullanıcı Bulunamadı", null, DateTime.Now, "", null, DateTime.Now));
        }

    }
}
 
