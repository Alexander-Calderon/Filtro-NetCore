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
public class DetalleOrdenController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DetalleOrdenController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetalleOrdenDto>>> Get()
    {
        var detalleordens = await _unitOfWork.DetallesOrdenes.GetAllAsync();
        return this._mapper.Map<List<DetalleOrdenDto>>(detalleordens);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetalleOrdenDto>> Get(int id)
    {
        var detalleorden = await _unitOfWork.DetallesOrdenes.GetByIdAsync(id);
        if (detalleorden == null){
            return NotFound();
        }
        return this._mapper.Map<DetalleOrdenDto>(detalleorden);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetalleOrden>> Post(DetalleOrdenDto detalleordenDto)
    {
        var detalleorden = this._mapper.Map<DetalleOrden>(detalleordenDto);
        this._unitOfWork.DetallesOrdenes.Add(detalleorden);
        await _unitOfWork.SaveAsync();
        if(detalleorden == null)
        {
            return BadRequest();
        }
        detalleordenDto.Id = detalleorden.Id;
        return CreatedAtAction(nameof(Post), new {id = detalleordenDto.Id}, detalleordenDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetalleOrdenDto>> Put(int id, [FromBody]DetalleOrdenDto detalleordenDto){
        if(detalleordenDto == null)
        {
            return NotFound();
        }
        var detalleorden = this._mapper.Map<DetalleOrden>(detalleordenDto);
        _unitOfWork.DetallesOrdenes.Update(detalleorden);
        await _unitOfWork.SaveAsync();
        return detalleordenDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var detalleorden = await _unitOfWork.DetallesOrdenes.GetByIdAsync(id);
        if(detalleorden == null)
        {
            return NotFound();
        }
        _unitOfWork.DetallesOrdenes.Remove(detalleorden);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


