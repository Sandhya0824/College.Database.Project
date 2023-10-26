create table Course(
	CourseId int primary key,
	CourseName varchar(30),
	Duration int,
	Domain varchar(30)
);
create table Student(
	RegdNo int primary key,
	Name varchar(70),
	DOB varchar(20),
	Gender varchar(20),
	Address varchar(50),
	CourseId int Foreign Key References Course(CourseId)
);

create table Faculty(
	FacultyId int primary key,
	Name varchar(70),
	DOB varchar(20),
	Gender varchar(20),
	Address varchar(50),
	CourseId int Foreign Key References Course(CourseId)
);

insert into Student(RegdNo,Name,DOB,Address,Gender) values(2041018156,'Yash Rout','03/10/2002','Male','Puri');

insert into Student values(2041018193,'Manaswini Ray','30/09/2002','Female','Bhubaneswar','5012');

insert into Course values(5012,'BTech',4,'Engineering');


