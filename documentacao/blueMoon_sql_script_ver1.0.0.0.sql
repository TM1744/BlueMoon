create table pessoa (
    id char(36) primary key,
    tipo varchar(20) not null,
    situacao varchar(7) not null check(situacao in ('ATIVO', 'INATIVO')) default 'ATIVO',
    codigo char(6) not null unique,
    telefone char(11) not null unique,
    email varchar(100) unique null,
    nome varchar(100) null,
    cpf_cnpj varchar (14) not null unique,
    data_nascimento date null,
    razao_social varchar(100) unique null,
    nome_fantasia varchar(100) unique null,
    iscricao_municipal varchar(20) unique null,
    cep char(8) not null,
    endereco varchar(100) not null,
    numero_endereco varchar(5) not null,
    bairro varchar(70) not null,
    cidade varchar(50) not null,
    estado char(2) not null
);

create table usuario (
   id_pessoa char(36) primary key,
   login varchar(100) not null unique,
   senha varchar(26) not null,
   salario decimal(10,2) not null check (salario> 0.0),
   cargo varchar(50) not null,
   admissao date not null,
   horario_inicio_carga_horaria time not null,
   horario_fim_carga_horaria time not null,
   check(horario_inicio_carga_horaria < horario_fim_carga_horaria),
   foreign key (id_pessoa) references pessoa (id)
);

create table produto (
    id char (36) primary key,
    descricao varchar(70) not null,
    marca varchar(50) not null,
    fornecedor varchar(70) not null,
    codigo char(6) not null unique,
    quantidade_estoque int(3) not null check(quantidade_estoque >= 0),
    estoque_minimo int(3) not null check (estoque_minimo >= 0),
    codigo_barras varchar(128) unique,
    ncm char(8) not null,
    situacao varchar(7) not null check(situacao in ('ATIVO', 'INATIVO')) default 'ATIVO',
    valor_custo decimal(10,2) not null check(valor_custo >= 0),
    valor_venda decimal(10,2) not null check(valor_venda > 0)
);

create table cupom (
  	id char (36) primary key,
  	tipo varchar(11) not null check (tipo = 'NUMERICO' or tipo = 'PORCENTAGEM'),
  	codigo char(6) not null unique,
  	prazo_validade datetime not null,
  	valor_minimo_utilizacao decimal(10,2) null check(valor_minimo_utilizacao > 0),
  	situacao varchar(7) not null check(situacao in ('ATIVO', 'INATIVO')) default 'ATIVO',
  	valor_numerico decimal(10,2) null,
  	valor_porcentagem decimal(5,2) null,
  	check ((tipo = 'NUMERICO' and valor_numerico > 0 and valor_porcentagem is null) or (tipo = 'PORCENTAGEM' and valor_porcentagem > 0 and valor_numerico is null))
);

create table venda (
    id char(36) primary key,
    id_cliente char(36) not null,
  	id_funcionario char(36) not null,
  	id_cupom char(36) null,
  	codigo char(6) not null unique,
	valor_total decimal(10,2) not null check (valor_total > 0),
	situacao varchar(20) not null,
  	foreign key (id_cliente) references pessoa(id),
  	foreign key (id_funcionario) references usuario(id_pessoa),
	foreign key (id_cupom) references cupom(id)
);

create table item_venda (
    id_venda char(36) not null,
    id_produto char(36) not null,
    quantidade int not null check (quantidade > 0),
    subtotal decimal(10,2) not null check (subtotal > 0),
    primary key (id_venda, id_produto),
    foreign key(id_venda) references venda(id),
    foreign key(id_produto) references produto(id)
);

create table produto_cupom (
    id_produto char(36) not null,
    id_cupom varchar(36) not null,
    primary key (id_produto, id_cupom),
    foreign key(id_produto) references produto(id),
    foreign key(id_cupom) references cupom(id)
);
