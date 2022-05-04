/*
Created		14/06/2021
Modified		21/04/2022
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/
USE [master]
GO


IF(EXISTS(SELECT * FROM SYSDATABASES WHERE NAME='MobilePhone'))
	DROP DATABASE MobilePhone
GO

CREATE DATABASE MobilePhone
GO

USE MobilePhone
GO

Create table [Category]
(
	[ID_Category] Integer Identity NOT NULL,
	[Name] Nvarchar(255) NOT NULL,
	[Status] Bit NOT NULL,
Primary Key ([ID_Category])
) 
go

Create table [Color]
(
	[ID_Color] Integer Identity NOT NULL,
	[Name] Nvarchar(20) NOT NULL,
Primary Key ([ID_Color])
)
go

Create table [Product]
(
	[ID_Product] Integer Identity NOT NULL,
	[ID_Category] Integer NOT NULL,
	[ID_Color] Integer NOT NULL,
	[Name] Nvarchar(255) NOT NULL,
	[Screen] Nvarchar(50)  NULL,
	[SystemOperation] Nvarchar(100)  NULL,
	[RearCamera] Nvarchar(50)  NULL,
	[FrontCamera] Nvarchar(50)  NULL,
	[Ram] Nvarchar(50)  NULL,
	[Rom] Nvarchar(50)  NULL,
	[Sim] Nvarchar(50)  NULL,
	[Battery] Nvarchar(50)  NULL,
	[Price] Money NOT NULL,
	[Image] Nvarchar(250) NOT NULL,
	[ShortDescription] Nvarchar(255)  NULL,
	[Detail] Ntext  NULL,
	[Amount] Integer  NULL,
	[Origin] Nvarchar(255)  NULL,
	[CreatedDate] [date] NULL,
	[Status] [bit] NOT NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [nvarchar](100) NULL,
Primary Key ([ID_Product])
) 
go

Create table [Account]
(
	[ID_Account] Integer Identity NOT NULL,
	[ID_Role] Integer NOT NULL,
	[UserName] Varchar(255) NOT NULL, UNIQUE ([UserName]),
	[Password] Varchar(255) NOT NULL,
	[FullName] Nvarchar(255) NOT NULL,
	[Phone] Char(20) NULL,
	[Address] Nvarchar(255) NULL,
	[Email] Varchar(50) NULL,
	[Status] Bit NOT NULL,
	[Avatar] Nvarchar(255) NULL,
	[IsManager] [bit] NULL,
	[ResetPasswordCode] [varchar](100) NULL,
	[ActivationCode] [varchar](100) NULL,
Primary Key ([ID_Account])
) 
go

Create table [Role]
(
	[ID_Role] Integer Identity NOT NULL,
	[Name] Nvarchar(255) NOT NULL,
	[Code] Varchar(50) NOT NULL,
Primary Key ([ID_Role])
) 
go

Create table [Cart]
(
	[ID_Product] Integer NOT NULL,
	[ID_Account] Integer NOT NULL,
	[Amount] Integer NOT NULL,
Primary Key ([ID_Product],[ID_Account])
) 
go

Create table [Bill]
(
	[ID_Bill] Integer Identity NOT NULL,
	[ID_Account] Integer NOT NULL,
	[ReceiverName] Nvarchar(50) NOT NULL,
	[ReceiverAddress] Nvarchar(255) NOT NULL,
	[ReceiverEmail] Varchar(255) NOT NULL,
	[ReceiverPhone] Char(20) NOT NULL,
	[Note] Ntext NULL,
	[PayType] Nvarchar(255) NOT NULL,
	[Status] Bit NOT NULL,
	[CreatedDate] [date] NULL,
	[ModifiedDate] [date] NULL,
Primary Key ([ID_Bill])
) 
go

Create table [BillDetail]
(
	[ID_Product] Integer NOT NULL,
	[ID_Bill] Integer NOT NULL,
	[Amount] Integer NOT NULL,
	[CurrentlyPrice] Money NOT NULL,
Primary Key ([ID_Product],[ID_Bill])
) 
go

Create table [Permission]
(
	[ID_Permission] Integer Identity NOT NULL,
	[Name] Nvarchar(255) NOT NULL,
	[Code] Nvarchar(100) NOT NULL,
Primary Key ([ID_Permission])
) 
go

Create table [PermissionDetail]
(
	[ID_Permission] Integer NOT NULL,
	[ID_Product] Integer NOT NULL,
	[Code] Varchar(100) NULL,
	[CanView] Bit NULL,
	[CanCreate] Bit NULL,
	[CanEdit] Bit NULL,
	[CanDelete] Bit NULL,
Primary Key ([ID_Permission],[ID_Product])
) 
go


Alter table [Product] add  foreign key([ID_Category]) references [Category] ([ID_Category])  on update no action on delete no action 
go
Alter table [Product] add  foreign key(ID_Color) references [Color] ([ID_Color])  on update no action on delete no action 
go
Alter table [Cart] add  foreign key([ID_Product]) references [Product] ([ID_Product])  on update no action on delete no action 
go
Alter table [BillDetail] add  foreign key([ID_Product]) references [Product] ([ID_Product])  on update no action on delete no action 
go
Alter table [PermissionDetail] add  foreign key([ID_Product]) references [Product] ([ID_Product])  on update no action on delete no action 
go
Alter table [Cart] add  foreign key([ID_Account]) references [Account] ([ID_Account])  on update no action on delete no action 
go
Alter table [Bill] add  foreign key([ID_Account]) references [Account] ([ID_Account])  on update no action on delete no action 
go
Alter table [Account] add  foreign key([ID_Role]) references [Role] ([ID_Role])  on update no action on delete no action 
go
Alter table [BillDetail] add  foreign key([ID_Bill]) references [Bill] ([ID_Bill])  on update no action on delete no action 
go
Alter table [PermissionDetail] add  foreign key([ID_Permission]) references [Permission] ([ID_Permission])  on update no action on delete no action 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


insert into Category([Name],[Status]) values
('Iphone',1),
('Samsung',1),
('Oppo',1),
('Huawei',1),
('Sony',1)

insert into Color([Name]) values
(N'Đen'),
(N'Trắng'),
(N'Xanh Pacific'),
(N'Đỏ'),
(N'Bạc')

insert into Product(ID_Category, [ID_Color],[Name], Price, [Image], Amount, [Status]) values
(1,1,N'Iphone 13',4500000,N'iphone-13-pro-max-1-2.jpg',2,1),
(2,2,N'Samsung A50',12000000,N'samsung-galaxy-a33-5g-xanh-1.jpg',2,1),
(3,3,N'Oppo A83',3400000,N'oppo-reno6-z-5g-bac-1-org.jpg',2,1),
(4,4,N'Huawei 10',12345679,N'xiaomi-redmi-note-11-1-4.jpg',2,1)

INSERT [dbo].[Permission] ( [Name], [Code]) VALUES ( N'Quản lý danh mục', N'CATEGORIES')
INSERT [dbo].[Permission] ( [Name], [Code]) VALUES ( N'Quản lý sản phẩm', N'PRODUCTS')
INSERT [dbo].[Permission] ( [Name], [Code]) VALUES ( N'Quản lý đơn hàng', N'ORDERS')


INSERT [dbo].[Role] ([Name], [Code]) VALUES ( N'Khach hang', N'CLIENT')
INSERT [dbo].[Role] ([Name], [Code]) VALUES ( N'Nguoi quan tri', N'ADMIN')

insert into Account(ID_Role, UserName, Password, Status, FullName, Address, Email, Phone) values
(2, N'Admin',N'Admin', 1, N'Vũ Duy Khánh',N'Vĩnh Phúc',N'khanh123@gmail.com','098238383')

select * from Category
select * from Product
select * from Permission
select * from Role

select * from Account
select * from Color

