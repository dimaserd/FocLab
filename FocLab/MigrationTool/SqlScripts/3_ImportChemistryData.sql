--Удаление старых данных в таблицах
DELETE FROM [FocLab_Chemistry].[dbo].[TaskDbFiles]
DELETE FROM [FocLab_Chemistry].[dbo].[TaskReagents]
DELETE FROM [FocLab_Chemistry].[dbo].[TaskExperiments]
DELETE FROM [FocLab_Chemistry].[dbo].[TaskExperimentFiles]

DELETE FROM [FocLab_Chemistry].[dbo].[Tasks]

DELETE FROM [FocLab_Chemistry].[dbo].[MethodFiles]
DELETE FROM [FocLab_Chemistry].[dbo].[Reagents] --7

DELETE FROM [FocLab_Chemistry].[dbo].[Users]
DELETE FROM [FocLab_Chemistry].[dbo].[Files]


--1 Импортирование [Users]
INSERT INTO [FocLab_Chemistry].[dbo].[Users]
 ([Id]
      ,[Name]
      ,[Email])
SELECT [Id]
      ,[Name]
      ,[Email]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[AspNetUsers]

INSERT INTO [FocLab_Chemistry].[dbo].[Files]
 ([Id]
      )
SELECT [Id]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[DbFiles]



--1 Импортирование [ChemistryMethodFiles]
INSERT INTO [FocLab_Chemistry].[dbo].[MethodFiles]
 ([Id]
      ,[Name]
      ,[FileId]
      ,[CreationDate]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedBy]
      ,[LastModifiedOn]
      --,[RowVersion]
	  )
SELECT [Id]
      ,[Name]
      ,[FileId]
      ,[CreationDate]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedBy]
      ,[LastModifiedOn]
      --,[RowVersion] 
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryMethodFiles]

--2 Импортироование [ChemistryTasks]

INSERT INTO [FocLab_Chemistry].[dbo].[Tasks]
 ([Id]
      ,[Title]
      ,[DeadLineDate]
      ,[PerformedDate]
      ,[CreationDate]
      ,[AdminUserId]
      ,[PerformerUserId]
      ,[MethodFileId]
      ,[AdminQuantity]
      ,[AdminQuality]
      ,[PerformerQuantity]
      ,[PerformerQuality]
      ,[PerformerText]
      ,[SubstanceCounterJson]
      ,[Deleted]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedBy]
      ,[LastModifiedOn]
      --,[RowVersion]
	  )
SELECT [Id]
      ,[Title]
      ,[DeadLineDate]
      ,[PerformedDate]
      ,[CreationDate]
      ,[AdminUserId]
      ,[PerformerUserId]
      ,[MethodFileId]
      ,[AdminQuantity]
      ,[AdminQuality]
      ,[PerformerQuantity]
      ,[PerformerQuality]
      ,[PerformerText]
      ,[SubstanceCounterJson]
      ,[Deleted]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedBy]
      ,[LastModifiedOn]
      --,[RowVersion]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryTasks]

--3 Импортирование [ChemistryTaskDbFiles]
INSERT INTO [FocLab_Chemistry].[dbo].[TaskDbFiles]
 ([ChemistryTaskId]
      ,[FileId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Type]
	  )
SELECT [ChemistryTaskId]
      ,[FileId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Type] 
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryTaskDbFiles]

--4 Импортирование [ChemistryReagents]
INSERT INTO [FocLab_Chemistry].[dbo].[Reagents]
 (
 [Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Name]
 )
SELECT [Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Name]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryReagents]

--5 Импортирование [ChemistryTaskReagents]
INSERT INTO [FocLab_Chemistry].[dbo].[TaskReagents]
 (
 [TaskId]
      ,[ReagentId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[TakenQuantity]
      ,[ReturnedQuantity]
 )
SELECT [TaskId]
      ,[ReagentId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[TakenQuantity]
      ,[ReturnedQuantity]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryTaskReagents]

--6 Импортирование [ChemistryTaskReagents]
INSERT INTO [FocLab_Chemistry].[dbo].[TaskExperiments]
 (
 [Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Title]
      ,[ChemistryTaskId]
      ,[PerformerId]
      ,[PerformedDate]
      ,[PerformerText]
      ,[CreationDate]
      ,[Deleted]
      ,[SubstanceCounterJson]
 )
SELECT [Id]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Title]
      ,[ChemistryTaskId]
      ,[PerformerId]
      ,[PerformedDate]
      ,[PerformerText]
      ,[CreationDate]
      ,[Deleted]
      ,[SubstanceCounterJson]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryTaskExperiments]

--7 Импортирование [ChemistryTaskExperimentFiles]
INSERT INTO [FocLab_Chemistry].[dbo].[TaskExperimentFiles]
 (
 [ChemistryTaskExperimentId]
      ,[FileId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Type]
 )
SELECT [ChemistryTaskExperimentId]
      ,[FileId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      --,[RowVersion]
      ,[Type]
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryTaskExperimentFiles]
