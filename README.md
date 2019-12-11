# KP71_Atamaniuk_Oleksii_DB2
[Lab 1](https://github.com/FireAndBlood12/DB_LAb1)
[Lab 3](https://github.com/FireAndBlood12/DB_lab3)

Лабораторна робота № 2.

Ознайомлення з базовими операціями СУБД PostgreSQL

КП-71 Атаманюк Олексій Віталійович

Варіант 1 - Університет

Сутності:

1. Група (Group)

CREATE TABLE public."groups"
(

    id integer NOT NULL DEFAULT nextval('"Group_Id_seq"'::regclass),
    code text COLLATE pg_catalog."default" NOT NULL,
    entrance_year date NOT NULL,
    CONSTRAINT "Group_pkey" PRIMARY KEY (id)
        
)

2. Предмет (Subject)

CREATE TABLE public."subjects"
(

     id integer NOT NULL DEFAULT nextval('"Subject_Id_seq"'::regclass),
    title text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "Subject_pkey" PRIMARY KEY (id)
    
)

3. Студент (Student)

CREATE TABLE public."students"
(

    id integer NOT NULL DEFAULT nextval('"Student_Id_seq"'::regclass),
    firstname text COLLATE pg_catalog."default" NOT NULL,
    lastname text COLLATE pg_catalog."default" NOT NULL,
    birthday date NOT NULL,
    group_id integer NOT NULL,
    CONSTRAINT "Student_pkey" PRIMARY KEY (id),
    CONSTRAINT students_group_id_fkey FOREIGN KEY (group_id)
        REFERENCES public.groups (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        
)

4. Викладач (Teacher)

CREATE TABLE public."teachers"
(

    id integer NOT NULL DEFAULT nextval('"Teacher_Id_seq"'::regclass),
    firstname text COLLATE pg_catalog."default" NOT NULL,
    lastname text COLLATE pg_catalog."default" NOT NULL,
    experience integer NOT NULL,
    main_subject_id integer NOT NULL,
    CONSTRAINT "Teacher_pkey" PRIMARY KEY (id),
    CONSTRAINT teachers_main_subject_id_fkey FOREIGN KEY (main_subject_id)
        REFERENCES public.subjects (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
    
)

5. Оцінка (Mark)

CREATE TABLE public."marks"
(

    id integer NOT NULL DEFAULT nextval('"Marks_Id_seq"'::regclass),
    mark integer NOT NULL,
    date date NOT NULL,
    student_id integer NOT NULL,
    teacher_id integer NOT NULL,
    subject_id integer NOT NULL,
    CONSTRAINT "Marks_pkey" PRIMARY KEY (id),
    CONSTRAINT marks_student_id_fkey FOREIGN KEY (student_id)
        REFERENCES public.students (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT marks_subject_id_fkey FOREIGN KEY (subject_id)
        REFERENCES public.subjects (id) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT marks_teacher_id_fkey FOREIGN KEY (teacher_id)
        REFERENCES public.teachers (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
        
)
