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
public class InventarioController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public InventarioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InventarioDto>>> Get()
    {
        var inventarios = await _unitOfWork.Inventarios.GetAllAsync();
        return this._mapper.Map<List<InventarioDto>>(inventarios);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InventarioDto>> Get(int id)
    {
        var inventario = await _unitOfWork.Inventarios.GetByIdAsync(id);
        if (inventario == null){
            return NotFound();
        }
        return this._mapper.Map<InventarioDto>(inventario);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Inventario>> Post(InventarioDto inventarioDto)
    {
        var inventario = this._mapper.Map<Inventario>(inventarioDto);
        this._unitOfWork.Inventarios.Add(inventario);
        await _unitOfWork.SaveAsync();
        if(inventario == null)
        {
            return BadRequest();
        }
        inventarioDto.Id = inventario.Id;
        return CreatedAtAction(nameof(Post), new {id = inventarioDto.Id}, inventarioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<InventarioDto>> Put(int id, [FromBody]InventarioDto inventarioDto){
        if(inventarioDto == null)
        {
            return NotFound();
        }
        var inventario = this._mapper.Map<Inventario>(inventarioDto);
        _unitOfWork.Inventarios.Update(inventario);
        await _unitOfWork.SaveAsync();
        return inventarioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var inventario = await _unitOfWork.Inventarios.GetByIdAsync(id);
        if(inventario == null)
        {
            return NotFound();
        }
        _unitOfWork.Inventarios.Remove(inventario);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


