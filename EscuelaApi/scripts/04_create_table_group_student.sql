CREATE TABLE group_student (
    code_group VARCHAR(50),
    student_dni VARCHAR(15),
    CONSTRAINT pk_group_student PRIMARY KEY (code_group, student_dni),
    CONSTRAINT fk_student FOREIGN KEY (student_dni)
        REFERENCES student(dni)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT fk_group FOREIGN KEY (code_group)
        REFERENCES info_group(code_group)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GRANT ALL PRIVILEGES ON TABLE group_student TO school_user;
