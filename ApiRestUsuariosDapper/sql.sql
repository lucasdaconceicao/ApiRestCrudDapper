create database ApiRestUsuarios;
use ApiRestUsuarios;
create table Usuarios(
	id integer primary key identity,
    nome varchar(100),
    senha varchar(100),
    status varchar(15)
);

-- STRING DE CONEXAO
Server=DESKTOP-LUCAS\\SQLEXPRESS;Database=ApiRestUsuarios;User Id=sa;Password=root;