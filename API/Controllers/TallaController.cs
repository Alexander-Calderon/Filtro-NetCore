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
public class TallaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public TallaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<TallaDto>>> Get()
    {
        var tallas = await _unitOfWork.Tallas.GetAllAsync();
        return this._mapper.Map<List<TallaDto>>(tallas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TallaDto>> Get(int id)
    {
        var talla = await _unitOfWork.Tallas.GetByIdAsync(id);
        if (talla == null){
            return NotFound();
        }
        return this._mapper.Map<TallaDto>(talla);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Talla>> Post(TallaDto tallaDto)
    {
        var talla = this._mapper.Map<Talla>(tallaDto);
        this._unitOfWork.Tallas.Add(talla);
        await _unitOfWork.SaveAsync();
        if(talla == null)
        {
            return BadRequest();
        }
        tallaDto.Id = talla.Id;
        return CreatedAtAction(nameof(Post), new {id = tallaDto.Id}, tallaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<TallaDto>> Put(int id, [FromBody]TallaDto tallaDto){
        if(tallaDto == null)
        {
            return NotFound();
        }
        var talla = this._mapper.Map<Talla>(tallaDto);
        _unitOfWork.Tallas.Update(talla);
        await _unitOfWork.SaveAsync();
        return tallaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var talla = await _unitOfWork.Tallas.GetByIdAsync(id);
        if(talla == null)
        {
            return NotFound();
        }
        _unitOfWork.Tallas.Remove(talla);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


