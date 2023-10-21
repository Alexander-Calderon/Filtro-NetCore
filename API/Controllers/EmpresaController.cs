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
public class EmpresaController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public EmpresaController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<EmpresaDto>>> Get()
    {
        var empresas = await _unitOfWork.Empresas.GetAllAsync();
        return this._mapper.Map<List<EmpresaDto>>(empresas);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmpresaDto>> Get(int id)
    {
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
        if (empresa == null){
            return NotFound();
        }
        return this._mapper.Map<EmpresaDto>(empresa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Empresa>> Post(EmpresaDto empresaDto)
    {
        var empresa = this._mapper.Map<Empresa>(empresaDto);
        this._unitOfWork.Empresas.Add(empresa);
        await _unitOfWork.SaveAsync();
        if(empresa == null)
        {
            return BadRequest();
        }
        empresaDto.Id = empresa.Id;
        return CreatedAtAction(nameof(Post), new {id = empresaDto.Id}, empresaDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<EmpresaDto>> Put(int id, [FromBody]EmpresaDto empresaDto){
        if(empresaDto == null)
        {
            return NotFound();
        }
        var empresa = this._mapper.Map<Empresa>(empresaDto);
        _unitOfWork.Empresas.Update(empresa);
        await _unitOfWork.SaveAsync();
        return empresaDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var empresa = await _unitOfWork.Empresas.GetByIdAsync(id);
        if(empresa == null)
        {
            return NotFound();
        }
        _unitOfWork.Empresas.Remove(empresa);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


