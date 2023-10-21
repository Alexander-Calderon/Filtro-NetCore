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
public class ClienteController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ClienteController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ClienteDto>>> Get()
    {
        var clientes = await _unitOfWork.Clientes.GetAllAsync();
        return this._mapper.Map<List<ClienteDto>>(clientes);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClienteDto>> Get(int id)
    {
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
        if (cliente == null){
            return NotFound();
        }
        return this._mapper.Map<ClienteDto>(cliente);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cliente>> Post(ClienteDto clienteDto)
    {
        var cliente = this._mapper.Map<Cliente>(clienteDto);
        this._unitOfWork.Clientes.Add(cliente);
        await _unitOfWork.SaveAsync();
        if(cliente == null)
        {
            return BadRequest();
        }
        clienteDto.Id = cliente.Id;
        return CreatedAtAction(nameof(Post), new {id = clienteDto.Id}, clienteDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<ClienteDto>> Put(int id, [FromBody]ClienteDto clienteDto){
        if(clienteDto == null)
        {
            return NotFound();
        }
        var cliente = this._mapper.Map<Cliente>(clienteDto);
        _unitOfWork.Clientes.Update(cliente);
        await _unitOfWork.SaveAsync();
        return clienteDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var cliente = await _unitOfWork.Clientes.GetByIdAsync(id);
        if(cliente == null)
        {
            return NotFound();
        }
        _unitOfWork.Clientes.Remove(cliente);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


