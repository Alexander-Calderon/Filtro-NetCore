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
public class OrdenController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrdenController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<OrdenDto>>> Get()
    {
        var ordens = await _unitOfWork.Ordenes.GetAllAsync();
        return this._mapper.Map<List<OrdenDto>>(ordens);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrdenDto>> Get(int id)
    {
        var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
        if (orden == null){
            return NotFound();
        }
        return this._mapper.Map<OrdenDto>(orden);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Orden>> Post(OrdenDto ordenDto)
    {
        var orden = this._mapper.Map<Orden>(ordenDto);
        this._unitOfWork.Ordenes.Add(orden);
        await _unitOfWork.SaveAsync();
        if(orden == null)
        {
            return BadRequest();
        }
        ordenDto.Id = orden.Id;
        return CreatedAtAction(nameof(Post), new {id = ordenDto.Id}, ordenDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<OrdenDto>> Put(int id, [FromBody]OrdenDto ordenDto){
        if(ordenDto == null)
        {
            return NotFound();
        }
        var orden = this._mapper.Map<Orden>(ordenDto);
        _unitOfWork.Ordenes.Update(orden);
        await _unitOfWork.SaveAsync();
        return ordenDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var orden = await _unitOfWork.Ordenes.GetByIdAsync(id);
        if(orden == null)
        {
            return NotFound();
        }
        _unitOfWork.Ordenes.Remove(orden);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    //

    [HttpGet("PrendasProduccion/{ordenId}")]
    public IActionResult GetPrendasEnProduccion(int ordenId) 
    {
        var prendas = _unitOfWork.Ordenes.GetPrendasEnProduccion(ordenId);
        return Ok(prendas);
    }



    
}


