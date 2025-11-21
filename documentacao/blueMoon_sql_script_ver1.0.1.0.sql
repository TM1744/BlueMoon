CREATE DATABASE IF NOT EXISTS bluemoon
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_unicode_ci;
    
CREATE TABLE Produtos (
    id CHAR(36) NOT NULL,
    descricao VARCHAR(100) NOT NULL,
    marca VARCHAR(50) NOT NULL,
    codigo INT NOT NULL,
    quantidade_estoque INT NOT NULL,
    quantidade_estoque_minimo INT NOT NULL,
    ncm CHAR(8) NOT NULL,
    codigo_barras VARCHAR(13) NOT NULL,
    situacao INT NOT NULL,
    valor_custo DECIMAL(11,2) NOT NULL,
    valor_venda DECIMAL(11,2) NOT NULL,
    margem_lucro DECIMAL(5,2) NOT NULL,
    nome VARCHAR(70) NOT NULL,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE Pessoas (
    id CHAR(36) NOT NULL,
    tipo INT NOT NULL,
    situacao INT NOT NULL,
    codigo INT NOT NULL,
    email VARCHAR(100) NOT NULL,
    nome VARCHAR(100) NOT NULL,
    documento VARCHAR(14) NOT NULL,
    inscricao_municipal VARCHAR(12) NOT NULL,
    inscricao_estadual VARCHAR(13) NOT NULL,
    bairro VARCHAR(70) NOT NULL,
    cep CHAR(8) NOT NULL,
    cidade VARCHAR(50) NOT NULL,
    complemento VARCHAR(40) NOT NULL,
    estado INT NOT NULL DEFAULT 0,
    logradouro VARCHAR(100) NOT NULL,
    numero VARCHAR(10) NOT NULL,
    telefone VARCHAR(11) NOT NULL,
    PRIMARY KEY (id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE Usuarios (
    id CHAR(36) NOT NULL,
    id_pessoa CHAR(36) NOT NULL,
    codigo INT NOT NULL,
    situacao INT NOT NULL,
    login VARCHAR(100) NOT NULL,
    senha CHAR(64) NOT NULL,
    cargo INT NOT NULL,
    salario DECIMAL(11,2) NOT NULL,
    admissao DATE NOT NULL,
    horario_inicio_carga_horaria TIME(6) NOT NULL,
    horario_fim_carga_horaria TIME(6) NOT NULL,
    PRIMARY KEY (id),
    INDEX idx_id_pessoa (id_pessoa),
    CONSTRAINT fk_usuario_pessoa FOREIGN KEY (id_pessoa) REFERENCES Pessoas(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

CREATE TABLE Vendas (
    id CHAR(36) NOT NULL,
    id_pessoa CHAR(36) NOT NULL,
    id_usuario CHAR(36) NOT NULL,
    codigo INT NOT NULL,
    situacao INT NOT NULL,
    valor_total DECIMAL(11,2) NOT NULL,
    data_faturamento DATETIME(6) NOT NULL,
    data_abertura DATETIME(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
    PRIMARY KEY (id),
    INDEX idx_id_pessoa (id_pessoa),
    INDEX idx_id_usuario (id_usuario),
    CONSTRAINT fk_venda_pessoa FOREIGN KEY (id_pessoa) REFERENCES Pessoas(id),
    CONSTRAINT fk_venda_usuario FOREIGN KEY (id_usuario) REFERENCES Usuarios(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
 
CREATE TABLE ItemVendas (
    id CHAR(36) NOT NULL,
    id_produto CHAR(36) NOT NULL,
    produto_nome VARCHAR(70) NOT NULL,
    produto_marca VARCHAR(50) NOT NULL,
    produto_codigo INT NOT NULL,
    produto_valor_venda DECIMAL(11,2) NOT NULL,
    quantidade INT NOT NULL,
    subtotal DECIMAL(11,2) NOT NULL,
    id_venda CHAR(36) NOT NULL,
    PRIMARY KEY (id),
    INDEX idx_id_produto (id_produto),
    INDEX idx_id_venda (id_venda),
    CONSTRAINT fk_itemvenda_produto FOREIGN KEY (id_produto) REFERENCES Produtos(id),
    CONSTRAINT fk_itemvenda_venda FOREIGN KEY (id_venda) REFERENCES Vendas(id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
