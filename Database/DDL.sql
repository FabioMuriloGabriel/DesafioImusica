USE DB_Challenge_Imusica

GO
CREATE TABLE Employee  
(  
	Id uniqueidentifier NOT NULL  
	DEFAULT newid(),  
	Name varchar(100) NOT NULL,  
	Email varchar(100) NULL,   
	Genre bit NOT NULL,   
	Birth datetime NULL,  
	Role smallint NOT NULL,  
	PRIMARY KEY(Id)
);  

GO
CREATE TABLE Dependent
(
	Id smallint NOT NULL identity(1,1),
	Name varchar(100) NOT NULL,
	Employee UNIQUEIDENTIFIER NOT NULL,
	PRIMARY KEY(Id)
);

GO
CREATE TABLE Role
(
	Id smallint NOT NULL identity(1,1),
	Name varchar(100) NOT NULL,
	PRIMARY KEY(Id)
);

GO
ALTER TABLE Employee add constraint FKEmployeeRole
foreign key(Role) references Role(Id);

GO 
ALTER TABLE Dependent ADD CONSTRAINT FKDependentEmployee
FOREIGN KEY(Employee) REFERENCES Employee(Id);