CREATE TABLE group_subject (
    code_group TEXT REFERENCES info_group(code_group),
    code_subject TEXT REFERENCES subject(code_subject),
    PRIMARY KEY (code_group, code_subject)
);

GRANT ALL PRIVILEGES ON TABLE subject TO school_user;