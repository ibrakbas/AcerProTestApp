using AP.Data.dtos;
using AP.Data.entites;
using AP.Generic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AP.Presentation.Controllers;

 
[Route("[controller]")]
[Authorize]
public class  WorkerController : ControllerBase
{
    private readonly IUnitOfWork _unitofWork;
    private IMapper Mapper { get; }
    public WorkerController(IUnitOfWork UOW, IMapper mapper)
    {
        _unitofWork = UOW;
        Mapper = mapper;
    }

    
    [HttpPost("add/")]
    [Authorize]
    public async Task<IActionResult> AddAsync([FromBody] WorkerDto dto)
    {
        var entity = await _unitofWork.Repository.AddAsync<Workers, int>(Mapper.Map<Workers>(dto), default);


        try
        {
            await _unitofWork.Repository.CompleteAsync();

        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.InnerException.Message);
        } 

        return Ok(entity);
    }
    
    [HttpPost("update/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] WorkerDto dto)
    {
        var entity = await _unitofWork.Repository.GetByIdAsync<Workers>(true, id);

        entity = Mapper.Map<WorkerDto, Workers>(dto, entity);

        var result = await _unitofWork.Repository.UpdateAsync<Workers, int>(entity);
        await _unitofWork.Repository.CompleteAsync();
        return Ok(result);
    }
   
    [HttpDelete("hard/{id}")]
    [Authorize]
    public async Task<IActionResult> HardDeleteAsync([FromRoute] int id)
    {
        var entity = await _unitofWork.Repository.GetByIdAsync<Workers>(true, id);
        await _unitofWork.Repository.HardDeleteAsync<Workers>(entity);
        await _unitofWork.Repository.CompleteAsync();
        return NoContent();
    }
   
    [HttpDelete("soft/{id}")]
    [Authorize]
    public async Task<IActionResult> SoftDeleteAsync([FromRoute] int id)
    {
        var entity = await _unitofWork.Repository.GetByIdAsync<Workers>(true, id);
        await _unitofWork.Repository.SoftDeleteAsync<Workers, int>(entity);
        await _unitofWork.Repository.CompleteAsync();
        return NoContent();
    }
 
    [HttpGet("get/{id}")]
    [Authorize]

    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        Func<IQueryable<Workers>, IIncludableQueryable<Workers, object>> include =
          a => a.Include(i => i.Department);

        var entity = await _unitofWork.Repository.GetByIdAsync<Workers>(true, id, include);
        return Ok(entity);
    }
    
    [HttpGet("getall")]
    [Authorize]
    public async Task<IActionResult> GetMultipleAsync()
    {
        Func<IQueryable<Workers>, IIncludableQueryable<Workers, object>> include =
           a => a.Include(i => i.Department);

        var entities = await _unitofWork.Repository.GetMultipleAsync<Workers>(true,include);

        return Ok(entities);
    }


}
