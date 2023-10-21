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
public class InventarioTallaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public InventarioTallaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InventarioTallaDto>>> Get()
    {
        var inventariotallas = await _unitOfWork.InventariosTallas.GetAllAsync();
        return this._mapper.Map<List<InventarioTallaDto>>(inventariotallas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventarioTallaDto>> Get(int id)
    {
        var inventariotalla = await _unitOfWork.InventariosTallas.GetByIdAsync(id);
        if (inventariotalla == null){
            return NotFound();
        }
        return this._mapper.Map<InventarioTallaDto>(inventariotalla);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<InventarioTalla>> Post(InventarioTallaDto inventariotallaDto)
    {
        var inventariotalla = this._mapper.Map<InventarioTalla>(inventariotallaDto);
        this._unitOfWork.InventariosTallas.Add(inventariotalla);
        await _unitOfWork.SaveAsync();
        if(inventariotalla == null)
        {
            return BadRequest();
        }
        inventariotallaDto.Id = inventariotalla.Id;
        return CreatedAtAction(nameof(Post), new {id = inventariotallaDto.Id}, inventariotallaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<InventarioTallaDto>> Put(int id, [FromBody]InventarioTallaDto inventariotallaDto){
        if(inventariotallaDto == null)
        {
            return NotFound();
        }
        var inventariotalla = this._mapper.Map<InventarioTalla>(inventariotallaDto);
        _unitOfWork.InventariosTallas.Update(inventariotalla);
        await _unitOfWork.SaveAsync();
        return inventariotallaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var inventariotalla = await _unitOfWork.InventariosTallas.GetByIdAsync(id);
        if(inventariotalla == null)
        {
            return NotFound();
        }
        _unitOfWork.InventariosTallas.Remove(inventariotalla);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


