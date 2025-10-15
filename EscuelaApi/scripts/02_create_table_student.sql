CREATE TABLE student (
    dni VARCHAR(15) PRIMARY KEY,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(100) NOT NULL,
    birth_date DATE NOT NULL,
    parent_emergency_phone1 VARCHAR(20) NOT NULL,
    parent_emergency_phone2 VARCHAR(20)
);

GRANT ALL PRIVILEGES ON TABLE student TO school_user;