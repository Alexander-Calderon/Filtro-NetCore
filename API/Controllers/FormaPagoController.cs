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
public class FormaPagoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public FormaPagoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<FormaPagoDto>>> Get()
    {
        var formapagos = await _unitOfWork.FormasPagos.GetAllAsync();
        return this._mapper.Map<List<FormaPagoDto>>(formapagos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FormaPagoDto>> Get(int id)
    {
        var formapago = await _unitOfWork.FormasPagos.GetByIdAsync(id);
        if (formapago == null){
            return NotFound();
        }
        return this._mapper.Map<FormaPagoDto>(formapago);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FormaPago>> Post(FormaPagoDto formapagoDto)
    {
        var formapago = this._mapper.Map<FormaPago>(formapagoDto);
        this._unitOfWork.FormasPagos.Add(formapago);
        await _unitOfWork.SaveAsync();
        if(formapago == null)
        {
            return BadRequest();
        }
        formapagoDto.Id = formapago.Id;
        return CreatedAtAction(nameof(Post), new {id = formapagoDto.Id}, formapagoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<FormaPagoDto>> Put(int id, [FromBody]FormaPagoDto formapagoDto){
        if(formapagoDto == null)
        {
            return NotFound();
        }
        var formapago = this._mapper.Map<FormaPago>(formapagoDto);
        _unitOfWork.FormasPagos.Update(formapago);
        await _unitOfWork.SaveAsync();
        return formapagoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var formapago = await _unitOfWork.FormasPagos.GetByIdAsync(id);
        if(formapago == null)
        {
            return NotFound();
        }
        _unitOfWork.FormasPagos.Remove(formapago);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


