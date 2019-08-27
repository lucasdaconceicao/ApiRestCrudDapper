using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiRestUsuarios.Models;
using ApiRestUsuarios.Util;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace ApiRestUsuarios.Repositorio
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IConfiguration _configuracoes;
        public UsuarioRepository (IConfiguration config){
            _configuracoes=config;
        }

        public void Atualizar(Usuario usuario)
        {
            using(SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("DefaultConnection"))){
                conexao.Execute(
                    @"UPDATE USUARIOS
                    SET 
                        nome=@nome,
                        senha=@senha,
                        status=@status
                    WHERE id=@id;",
                    usuario);
                }
        }

        public void Criar(Usuario usuario)
        {
           using(SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("DefaultConnection"))){
                Criptografia cripto = new Criptografia(); 
                usuario.senha=cripto.RetornarMD5(usuario.senha);
                conexao.Execute(
                    @"INSERT INTO USUARIOS
                        (nome,
                        senha,
                        status)
                    VALUES
                        (@nome,
                        @senha,
                        @status);",
                    usuario);
                }
        }

        public List<Usuario> ListarTodos()
        {
            using(SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("DefaultConnection"))){
                return conexao.Query<Usuario>(
                    @"SELECT
                        *
                    FROM USUARIOS").ToList();
            }
        }

        public Usuario ObterPorId(int id)
        {
            using(SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("DefaultConnection"))){
                return conexao.QueryFirstOrDefault<Usuario>(
                    @"SELECT
                        *
                    FROM USUARIOS
                    WHERE id=@codigo",
                    new {codigo=id});
            }
        }  
        public void Remover(int id)
        {
            using(SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("DefaultConnection"))){
                conexao.Execute(
                    @"DELETE FROM USUARIOS 
                    WHERE
                        id=@codigo",
                    new {codigo=id});
            }
        }
         public Usuario Login(Usuario usuario)
        {
            using(SqlConnection conexao = new SqlConnection(_configuracoes.GetConnectionString("DefaultConnection"))){
                Criptografia cripto = new Criptografia(); 
                usuario.senha=cripto.RetornarMD5(usuario.senha);
                return conexao.QueryFirstOrDefault<Usuario>(
                    @"SELECT
                        senha
                    FROM USUARIOS
                    WHERE nome=@nome AND 
                    senha=@senha",
                   usuario);
            }
        } 
    }
}
