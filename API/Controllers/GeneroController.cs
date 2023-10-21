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
public class GeneroController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public GeneroController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<GeneroDto>>> Get()
    {
        var generos = await _unitOfWork.Generos.GetAllAsync();
        return this._mapper.Map<List<GeneroDto>>(generos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<GeneroDto>> Get(int id)
    {
        var genero = await _unitOfWork.Generos.GetByIdAsync(id);
        if (genero == null){
            return NotFound();
        }
        return this._mapper.Map<GeneroDto>(genero);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Genero>> Post(GeneroDto generoDto)
    {
        var genero = this._mapper.Map<Genero>(generoDto);
        this._unitOfWork.Generos.Add(genero);
        await _unitOfWork.SaveAsync();
        if(genero == null)
        {
            return BadRequest();
        }
        generoDto.Id = genero.Id;
        return CreatedAtAction(nameof(Post), new {id = generoDto.Id}, generoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<GeneroDto>> Put(int id, [FromBody]GeneroDto generoDto){
        if(generoDto == null)
        {
            return NotFound();
        }
        var genero = this._mapper.Map<Genero>(generoDto);
        _unitOfWork.Generos.Update(genero);
        await _unitOfWork.SaveAsync();
        return generoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var genero = await _unitOfWork.Generos.GetByIdAsync(id);
        if(genero == null)
        {
            return NotFound();
        }
        _unitOfWork.Generos.Remove(genero);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


