CREATE TABLE IF NOT EXISTS clients (
    id UUID PRIMARY KEY,                -- identificador único no formato UUID
    name VARCHAR(100) NOT NULL,         -- nome do cliente
    surname VARCHAR(100) NOT NULL,      -- sobrenome
    email VARCHAR(100) UNIQUE NOT NULL, -- email de contato
    birthdate DATE NOT NULL,            -- data de nascimento
    is_active BOOLEAN DEFAULT TRUE,     -- cliente ativo ou inativo (delete lógico)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, -- data de criação
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP  -- data da última atualização
);
