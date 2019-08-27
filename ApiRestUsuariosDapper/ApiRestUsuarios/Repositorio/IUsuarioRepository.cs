using ApiRestUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiRestUsuarios.Repositorio
{
    public interface IUsuarioRepository
    {
        void Criar(Usuario usuario);
        List<Usuario> ListarTodos();
        Usuario ObterPorId(int id);
        void Remover(int id);
        void Atualizar(Usuario usuario);

    }
}
