create database CollegeLoginPortal

create table Student(
	RegdNo int primary key,
	Name varchar(70),
	DOB varchar(20),
	Gender varchar(20),
	Address varchar(50),
);

ALTER TABLE Student
ADD CourseId int;

ALTER TABLE Student
ADD CONSTRAINT FK_Student_Course
FOREIGN KEY (CourseId)
REFERENCES Course(CourseId);

create table Course(
	CourseId int primary key,
	CourseName varchar(30),
	Duration int,
	Domain varchar(30)
)

create table Faculty(
	FacultyId int primary key,
	Name varchar(70),
	DOB varchar(20),
	Gender varchar(20),
	Address varchar(50),
)

ALTER TABLE Faculty
ADD CourseId int;

ALTER TABLE Faculty
ADD CONSTRAINT FK_Faculty_Course
FOREIGN KEY (CourseId)
REFERENCES Course(CourseId);

Insert into Faculty(FacultyId, Name, DOB, Gender, Address)
Values
(1, 'Alex'   , '1/04/1998', 'Male' , 'New York, USA'),
(2, 'Bob'    , '28/02/1996', 'Male' , 'New York, USA'),
(3, 'Maerly' , '15/10/1989', 'Female' , 'New York, USA'),
(4, 'Smith'  , '9/09/1999', 'Male' , 'New York, USA');

Select * from Faculty

insert into Course values
(101,'BTech',4,'Engineering'),
(102,'MTech',3,'Engineering'),
(103,'BBA',3,'Management'),
(104,'MBA',2,'Management'),
(105,'B.Arch',4,'Architecture'),
(106,'B.Plan',4,'Architecture'),
(107,'M.Arch',2,'Architecture'),
(108,'Fashion Technology',3,'Design'),
(109,'Textile Design',3,'Design'),
(110,'BA LLB',4,'Law'),
(111,'BCom LLB',4,'Law'),
(112,'BMM',3,'Media'),
(113,'BA Mass Communication',3,'Media'),
(114,'BMS',3,'Business Studies'),
(115,'Managerial Economics',3,'Economics'),
(116,'BSc',3,'Applied Science'),
(117,'MSc',2,'Applied Science'),
(118,'Philosophy',3,'Humanities'),
(119,'Psychology',3,'Humanities'),
(120,'MBBS',5,'Medical'),
(121,'BSc Nursing',4,'Medical');

Select * from Course