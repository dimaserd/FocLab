

DELETE FROM [FocLab_Clt].[dbo].[AspNetUserRoles]
DELETE FROM [FocLab_Clt].[dbo].[AspNetUsers]
DELETE FROM [FocLab_Clt].[dbo].[AspNetRoles]
DELETE FROM [FocLab_Clt].[dbo].Clients

INSERT INTO [FocLab_Clt].[dbo].[AspNetRoles]
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
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetRoles]


INSERT INTO [FocLab_Clt].[dbo].[AspNetUsers]
(
	[Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
)
SELECT 
	[Id]
      ,[UserName]
      ,[NormalizedUserName]
      ,[Email]
      ,[NormalizedEmail]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[ConcurrencyStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEnd]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUsers]


INSERT INTO [FocLab_Clt].[dbo].[AspNetUserRoles]
(
	[UserId]
      ,[RoleId]
)
SELECT 
	[UserId]
      ,[RoleId]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUserRoles]


INSERT INTO [FocLab_Clt].[dbo].[Clients]
(
	[Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Email]
      ,[Name]
      ,[Surname]
      ,[Patronymic]
      ,[BirthDate]
      ,[Sex]
      ,[DeActivated]
      ,[ObjectJson]
      ,[PhoneNumber]
      ,[AvatarFileId]
)
SELECT 
	[Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Email]
      ,[Name]
      ,[Surname]
      ,[Patronymic]
      ,[BirthDate]
      ,[Sex]
      ,[DeActivated]
      ,[ObjectJson]
      ,[PhoneNumber]
      ,[AvatarFileId]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUsers]
