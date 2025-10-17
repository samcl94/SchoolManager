CREATE TABLE subject (
    code_subject VARCHAR(50) PRIMARY KEY,
    label VARCHAR(100) NOT NULL
);

GRANT ALL PRIVILEGES ON TABLE subject TO school_user;