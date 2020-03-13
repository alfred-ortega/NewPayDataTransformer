CREATE TABLE newpaydb.employee (
  Id int(10) NOT NULL AUTO_INCREMENT PRIMARY KEY,
  Emplid varchar(15) NOT NULL unique,
  Agency varchar(3) NOT NULL,
  LastName varchar(35) NOT NULL,
  FirstName varchar(35) NOT NULL,
  MiddleName varchar(35) DEFAULT NULL,
  DateOfBirth date NOT NULL,
  StreetAddress varchar(75) DEFAULT NULL,
  StreetAddress2 varchar(75) DEFAULT NULL,
  City varchar(75) DEFAULT NULL,
  State varchar(2) DEFAULT NULL,
  ZipCode varchar(10) DEFAULT NULL,
  SSN varchar(10)
)-- ENCRYPTION='Y'
;


INSERT INTO newpaydb.employee
(Emplid,
Agency,
LastName,
FirstName,
MiddleName,
DateOfBirth,
StreetAddress,
StreetAddress2,
City,
State,
ZipCode,
ssn)
VALUES
('03061995',
'FED',
'WALKER',
'JOHNNY',
NULL,
'1974-01-01',
'123 Main St',
NULL,
'BOSTON',
'MA',
'12345-6789',
'333114444');

select * from newpaydb.employee;