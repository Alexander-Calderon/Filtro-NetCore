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
public class InsumoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public InsumoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<InsumoDto>>> Get()
    {
        var insumos = await _unitOfWork.Insumos.GetAllAsync();
        return this._mapper.Map<List<InsumoDto>>(insumos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<InsumoDto>> Get(int id)
    {
        var insumo = await _unitOfWork.Insumos.GetByIdAsync(id);
        if (insumo == null){
            return NotFound();
        }
        return this._mapper.Map<InsumoDto>(insumo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Insumo>> Post(InsumoDto insumoDto)
    {
        var insumo = this._mapper.Map<Insumo>(insumoDto);
        this._unitOfWork.Insumos.Add(insumo);
        await _unitOfWork.SaveAsync();
        if(insumo == null)
        {
            return BadRequest();
        }
        insumoDto.Id = insumo.Id;
        return CreatedAtAction(nameof(Post), new {id = insumoDto.Id}, insumoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<InsumoDto>> Put(int id, [FromBody]InsumoDto insumoDto){
        if(insumoDto == null)
        {
            return NotFound();
        }
        var insumo = this._mapper.Map<Insumo>(insumoDto);
        _unitOfWork.Insumos.Update(insumo);
        await _unitOfWork.SaveAsync();
        return insumoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var insumo = await _unitOfWork.Insumos.GetByIdAsync(id);
        if(insumo == null)
        {
            return NotFound();
        }
        _unitOfWork.Insumos.Remove(insumo);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    //

    [HttpGet("proveedor/{proveedorId}")]
    public IActionResult GetInsumosByProveedor(int proveedorId)
    {
        var insumos = _unitOfWork.Insumos.GetInsumosByProveedor(proveedorId);
        return Ok(insumos);
    }


    
}


