
DELETE FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryDayTasks]

--Импортирование заданий на день
INSERT INTO [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryDayTasks]
 ([Id]
      ,[CreationDate]
      ,[TaskDate]
      ,[TaskText]
	  ,[TaskTitle]
	  ,[FinishDate]
	  ,[TaskReviewHtml]
	  ,[TaskCommentHtml]
      ,[AdminId]
      ,[AssigneeUserId])
SELECT [Id]
      ,[CreationDate]
      ,[TaskDate]
      ,[TaskText]
	  ,[TaskTitle]
	  ,[FinishDate]
	  ,[TaskReviewHtml]
	  ,[TaskCommentHtml]
      ,[AdminId]
      ,[AssigneeUserId] 
FROM [1gb_custom-chemistry].[dbo].[ChemistryDayTasks]


DELETE FROM [1gb_foclab-on-netcroco].[dbo].[DayTasks]

--Импортирование заданий на день
INSERT INTO [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[DayTasks]
 ([Id]
      ,[CreatedOn]
      ,[TaskDate]
      ,[TaskText]
	  ,[TaskTitle]
	  ,[FinishDate]
	  ,[TaskReview]
	  ,[TaskComment]
      ,[AuthorId]
      ,[AssigneeUserId]
	  ,[EstimationSeconds]
	  ,[CompletionSeconds]
	  ,[Seconds])
SELECT [Id]
      ,[CreationDate]
      ,[TaskDate]
      ,[TaskText]
	  ,[TaskTitle]
	  ,[FinishDate]
	  ,[TaskReviewHtml]
	  ,[TaskCommentHtml]
      ,[AdminId]
      ,[AssigneeUserId]
	  ,0 
	  ,0
	  ,0
FROM [aspnet-FocLab-53bc9b9d-9d6a-45d4-8429-2a2761773502].[dbo].[ChemistryDayTasks]