drop table Employee;
create table Employee
(
 Id int(10) auto_increment primary key
,Agency varchar(3) not null
,SSN varchar(10) not null
,LastName varchar(35) not null
,FirstName varchar(35) not null
,MiddleName varchar(35)
,Suffix varchar(7)
,DateOfBirth date not null
,StreetAddress varchar(75)
,StreetAddress2 varchar(75)
,StreetAddress3 varchar(75)
,City varchar(75)
,State varchar(2)
,ZipCode varchar(5)
,ZipCode2 varchar(4)
,PayPeriodEndDate datetime not null
,CONSTRAINT employee_unique UNIQUE (Agency, SSN)
);

drop table EmployeeEft;
create table EmployeeEft
(
 Id int(10) auto_increment primary key
,EmployeeId int(10) not null
,RecipientName varchar(30)
,RoutingNumber varchar(9)
,AccountNumber varchar(30)
,PayPeriodEndDate datetime not null
);
ALTER TABLE EmployeeEft
ADD CONSTRAINT np_EmployeeEft_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee (Id) ON DELETE CASCADE ON UPDATE CASCADE;

drop table EmployeeEftAddress;
create table EmployeeEftAddress
(
 Id int(10) auto_increment primary key
,EmployeeId int(10) not null
,BankName varchar(35)
,AccountNumber varchar(30)
,BankStreetAddress varchar(75)
,BankStreetAddress2 varchar(75)
,PayPeriodEndDate datetime not null
);
ALTER TABLE EmployeeEftAddress
ADD CONSTRAINT np_EmployeeEftAddress_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee (Id) ON DELETE CASCADE ON UPDATE CASCADE;


drop table EmployeeNonEft;
create table EmployeeNonEft
(
 Id int(10) auto_increment primary key
,EmployeeId int(10) not null
,RecipientName varchar(75)
,StreetAddress varchar(75)
,StreetAddress2 varchar(75)
,City varchar(75)
,State varchar(2)
,ZipCode varchar(5)
,ZipCode2 varchar(4)
,HomePhone varchar(12)
,PayPeriodEndDate datetime not null
,CONSTRAINT employeenoneft_unique UNIQUE (EmployeeId, RecipientName,PayPeriodEndDate)
);
ALTER TABLE EmployeeNonEft
ADD CONSTRAINT np_EmployeeNonEft_Employee FOREIGN KEY (EmployeeId) REFERENCES Employee (Id) ON DELETE CASCADE ON UPDATE CASCADE;
