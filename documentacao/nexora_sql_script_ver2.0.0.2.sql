create table pessoa (
    id char(36) primary key,
    situacao varchar(7) not null check(situacao in ('ativo', 'inativo')),
    codigo int not null unique,
    telefone char(11) not null unique,
    email varchar(255) unique
);

create table pessoa_fisica (
    id_pessoa char(36) primary key,
    nome varchar(255) not null,
    cpf char(11) not null unique,
    data_nascimento date not null,
    foreign key (id_pessoa) references pessoa (id)
);

create table pessoa_juridica (
    id_pessoa char (36) primary key,
    razao_social varchar (255) not null,
    nome_fantasia varchar (255) not null,
    cnpj char (14) not null unique,
    inscricao_municipal int not null unique,
    foreign key (id_pessoa) references pessoa (id)
);

create table cliente (
    id_pessoa char(36) primary key,
    cep char(8) not null,
    endereco varchar(100) not null,
    numero int not null,
    bairro varchar(100) not null,
    cidade varchar(100) not null,
    foreign key (id_pessoa) references pessoa (id)
);

create table funcionario (
    id_pessoa char(36) primary key,
    login varchar(255) not null unique,
    senha varchar(255) not null,
    salario DECIMAL(10,2) not null check(salario > 0.0),
    cargo varchar(255) not null,
    admissao date not null,
    horario_inicio_carga_horaria time not null,
    horario_fim_carga_horaria time not null,
    check (horario_inicio_carga_horaria < horario_fim_carga_horaria),
    foreign key (id_pessoa) references pessoa (id)
);

create table produto (
    id char (36) primary key,
    codigo int not null unique,
    quantidade_estoque int not null check(quantidade_estoque >= 0),
    estoque_minimo int check (estoqueMinimo >= 0 or estoqueMinimo is null)),
    codigo_barras varchar(255) unique,
    ncm char(8) not null unique,
    situacao varchar(7) not null check(situacao in ('ativo', 'inativo')),
    valor_custo decimal(10,2),
    valor_venda decimal(10,2)
);

create table cupom (
  	id char (36) primary key,
  	codigo int not null unique,
  	prazo_validade datetime not null,
  	valor_minimo_utilizacao decimal(10,2) not null,
  	situacao varchar(20) not null
);

create table cupom_valor_inteiro (
    id_cupom char(36) primary key,
    valor_numerico decimal(10,2) not null check(valor_numerico > 0),
    foreign key (id_cupom) references cupom (id)
);

create table cupom_valor_porcentagem (
    id_cupom char(36) primary key,
    valor_porcentagem decimal(5,2) not null check(valor_porcentagem > 0),
    foreign key (id_cupom) references cupom (id)
);

create table venda (
    id char(36) primary key,
    id_cliente varchar(255) not null,
  	id_funcionario varchar(255) not null,
  	id_cupom varchar(255),
  	codigo int not null unique,
	valor_total decimal(10,2) not null check (valor_total > 0),
	situacao varchar(20) not null,
  	foreign key (id_cliente) references cliente(id_pessoa),
  	foreign key (id_funcionario) references funcionario (id_pessoa),
	foreign key (id_cupom) references cupom (id)
);

create table item_venda (
    id_venda char(36) not null,
    id_produto varchar(255) not null,
    quantidade int not null check (quantidade > 0),
    subtotal decimal(10,2) not null check (valor_total > 0),
    primary key (id_venda, id_produto),
    foreign key(id_venda) references venda(id),
    foreign key(id_produto) references produto(id)
);

create table produto_cupom (
    id_produto char(36) not null,
    id_cupom varchar(255) not null,
    primary key (id_produto, id_cupom),
    foreign key(id_produto) references produto(id),
    foreign key(id_cupom) references cupom(id)
);

create table permissao (
    id int auto_increment primary key,
    tipo varchar(50) not null
);

create table permissao_funcionario (
    id_permissao int not null,
    id_funcionario char(36) not null,
    primary key(id_permissao, id_funcionario),
    foreign key(id_permissao) references permissao(id),
    foreign key(id_funcionario) references funcionario(id)
);

create table log (
    id char(36) primary key,
    id_funcionario varchar(255) not null,
    id_entidade varchar(255) not null,
    tipo char(10) not null,
    descricao text not null,
    data_hora datetime not null,
    foreign key(id_funcionario) references funcionario(id_pessoa)
);

insert into permissao (tipo)
values
    ('REALIZAR_VENDA'),
    ('EDITAR_VENDA'),
    ('ESTORNAR_VENDA'),
    ('PESQUISAR_VENDA'),
    ('GERAR_RELATORIOS'),
    ('EDITAR_CUPOM'),
    ('INATIVAR_CUPOM'),
    ('CADASTRAR_CUPOM'),
    ('PESQUISAR_CUPOM'),
    ('CADASTRAR_CLIENTE'),
    ('EDITAR_CLIENTE'),
    ('INATIVAR_CLIENTE'),
    ('PESQUISAR_CLIENTE'),
    ('INATIVAR_PRODUTO'),
    ('CADASTRAR_PRODUTO'),
    ('PESQUISAR_PRODUTO'),
    ('EDITAR_FUNCIONARIO'),
    ('CADASTRAR_FUNCIONARIO'),
    ('INATIVAR_FUNCIONARIO'),
    ('PESQUISAR_FUNCIONARIO'),
    ('DEFINIR_MARGEM_LUCRO');
