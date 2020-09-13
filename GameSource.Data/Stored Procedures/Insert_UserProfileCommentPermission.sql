INSERT INTO [UserProfileCommentPermission]
(Name)
VALUES
('Everybody');

INSERT INTO [UserProfileCommentPermission]
(Name)
VALUES
('Friends Only');

INSERT INTO [UserProfileCommentPermission]
(Name)
VALUES
('Nobody');

SELECT TOP (1000) [ID]
      ,[Name]
  FROM [GameSource_DB].[dbo].[UserProfileCommentPermission]