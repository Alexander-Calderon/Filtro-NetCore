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
public class PrendaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public PrendaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<PrendaDto>>> Get()
    {
        var prendas = await _unitOfWork.Prendas.GetAllAsync();
        return this._mapper.Map<List<PrendaDto>>(prendas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<PrendaDto>> Get(int id)
    {
        var prenda = await _unitOfWork.Prendas.GetByIdAsync(id);
        if (prenda == null){
            return NotFound();
        }
        return this._mapper.Map<PrendaDto>(prenda);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Prenda>> Post(PrendaDto prendaDto)
    {
        var prenda = this._mapper.Map<Prenda>(prendaDto);
        this._unitOfWork.Prendas.Add(prenda);
        await _unitOfWork.SaveAsync();
        if(prenda == null)
        {
            return BadRequest();
        }
        prendaDto.Id = prenda.Id;
        return CreatedAtAction(nameof(Post), new {id = prendaDto.Id}, prendaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<PrendaDto>> Put(int id, [FromBody]PrendaDto prendaDto){
        if(prendaDto == null)
        {
            return NotFound();
        }
        var prenda = this._mapper.Map<Prenda>(prendaDto);
        _unitOfWork.Prendas.Update(prenda);
        await _unitOfWork.SaveAsync();
        return prendaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var prenda = await _unitOfWork.Prendas.GetByIdAsync(id);
        if(prenda == null)
        {
            return NotFound();
        }
        _unitOfWork.Prendas.Remove(prenda);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    // Consultas



    
}


