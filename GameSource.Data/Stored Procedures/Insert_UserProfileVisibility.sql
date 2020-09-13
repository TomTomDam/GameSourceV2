INSERT INTO [UserProfileVisibility]
(Name)
VALUES
('Everybody');

INSERT INTO [UserProfileVisibility]
(Name)
VALUES
('Friends Only');

INSERT INTO [UserProfileVisibility]
(Name)
VALUES
('Private');

SELECT TOP (1000) [ID]
      ,[Name]
  FROM [GameSource_DB].[dbo].[UserProfileVisibility]