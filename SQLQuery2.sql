create database MyDataBase11
go

create table MyDataBase11.dbo.Groups(
	gro_id int not null IDENTITY(1,1),
	gro_name varchar(150),
	constraint Pk_groups primary key (gro_id)
);
create table MyDataBase11.dbo.Students(
	id int not null identity(1,1),
	stu_id int null,
	f varchar(50),
	gro_id int null,
	constraint Pk_student primary key(id),
	constraint Fk_student foreign key(gro_id) references MyDataBase11.dbo.Groups(gro_id)
);