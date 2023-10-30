SELECT TOP (1000) [RegdNo]
      ,[Name]
      ,[DOB]
      ,[Gender]
      ,[Address]
      ,[CourseId]
  FROM [CollegeLoginPortal].[dbo].[Student]

Insert into Faculty Values
(1001, 'Ravi Kumar'      , '15/03/1980', 'Male'   , 'Delhi, India'     ,101),
(1002, 'Priya Sharma'    , '10/12/1985', 'Female' , 'Mumbai, India'    ,102),
(1003, 'Suresh Patel'    , '22/08/1978', 'Male'   , 'Ahmedabad, India' ,103),
(1004, 'Kavita Reddy'    , '28/03/1990', 'Female' , 'Bangalore, India' ,104),
(1005, 'Deepika Verma'   , '25/12/1980', 'Female' , 'Mumbai, India'    ,105),
(1006, 'Meera Gupta'     , '20/09/1983', 'Female' , 'Raipur, India'    ,106),
(1007, 'Arjun Singh'     , '12/07/1978', 'Male'   , 'Ahmedabad, India' ,107),
(1008, 'Aishwarya Desai' , '05/11/1988', 'Female' , 'Goa, India'       ,108),
(1009, 'Karthik Iyer'    , '08/08/1980', 'Male'   , 'Bangalore, India' ,109),
(1010, 'Neha Kapoor'     , '19/02/1982', 'Female' , 'Patna, India'     ,110),
(1011, 'Vivek Sharma'    , '30/06/1974', 'Male'   , 'Udaipur, India'   ,111),
(1012, 'Nisha Singh'     , '28/05/1984', 'Female' , 'Ranchi, India'    ,112),
(1013, 'Prakash Patel'   , '10/03/1976', 'Male'   , 'Lucknow, India'   ,113),
(1014, 'Tanvi Choudhary' , '29/07/1992', 'Female' , 'Surat, India'     ,114),
(1015, 'Arvind Verma'    , '03/10/1981', 'Male'   , 'Nagpur, India'    ,115),
(1016, 'Kavita Sharma'   , '16/01/1973', 'Female' , 'Chennai, India'   ,116),
(1017, 'Priya Patel'     , '18/06/1979', 'Female' , 'Pune, India'      ,117),
(1018, 'Advika Singh'    , '14/04/1976', 'Female' , 'Jaipur, India'    ,118),
(1019, 'Ajay Kapoor'     , '05/04/1987', 'Male'   , 'Gurgaon, India'   ,119),
(1020, 'Sanjana Mehta'   , '17/07/1981', 'Female' , 'Mussoorie, India' ,120),
(1021, 'Rajan Joshi'     , '28/11/1975', 'Male'   , 'Dehradun, India'  ,121);

select * from Student
select * from Faculty
select * from Course

insert into Student
values(328239177,'Karishma Roy','12/12/2001','Female','Kanpur',120,
(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image))

alter table Student add ProfilePhoto varbinary(max)

insert into Student
values(60391986,'Rita Khanna','12/12/2001','Female','Kanpur',120,
(select * from openrowset(BULK 'C:\Users\MRA572\Downloads\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image))

delete from Student where RegdNo=328239177

truncate table Student

insert into Student values
(244019141,'Aradhya Srivastav','17/05/2002','Female','Bhubaneswar',111,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(547789178,'Rahul Das','06/11/2001','Male','Raipur',112,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(741018115,'Mihir Ray','03/07/2001','Male','Patna',120,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(221239147,'Riya Goel','06/12/2001','Female','Ranchi',121,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(502010047,'Charan Singh','29/09/2002','Male','Agra',110,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(541719069,'Aisha Sinha','15/06/2002','Female','Kolkata',113,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(756014130,'Celina Cristopher','14/02/2001','Female','Aizwal',107,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(641015458,'Naina Singhania','16/04/2002','Female','Bangalore',108,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(251019389,'Sumit Arora','03/08/2002','Male','Hyderabad',104,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(581019217,'Ajay Sahu','01/02/2002','Male','Chennai',110,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(862369471,'Ananya Pandey','09/09/2002','Female','Mumbai',114,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(258963256,'Himansh Shukla','10/02/2001','Male','Jaipur',117,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(789522315,'Jethalal Gada','20/12/2002','Male','Ahemdabad',119,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(641019215,'Atamaram Bhide','11/11/2002','Male','Nagpur',108,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(851019217,'Shivangi Mehera','08/04/2002','Female','Jammu',120,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image)),
(541015481,'Vyom Dixit','25/06/2001','Male','Chandigarh',110,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(241019211,'Tanmay Bhatt','19/07/2001','Male','Amritsar',105,(select * from openrowset(BULK 'C:\PhotoFolder\BoyFormalPhoto.jfif', SINGLE_BLOB) as image)),
(541000237,'Mahira Sharma','05/05/2001','Female','Gurgaon',103,(select * from openrowset(BULK 'C:\PhotoFolder\GirlFormalPhoto2.jfif', SINGLE_BLOB) as image))