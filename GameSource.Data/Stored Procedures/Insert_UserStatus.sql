INSERT INTO [UserStatus]
(Name)
VALUES
('Active');

INSERT INTO [UserStatus]
(Name)
VALUES
('Deactivated');

INSERT INTO [UserStatus]
(Name)
VALUES
('Deleted');

INSERT INTO [UserStatus]
(Name)
VALUES
('Banned');

SELECT TOP (1000) [Id]
      ,[Name]
  FROM [GameSource_DB].[dbo].[UserStatus]