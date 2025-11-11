CREATE TABLE info_group (
    code_group VARCHAR(50) PRIMARY KEY,
    label VARCHAR(100) NOT NULL,
    ordre INTEGER NOT NULL
);

GRANT ALL PRIVILEGES ON TABLE info_group TO school_user;