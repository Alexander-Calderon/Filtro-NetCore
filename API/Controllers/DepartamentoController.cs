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
public class DepartamentoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DepartamentoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<DepartamentoDto>>> Get()
    {
        var departamentos = await _unitOfWork.Departamentos.GetAllAsync();
        return this._mapper.Map<List<DepartamentoDto>>(departamentos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartamentoDto>> Get(int id)
    {
        var departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if (departamento == null){
            return NotFound();
        }
        return this._mapper.Map<DepartamentoDto>(departamento);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Departamento>> Post(DepartamentoDto departamentoDto)
    {
        var departamento = this._mapper.Map<Departamento>(departamentoDto);
        this._unitOfWork.Departamentos.Add(departamento);
        await _unitOfWork.SaveAsync();
        if(departamento == null)
        {
            return BadRequest();
        }
        departamentoDto.Id = departamento.Id;
        return CreatedAtAction(nameof(Post), new {id = departamentoDto.Id}, departamentoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<DepartamentoDto>> Put(int id, [FromBody]DepartamentoDto departamentoDto){
        if(departamentoDto == null)
        {
            return NotFound();
        }
        var departamento = this._mapper.Map<Departamento>(departamentoDto);
        _unitOfWork.Departamentos.Update(departamento);
        await _unitOfWork.SaveAsync();
        return departamentoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var departamento = await _unitOfWork.Departamentos.GetByIdAsync(id);
        if(departamento == null)
        {
            return NotFound();
        }
        _unitOfWork.Departamentos.Remove(departamento);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


