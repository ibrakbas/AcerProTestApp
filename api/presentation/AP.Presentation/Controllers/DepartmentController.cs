using AP.Data.dtos;
using AP.Data.entites;
using AP.Generic.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AP.Presentation.Controllers;


 
[Route("[controller]")]
public class  DepartmentController : ControllerBase
{
    private readonly IUnitOfWork _unitofWork;
    private IMapper Mapper { get; }
    public DepartmentController(IUnitOfWork UOW, IMapper mapper)
    {
        _unitofWork = UOW;
        Mapper = mapper;
    }

   
    [HttpPost("add/")]
    public async Task<IActionResult> AddAsync([FromBody] DepartmentDto dto)
    {

        var entity = await _unitofWork.Repository.AddAsync<Departments, int>(Mapper.Map<Departments>(dto), default);


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
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] DepartmentDto dto)
    {
        var entity = await _unitofWork.Repository.GetByIdAsync<Departments>(true, id);

        entity = Mapper.Map<DepartmentDto, Departments>(dto, entity);

        var result = await _unitofWork.Repository.UpdateAsync<Departments, int>(entity);
        await _unitofWork.Repository.CompleteAsync();
        return Ok(result);
    }
 
    [HttpDelete("hard/{id}")]
    public async Task<IActionResult> HardDeleteAsync([FromRoute] int id)
    {
        var entity = await _unitofWork.Repository.GetByIdAsync<Departments>(true, id);
        await _unitofWork.Repository.HardDeleteAsync<Departments>(entity);
        await _unitofWork.Repository.CompleteAsync();
        return NoContent();
    }
    
    [HttpDelete("soft/{id}")]
    public async Task<IActionResult> SoftDeleteAsync([FromRoute] int id)
    {
        var entity = await _unitofWork.Repository.GetByIdAsync<Departments>(true, id);
        await _unitofWork.Repository.SoftDeleteAsync<Departments, int>(entity);
        await _unitofWork.Repository.CompleteAsync();
        return NoContent();
    }

   
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        Func<IQueryable<Departments>, IIncludableQueryable<Departments, object>> include =
          a => a.Include(i => i.Workers);

        var entity = await _unitofWork.Repository.GetByIdAsync<Departments>(true, id, include);
        return Ok(entity);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetMultipleAsync()
    {
        Func<IQueryable<Departments>, IIncludableQueryable<Departments, object>> include =
           a => a.Include(i => i.Workers);

        var entities = await _unitofWork.Repository.GetMultipleAsync<Departments>(true, include);

        return Ok(entities);
    }


}
