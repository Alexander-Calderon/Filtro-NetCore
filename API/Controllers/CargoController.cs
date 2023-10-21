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
public class CargoController : BaseApiController
{

    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CargoController(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }



    /* RUTAS */
    
    // # CRUD Base

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<CargoDto>>> Get()
    {
        var cargos = await _unitOfWork.Cargos.GetAllAsync();
        return this._mapper.Map<List<CargoDto>>(cargos);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CargoDto>> Get(int id)
    {
        var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
        if (cargo == null){
            return NotFound();
        }
        return this._mapper.Map<CargoDto>(cargo);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Cargo>> Post(CargoDto cargoDto)
    {
        var cargo = this._mapper.Map<Cargo>(cargoDto);
        this._unitOfWork.Cargos.Add(cargo);
        await _unitOfWork.SaveAsync();
        if(cargo == null)
        {
            return BadRequest();
        }
        cargoDto.Id = cargo.Id;
        return CreatedAtAction(nameof(Post), new {id = cargoDto.Id}, cargoDto);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<ActionResult<CargoDto>> Put(int id, [FromBody]CargoDto cargoDto){
        if(cargoDto == null)
        {
            return NotFound();
        }
        var cargo = this._mapper.Map<Cargo>(cargoDto);
        _unitOfWork.Cargos.Update(cargo);
        await _unitOfWork.SaveAsync();
        return cargoDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id){
        var cargo = await _unitOfWork.Cargos.GetByIdAsync(id);
        if(cargo == null)
        {
            return NotFound();
        }
        _unitOfWork.Cargos.Remove(cargo);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }


    
}


