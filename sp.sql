
--Users--
--1--
create Procedure S_IsRegisteredUser_P
@MobileNumber varchar(10)
as
select * from users where MobileNumber=@MobileNumber
go

create procedure S_InsertUser_P 
@MobileNumber varchar(10),
@UserName varchar(25),
@UserRole varchar(10),
@UserStatus bit,
@Password varchar(25) 
as
begin
Insert into users(MobileNumber,UserName,UserRole,UserStatus,Password) values (@MobileNumber,@UserName,@UserRole,@UserStatus,@Password)
end
go

alter procedure S_UpdateUser_P
@id int,
@MobileNumber varchar(10),
@UserName varchar(25),
@UserRole varchar(10),
@UserStatus bit,
@Password varchar(25)
as 
begin
update users 
set 
MobileNumber=@MobileNumber,
UserRole=@UserRole,
UserStatus=@UserStatus,
UserName=@UserName,
Password=@Password
where UserId=@id
end
go

create procedure S_DeleteUser_P
@id int
as
Delete from users where users.UserId=@id

exec S_DeleteUser_P 8


create function SplitString(@input varchar(max),@delimitter varchar(1))
returns @rtnValue Table(SplitedValues varchar(100))
as
Begin
while(len(@input)!=0)
Begin	
if(CHARINDEX(@delimitter,@input)!=0)
begin
		Insert into @rtnValue
		Select Left(@input,CHARINDEX(@delimitter,@input)) as SplitedValues
		set @input = Right(@input,CHARINDEX(@delimitter,@input)-1)
		
End	
set @input = LEFT(@input,LEN(@input)-1)
end
Return
End
 go;




 Alter procedure S_SearchTruck_P
 @PickCity varchar(100),
 @DropCity varchar(100),
 @TruckType varchar(100),
 @ServiceDate date,
 @Aproxdistance int
 as
 select *,@Aproxdistance*30 as cost
 from Truck t  where t.PickCity=@PickCity and t.DropCity=@DropCity and t.TruckType=@TruckType and not exists(select * from Service s where s.TruckNumber=t.TruckNumber and s.BookingDate=@ServiceDate)
 go

 exec S_SearchTruck_P 'Chennai','Hyderabad','TATA ACE(PICKUP) (1.5 TON)','2021-12-08',15
 --Truck--
 --1--
 create Procedure S_GetTruckByTruckNumber_P
@TruckNumber varchar(10)
as
select * from Truck where TruckNumber=@TruckNumber
go


--2--
create Procedure S_GetAllTrucks_P
as
select * from Truck 
go


Create Procedure S_InsertTruck_P
@TruckNumber varchar(10) ,
@TruckType varchar(20) ,
@ManagerId int ,
@DriverName varchar(25),
@DriverLicenceNumber varchar(15) ,
@PickCity varchar(300) ,
@DropCity varchar(300) 
as
begin
DECLARE @TrkStatus bit;
set @TrkStatus=0;

insert into Truck(TruckNumber,TruckType,ManagerId,DriverName,DriverLicenceNumber,PickCity,DropCity,TruckStatus) 
values (@TruckNumber,@TruckType,@ManagerId,@DriverName,@DriverLicenceNumber,@PickCity,@DropCity,@TrkStatus)
end


Create procedure S_UpdateTruck_P
@TruckNumber varchar(10),
@TruckType varchar(max),
@DriverName varchar(25),
@DriverLicenceNumber varchar(20),
@PickCity varchar(max),
@DropCity varchar(max),
@TruckStatus bit
as 
begin
update Truck 
set 
TruckType=@TruckType,
DriverName=@DriverName,
DriverLicenceNumber=@DriverLicenceNumber,
PickCity=@PickCity,
DropCity=@DropCity,
TruckStatus=@TruckStatus
where TruckNumber=@TruckNumber
end
go



create procedure S_DeleteTruck_P
@id varchar(10)
as
Delete from Truck where Truck.TruckNumber=@id

exec S_DeleteTruck_P 'TN30AY4512'






exec S_InsertTruck_P 'TN29AY2709','DemoTruckType',1,'Pro','157865987845653','Erode','Salem',0
exec S_UpdateUser_P '8','9876543210','xyz','user',0,'MTIzNDU2'


Create procedure S_AllUsers_P
as  
select * from users
go


--Service--
create Procedure S_GetAllService_P
as
select * from Service 
go


Create Procedure S_InsertService_P

@CustomerId int ,
@ManagerId int ,
@TruckNumber varchar(10),
@BookingDate Date,
@ServiceStatus bit,
@BookingStatus bit,
@PickCity varchar(300),
@DropCity varchar(300),
@ServiceCost int
as
begin

insert into Service(CustomerId,ManagerId,TruckNumber,BookingDate,ServiceStatus,BookingStatus,PickupCity,DropCity,ServiceCost) 
values (@CustomerId,@ManagerId,@TruckNumber,@BookingDate,@ServiceStatus,@BookingStatus,@PickCity,@DropCity,@ServiceCost)
end


Create Procedure S_UpdateService_P

@CustomerId int ,
@ManagerId int ,
@TruckNumber varchar(10),
@BookingDate Date,
@ServiceStatus bit,
@BookingStatus bit,
@PickCity varchar(300),
@DropCity varchar(300),
@ServiceCost int
as
begin
update Service
set 
ManagerId=@ManagerId,
TruckNumber=@TruckNumber,
BookingDate=@BookingDate,
ServiceStatus=@ServiceStatus,
BookingStatus=@BookingStatus,
PickupCity=@PickCity,
DropCity=@DropCity,
ServiceCost=@ServiceCost
where
CustomerId=@CustomerId
end

alter procedure S_bookingRequest_P
@Id int
as
begin
select s.TruckNumber,u.UserName,u.MobileNumber,s.BookingDate,s.PickupCity,s.DropCity,s.ServiceCost,s.ServiceId
from Service s 
inner join users u 
on s.ManagerId=@Id and s.CustomerId=u.UserId and s.BookingStatus=1 and s.ServiceStatus=0
end

exec S_bookingRequest_P 2
