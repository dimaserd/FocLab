DELETE FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[DbFiles]

GO 
SET IDENTITY_INSERT [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[DbFiles] ON; 
GO

INSERT INTO [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[DbFiles]
 ([Id]
      ,[CurrentSnapshotId]
      ,[CreatedBy]
      ,[CreatedOn]
      ,[LastModifiedOn]
      ,[LastModifiedBy]
      ,[FilePath]
      ,[FileName]
      ,[FileData])
SELECT Id, NEWID(), NEWID(), UploadDate, NULL, NULL, NULL, FileName, FileData 
FROM [1gb_new-foclab-db].[dbo].[DbFiles]

GO 
SET IDENTITY_INSERT [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[DbFiles] OFF; 
GO