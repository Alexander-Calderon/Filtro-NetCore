using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using AutoMapper;
using API.Dtos;
using Dominio.Entidades;

namespace API.Controllers;

[ApiVersion("1.0")]
[ApiVersion("1.1")]
[Authorize(Roles = "Empleado, Administrador, Gerente")]
public class EmpleadoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpleadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpleadoDto>>> Get()
    {
        var empleados = await _unitOfWork.Empleados.GetAllAsync();
        return this._mapper.Map<List<EmpleadoDto>>(empleados);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpleadoDto>> Get(int id)
    {
        var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
        if (empleado == null){
            return NotFound();
        }
        return this._mapper.Map<EmpleadoDto>(empleado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Empleado>> Post(EmpleadoDto empleadoDto)
    {
        var empleado = this._mapper.Map<Empleado>(empleadoDto);
        this._unitOfWork.Empleados.Add(empleado);
        await _unitOfWork.SaveAsync();
        if(empleado == null)
        {
            return BadRequest();
        }
        empleadoDto.Id = empleado.Id;
        return CreatedAtAction(nameof(Post), new {id = empleadoDto.Id}, empleadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpleadoDto>> Put(int id, [FromBody]EmpleadoDto empleadoDto){
        if(empleadoDto == null)
        {
            return NotFound();
        }
        var empleado = this._mapper.Map<Empleado>(empleadoDto);
        _unitOfWork.Empleados.Update(empleado);
        await _unitOfWork.SaveAsync();
        return empleadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var empleado = await _unitOfWork.Empleados.GetByIdAsync(id);
        if(empleado == null)
        {
            return NotFound();
        }
        _unitOfWork.Empleados.Remove(empleado);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


