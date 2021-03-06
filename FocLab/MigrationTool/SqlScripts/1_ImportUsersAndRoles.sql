--Импортирование ролей

DELETE FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetRoles]

INSERT INTO [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetRoles]
(
	[Id]
    ,[Name]
    ,[NormalizedName]
    ,[ConcurrencyStamp]
)
SELECT 
	[Id]
	,[Name]
	,UPPER([Name])
	,NEWID()
FROM [1gb_new-foclab-db].[dbo].[AspNetRoles]

--Импортивание пользователей

DELETE FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUsers]

INSERT INTO [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUsers]
 ([Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email] --4
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash] 
      ,[SecurityStamp] --8
      ,[ConcurrencyStamp] 
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled] --12
      ,[LockoutEnd]
      ,[LockoutEnabled] 
      ,[AccessFailedCount]
      ,[Name] --16
      ,[Surname]
      ,[Patronymic]
      ,[CurrentSnapshotId] 
      ,[CreatedBy] --20
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy] 
      ,[AvatarFileId] --24
      ,[UnConfirmedEmail]
      ,[BirthDate]
      ,[Sex]
      ,[Balance] --28
      ,[DeActivated]
      ,[ObjectJson]) --30
SELECT 
	   [Id]
	  ,[UserName]
	  ,UPPER([UserName])
	  ,[Email] --4
	  ,UPPER([Email])
	  ,[EmailConfirmed] --+
	  ,[PasswordHash] 
	  ,[SecurityStamp] --8
	  ,NEWID()  --ConcurrencyStamp
	  ,[PhoneNumber]
	  ,[PhoneNumberConfirmed]
	  ,[TwoFactorEnabled] --12
	  ,[LockoutEndDateUtc]
	  ,[LockoutEnabled] 
	  ,[AccessFailedCount]
      ,[Name]
      ,[Surname] --16
      ,[Patronymic]
	  ,[CurrentSnapshotId]
	  ,[CreatedBy]
      ,[CreatedOn] --20
      ,[LastModifiedOn]
      ,[LastModifiedBy]
	  , NULL --AvatarFileId
      ,[UnConfirmedEmail] --24
      ,[BirthDate]
      ,[Sex]
      ,[Balance]
      ,[DeActivated] --28
      ,[ObjectJSON]
      
FROM [1gb_new-foclab-db].[dbo].[AspNetUsers]

--Импортирование ролей для пользователя

DELETE FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUserRoles]

INSERT INTO [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUserRoles]
(
	[UserId]
    ,[RoleId]
)
SELECT 
	[UserId]
   ,[RoleId]
FROM [1gb_new-foclab-db].[dbo].[AspNetUserRoles]