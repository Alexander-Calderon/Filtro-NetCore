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
public class VentaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public VentaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<VentaDto>>> Get()
    {
        var ventas = await _unitOfWork.Ventas.GetAllAsync();
        return this._mapper.Map<List<VentaDto>>(ventas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<VentaDto>> Get(int id)
    {
        var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
        if (venta == null){
            return NotFound();
        }
        return this._mapper.Map<VentaDto>(venta);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Venta>> Post(VentaDto ventaDto)
    {
        var venta = this._mapper.Map<Venta>(ventaDto);
        this._unitOfWork.Ventas.Add(venta);
        await _unitOfWork.SaveAsync();
        if(venta == null)
        {
            return BadRequest();
        }
        ventaDto.Id = venta.Id;
        return CreatedAtAction(nameof(Post), new {id = ventaDto.Id}, ventaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<VentaDto>> Put(int id, [FromBody]VentaDto ventaDto){
        if(ventaDto == null)
        {
            return NotFound();
        }
        var venta = this._mapper.Map<Venta>(ventaDto);
        _unitOfWork.Ventas.Update(venta);
        await _unitOfWork.SaveAsync();
        return ventaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var venta = await _unitOfWork.Ventas.GetByIdAsync(id);
        if(venta == null)
        {
            return NotFound();
        }
        _unitOfWork.Ventas.Remove(venta);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


