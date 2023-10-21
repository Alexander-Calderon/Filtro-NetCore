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
public class TipoPersonaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TipoPersonaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TipoPersonaDto>>> Get()
    {
        var tipopersonas = await _unitOfWork.TiposPersonas.GetAllAsync();
        return this._mapper.Map<List<TipoPersonaDto>>(tipopersonas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TipoPersonaDto>> Get(int id)
    {
        var tipopersona = await _unitOfWork.TiposPersonas.GetByIdAsync(id);
        if (tipopersona == null){
            return NotFound();
        }
        return this._mapper.Map<TipoPersonaDto>(tipopersona);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TipoPersona>> Post(TipoPersonaDto tipopersonaDto)
    {
        var tipopersona = this._mapper.Map<TipoPersona>(tipopersonaDto);
        this._unitOfWork.TiposPersonas.Add(tipopersona);
        await _unitOfWork.SaveAsync();
        if(tipopersona == null)
        {
            return BadRequest();
        }
        tipopersonaDto.Id = tipopersona.Id;
        return CreatedAtAction(nameof(Post), new {id = tipopersonaDto.Id}, tipopersonaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TipoPersonaDto>> Put(int id, [FromBody]TipoPersonaDto tipopersonaDto){
        if(tipopersonaDto == null)
        {
            return NotFound();
        }
        var tipopersona = this._mapper.Map<TipoPersona>(tipopersonaDto);
        _unitOfWork.TiposPersonas.Update(tipopersona);
        await _unitOfWork.SaveAsync();
        return tipopersonaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var tipopersona = await _unitOfWork.TiposPersonas.GetByIdAsync(id);
        if(tipopersona == null)
        {
            return NotFound();
        }
        _unitOfWork.TiposPersonas.Remove(tipopersona);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


