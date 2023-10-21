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
public class TipoEstadoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoEstadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoEstadoDto>>> Get()
    {
        var tipoestados = await _unitOfWork.TiposEstados.GetAllAsync();
        return this._mapper.Map<List<TipoEstadoDto>>(tipoestados);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoEstadoDto>> Get(int id)
    {
        var tipoestado = await _unitOfWork.TiposEstados.GetByIdAsync(id);
        if (tipoestado == null){
            return NotFound();
        }
        return this._mapper.Map<TipoEstadoDto>(tipoestado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoEstado>> Post(TipoEstadoDto tipoestadoDto)
    {
        var tipoestado = this._mapper.Map<TipoEstado>(tipoestadoDto);
        this._unitOfWork.TiposEstados.Add(tipoestado);
        await _unitOfWork.SaveAsync();
        if(tipoestado == null)
        {
            return BadRequest();
        }
        tipoestadoDto.Id = tipoestado.Id;
        return CreatedAtAction(nameof(Post), new {id = tipoestadoDto.Id}, tipoestadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoEstadoDto>> Put(int id, [FromBody]TipoEstadoDto tipoestadoDto){
        if(tipoestadoDto == null)
        {
            return NotFound();
        }
        var tipoestado = this._mapper.Map<TipoEstado>(tipoestadoDto);
        _unitOfWork.TiposEstados.Update(tipoestado);
        await _unitOfWork.SaveAsync();
        return tipoestadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var tipoestado = await _unitOfWork.TiposEstados.GetByIdAsync(id);
        if(tipoestado == null)
        {
            return NotFound();
        }
        _unitOfWork.TiposEstados.Remove(tipoestado);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


