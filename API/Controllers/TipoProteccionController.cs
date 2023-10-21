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
public class TipoProteccionController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoProteccionController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoProteccionDto>>> Get()
    {
        var tipoproteccions = await _unitOfWork.TiposProtecciones.GetAllAsync();
        return this._mapper.Map<List<TipoProteccionDto>>(tipoproteccions);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoProteccionDto>> Get(int id)
    {
        var tipoproteccion = await _unitOfWork.TiposProtecciones.GetByIdAsync(id);
        if (tipoproteccion == null){
            return NotFound();
        }
        return this._mapper.Map<TipoProteccionDto>(tipoproteccion);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoProteccion>> Post(TipoProteccionDto tipoproteccionDto)
    {
        var tipoproteccion = this._mapper.Map<TipoProteccion>(tipoproteccionDto);
        this._unitOfWork.TiposProtecciones.Add(tipoproteccion);
        await _unitOfWork.SaveAsync();
        if(tipoproteccion == null)
        {
            return BadRequest();
        }
        tipoproteccionDto.Id = tipoproteccion.Id;
        return CreatedAtAction(nameof(Post), new {id = tipoproteccionDto.Id}, tipoproteccionDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoProteccionDto>> Put(int id, [FromBody]TipoProteccionDto tipoproteccionDto){
        if(tipoproteccionDto == null)
        {
            return NotFound();
        }
        var tipoproteccion = this._mapper.Map<TipoProteccion>(tipoproteccionDto);
        _unitOfWork.TiposProtecciones.Update(tipoproteccion);
        await _unitOfWork.SaveAsync();
        return tipoproteccionDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var tipoproteccion = await _unitOfWork.TiposProtecciones.GetByIdAsync(id);
        if(tipoproteccion == null)
        {
            return NotFound();
        }
        _unitOfWork.TiposProtecciones.Remove(tipoproteccion);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


