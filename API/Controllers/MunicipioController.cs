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
public class MunicipioController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MunicipioController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<MunicipioDto>>> Get()
    {
        var municipios = await _unitOfWork.Municipios.GetAllAsync();
        return this._mapper.Map<List<MunicipioDto>>(municipios);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<MunicipioDto>> Get(int id)
    {
        var municipio = await _unitOfWork.Municipios.GetByIdAsync(id);
        if (municipio == null){
            return NotFound();
        }
        return this._mapper.Map<MunicipioDto>(municipio);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Municipio>> Post(MunicipioDto municipioDto)
    {
        var municipio = this._mapper.Map<Municipio>(municipioDto);
        this._unitOfWork.Municipios.Add(municipio);
        await _unitOfWork.SaveAsync();
        if(municipio == null)
        {
            return BadRequest();
        }
        municipioDto.Id = municipio.Id;
        return CreatedAtAction(nameof(Post), new {id = municipioDto.Id}, municipioDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<MunicipioDto>> Put(int id, [FromBody]MunicipioDto municipioDto){
        if(municipioDto == null)
        {
            return NotFound();
        }
        var municipio = this._mapper.Map<Municipio>(municipioDto);
        _unitOfWork.Municipios.Update(municipio);
        await _unitOfWork.SaveAsync();
        return municipioDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var municipio = await _unitOfWork.Municipios.GetByIdAsync(id);
        if(municipio == null)
        {
            return NotFound();
        }
        _unitOfWork.Municipios.Remove(municipio);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


