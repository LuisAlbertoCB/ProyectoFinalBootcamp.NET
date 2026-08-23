-- Ejecutar conectado a la base "accountsdb" (creada con 01_create_database.sql).
-- psql: psql -h localhost -p 5432 -U postgres -d accountsdb -f 02_create_table_and_seed.sql

CREATE TABLE IF NOT EXISTS clientes (
    id                  UUID PRIMARY KEY,
    nombre              VARCHAR(80)  NOT NULL,
    apellido            VARCHAR(80)  NOT NULL,
    numero_documento    VARCHAR(30)  NOT NULL,
    correo_electronico  VARCHAR(150) NOT NULL,
    telefono            VARCHAR(30)  NOT NULL,
    fecha_nacimiento    DATE         NOT NULL,
    estado              VARCHAR(20)  NOT NULL,
    fecha_creacion      TIMESTAMPTZ  NOT NULL DEFAULT NOW(),
    fecha_actualizacion TIMESTAMPTZ  NOT NULL DEFAULT NOW(),
    CONSTRAINT ix_clientes_numero_documento UNIQUE (numero_documento),
    CONSTRAINT ix_clientes_correo_electronico UNIQUE (correo_electronico)
);

CREATE TABLE IF NOT EXISTS cuentas (
    id                  UUID          PRIMARY KEY,
    cliente_id          UUID          NOT NULL REFERENCES clientes (id) ON DELETE RESTRICT,
    numero_cuenta       VARCHAR(20)   NOT NULL,
    tipo_cuenta         VARCHAR(20)   NOT NULL,
    moneda              VARCHAR(3)    NOT NULL,
    saldo               NUMERIC(18,2) NOT NULL DEFAULT 0,
    limite_sobregiro    NUMERIC(18,2) NOT NULL DEFAULT 0,
    estado              VARCHAR(20)   NOT NULL,
    fecha_creacion      TIMESTAMPTZ   NOT NULL DEFAULT NOW(),
    fecha_actualizacion TIMESTAMPTZ   NOT NULL DEFAULT NOW(),
    CONSTRAINT ix_cuentas_numero_cuenta UNIQUE (numero_cuenta)
);

CREATE INDEX IF NOT EXISTS ix_cuentas_cliente_id ON cuentas (cliente_id);

INSERT INTO clientes (id, nombre, apellido, numero_documento, correo_electronico, telefono, fecha_nacimiento, estado, fecha_creacion, fecha_actualizacion) VALUES
    ('b1b1b1b1-0001-0001-0001-000000000001', 'Lionel',  'Messi',      '20-30000001-1', 'lionel.messi@example.com',    '+54-9-11-0001', '1987-06-24', 'Activo',   NOW(), NOW()),
    ('b1b1b1b1-0002-0002-0002-000000000002', 'Julian',  'Alvarez',    '20-30000002-2', 'julian.alvarez@example.com',  '+54-9-11-0002', '2000-01-30', 'Activo',   NOW(), NOW()),
    ('b1b1b1b1-0003-0003-0003-000000000003', 'Sebas',   'Caballero',  '20-30000003-3', 'sebas.caballero@example.com', '+54-9-11-0003', '1995-11-03', 'Inactivo', NOW(), NOW())
ON CONFLICT (numero_documento) DO NOTHING;

INSERT INTO cuentas (id, cliente_id, numero_cuenta, tipo_cuenta, moneda, saldo, limite_sobregiro, estado, fecha_creacion, fecha_actualizacion) VALUES
    ('a1a1a1a1-0001-0001-0001-000000000001', 'b1b1b1b1-0001-0001-0001-000000000001', 'CTA-0001', 'Corriente', 'USD', 15000.50, 500.00, 'Activa',   NOW(), NOW()),
    ('a1a1a1a1-0002-0002-0002-000000000002', 'b1b1b1b1-0002-0002-0002-000000000002', 'CTA-0002', 'Ahorro',    'EUR',  8320.00,   0.00, 'Activa',   NOW(), NOW()),
    ('a1a1a1a1-0003-0003-0003-000000000003', 'b1b1b1b1-0003-0003-0003-000000000003', 'CTA-0003', 'Corriente', 'ARS',     0.00,   0.00, 'Inactiva', NOW(), NOW())
ON CONFLICT (numero_cuenta) DO NOTHING;

SELECT * FROM clientes;
SELECT * FROM cuentas;
