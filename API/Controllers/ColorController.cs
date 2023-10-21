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
public class ColorController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ColorController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ColorDto>>> Get()
    {
        var colors = await _unitOfWork.Colores.GetAllAsync();
        return this._mapper.Map<List<ColorDto>>(colors);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ColorDto>> Get(int id)
    {
        var color = await _unitOfWork.Colores.GetByIdAsync(id);
        if (color == null){
            return NotFound();
        }
        return this._mapper.Map<ColorDto>(color);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Color>> Post(ColorDto colorDto)
    {
        var color = this._mapper.Map<Color>(colorDto);
        this._unitOfWork.Colores.Add(color);
        await _unitOfWork.SaveAsync();
        if(color == null)
        {
            return BadRequest();
        }
        colorDto.Id = color.Id;
        return CreatedAtAction(nameof(Post), new {id = colorDto.Id}, colorDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ColorDto>> Put(int id, [FromBody]ColorDto colorDto){
        if(colorDto == null)
        {
            return NotFound();
        }
        var color = this._mapper.Map<Color>(colorDto);
        _unitOfWork.Colores.Update(color);
        await _unitOfWork.SaveAsync();
        return colorDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var color = await _unitOfWork.Colores.GetByIdAsync(id);
        if(color == null)
        {
            return NotFound();
        }
        _unitOfWork.Colores.Remove(color);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


