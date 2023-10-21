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
public class DetalleVentaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DetalleVentaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DetalleVentaDto>>> Get()
    {
        var detalleventas = await _unitOfWork.DetallesVentas.GetAllAsync();
        return this._mapper.Map<List<DetalleVentaDto>>(detalleventas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetalleVentaDto>> Get(int id)
    {
        var detalleventa = await _unitOfWork.DetallesVentas.GetByIdAsync(id);
        if (detalleventa == null){
            return NotFound();
        }
        return this._mapper.Map<DetalleVentaDto>(detalleventa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<DetalleVenta>> Post(DetalleVentaDto detalleventaDto)
    {
        var detalleventa = this._mapper.Map<DetalleVenta>(detalleventaDto);
        this._unitOfWork.DetallesVentas.Add(detalleventa);
        await _unitOfWork.SaveAsync();
        if(detalleventa == null)
        {
            return BadRequest();
        }
        detalleventaDto.Id = detalleventa.Id;
        return CreatedAtAction(nameof(Post), new {id = detalleventaDto.Id}, detalleventaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DetalleVentaDto>> Put(int id, [FromBody]DetalleVentaDto detalleventaDto){
        if(detalleventaDto == null)
        {
            return NotFound();
        }
        var detalleventa = this._mapper.Map<DetalleVenta>(detalleventaDto);
        _unitOfWork.DetallesVentas.Update(detalleventa);
        await _unitOfWork.SaveAsync();
        return detalleventaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var detalleventa = await _unitOfWork.DetallesVentas.GetByIdAsync(id);
        if(detalleventa == null)
        {
            return NotFound();
        }
        _unitOfWork.DetallesVentas.Remove(detalleventa);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


