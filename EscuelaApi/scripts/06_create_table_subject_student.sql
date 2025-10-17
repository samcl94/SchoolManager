CREATE TABLE subject_student (
    student_dni VARCHAR(15),
    code_subject VARCHAR(50),
    CONSTRAINT pk_subject_student PRIMARY KEY (student_dni, code_subject),
    CONSTRAINT fk_student FOREIGN KEY (student_dni)
        REFERENCES student(dni)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    CONSTRAINT fk_subject FOREIGN KEY (code_subject)
        REFERENCES subject(code_subject)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    ordre INTEGER NOT NULL
);
GRANT ALL PRIVILEGES ON TABLE subject_student TO school_user;
