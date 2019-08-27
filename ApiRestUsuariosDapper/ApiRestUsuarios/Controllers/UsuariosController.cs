using ApiRestUsuarios.Models;
using ApiRestUsuarios.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestUsuarios.Controllers
{
    [Route("api/[Controller]")]
    public class UsuariosController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepositorio;
        public UsuariosController(IUsuarioRepository usuarioRepo)
        {
            _usuarioRepositorio = usuarioRepo;
        }
        [HttpGet]
        public List<Usuario> ListarTodos()
        {
            return _usuarioRepositorio.ListarTodos();
        }
        [HttpGet("{id}",Name="GetUsuario")]
        public IActionResult ObterPorId(int id)
        {
            var usuario= _usuarioRepositorio.ObterPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return new ObjectResult(usuario);
        }
        [HttpPost]
        public IActionResult Criar([FromBody]Usuario usuario){
            if(usuario==null)
                return BadRequest();
            _usuarioRepositorio.Criar(usuario);
            return CreatedAtRoute("GetUsuario",new {id=usuario.id},usuario);
        }
        [HttpPut]
        public IActionResult Atualizar([FromBody]Usuario usuario)
        {
            if(usuario==null){
                return BadRequest();
            }
            var _usuario=_usuarioRepositorio.ObterPorId(usuario.id);
            if(_usuario==null){
                return BadRequest();
            }
            _usuario.nome=usuario.nome;
            _usuario.status=usuario.status;
            _usuarioRepositorio.Atualizar(_usuario);
            return Ok();
        }
        [HttpDelete("{id}")]
        public IActionResult Remover(int id){
            var usuario=_usuarioRepositorio.ObterPorId(id);
            if(usuario==null){
                return BadRequest();
            }
            _usuarioRepositorio.Remover(id);
            return Ok();

        }
        [HttpPost("{id}")]
        public IActionResult Login(int id,[FromBody]Usuario usuario){
            if(usuario==null){
                return BadRequest();
            }
            var resutado =_usuarioRepositorio.Login(usuario);
            if(resutado==null){
                return BadRequest();
            }
            return Ok();
        }
    }
} 