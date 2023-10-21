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
public class EstadoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EstadoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EstadoDto>>> Get()
    {
        var estados = await _unitOfWork.Estados.GetAllAsync();
        return this._mapper.Map<List<EstadoDto>>(estados);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EstadoDto>> Get(int id)
    {
        var estado = await _unitOfWork.Estados.GetByIdAsync(id);
        if (estado == null){
            return NotFound();
        }
        return this._mapper.Map<EstadoDto>(estado);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Estado>> Post(EstadoDto estadoDto)
    {
        var estado = this._mapper.Map<Estado>(estadoDto);
        this._unitOfWork.Estados.Add(estado);
        await _unitOfWork.SaveAsync();
        if(estado == null)
        {
            return BadRequest();
        }
        estadoDto.Id = estado.Id;
        return CreatedAtAction(nameof(Post), new {id = estadoDto.Id}, estadoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EstadoDto>> Put(int id, [FromBody]EstadoDto estadoDto){
        if(estadoDto == null)
        {
            return NotFound();
        }
        var estado = this._mapper.Map<Estado>(estadoDto);
        _unitOfWork.Estados.Update(estado);
        await _unitOfWork.SaveAsync();
        return estadoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var estado = await _unitOfWork.Estados.GetByIdAsync(id);
        if(estado == null)
        {
            return NotFound();
        }
        _unitOfWork.Estados.Remove(estado);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


