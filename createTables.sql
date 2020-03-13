DROP TABLE NEWPAY.employee;
CREATE TABLE newpay.employee (
  Id int(10) NOT NULL AUTO_INCREMENT,
  Emplid varchar(15) NOT NULL unique,
  Agency varchar(3) NOT NULL,
  LastName varchar(35) NOT NULL,
  FirstName varchar(35) NOT NULL,
  MiddleName varchar(35) DEFAULT NULL,
  Suffix varchar(7) DEFAULT NULL,
  DateOfBirth date NOT NULL,
  StreetAddress varchar(75) DEFAULT NULL,
  StreetAddress2 varchar(75) DEFAULT NULL,
  StreetAddress3 varchar(75) DEFAULT NULL,
  City varchar(75) DEFAULT NULL,
  State varchar(2) DEFAULT NULL,
  ZipCode varchar(5) DEFAULT NULL,
  ZipCode2 varchar(4) DEFAULT NULL,
  PayPeriodEndDate datetime NOT NULL,
  PRIMARY KEY (Id)
) ENCRYPTION='Y';


INSERT INTO newpay.employee
(Emplid,
Agency,
LastName,
FirstName,
MiddleName,
Suffix,
DateOfBirth,
StreetAddress,
StreetAddress2,
StreetAddress3,
City,
State,
ZipCode,
ZipCode2,
PayPeriodEndDate)
VALUES
('03061995',
'FED',
'WALKER',
'JOHNNY',
NULL,
'III',
'1974-01-01',
'123 Main St',
NULL,
NULL,
'BOSTON',
'MA',
'12345',
'6789',
'2019-12-09');

SELECT * FROM newpay.employee;