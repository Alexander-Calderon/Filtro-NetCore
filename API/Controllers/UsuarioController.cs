using API.Dtos;
using API.Helpers;
using API.Services;
using AutoMapper;
using Dominio.Entidades;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    
public class UsuarioController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    public UsuarioController(IUserService userService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpPost("Registro")]
    public async Task<IActionResult> RegisterAsync(RegisterDto model)
    {
        var result = await _userService.RegisterAsync(model);
        return Ok(result);
    }

    [HttpPost("Token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto model)
    {
        var result = await _userService.GetTokenAsync(model);
        
        return Ok(result);
    }

    [HttpPost("AgregarRol")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto model)
    {
        var result = await _userService.AddRoleAsync(model);
        return Ok(result);
    }
    
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        var response = await _userService.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
            SetRefreshTokenInCookie(response.RefreshToken);
        return Ok(response);
    }

    
    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(10),
        };
        Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
    }

    


    /* [HttpGet]
    [ApiVersion("1.1")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<UsuarioDto>>> Get([FromQuery] Params aParams)
    {
        var (totalRegistros, registros) = await _unitOfWork.Usuarios.GetAllAsync(aParams.PageIndex,aParams.PageSize,aParams.Search);
        var lista = _mapper.Map<List<UsuarioDto>>(registros);
        return new Pager<UsuarioDto>(lista,totalRegistros,aParams.PageIndex,aParams.PageSize,aParams.Search);
    } */
    
    /* [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> Put(int id,[FromBody] UsuarioPDto usuarioPDto)
    {
        if (usuarioPDto == null|| id != usuarioPDto.Id)
        {
            return BadRequest();
        }
        var usuarioExiste = await _unitOfWork.Usuarios.GetByIdAsync(id);

        if (usuarioExiste == null)
        {
            return NotFound();
        }
        _mapper.Map(usuarioPDto, usuarioExiste);
        _unitOfWork.Usuarios.Update(usuarioExiste);
        await _unitOfWork.SaveAsync();

        return "Usuario Actualizado con Éxito!";
    }  */

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador, Gerente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete(int id)
    {
        var usuario = await _unitOfWork.Usuarios.GetByIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        _unitOfWork.Usuarios.Remove(usuario);
        await _unitOfWork.SaveAsync();
        return Ok(new { message = $"El usuario {id} se eliminó con éxito." });
    }
}
