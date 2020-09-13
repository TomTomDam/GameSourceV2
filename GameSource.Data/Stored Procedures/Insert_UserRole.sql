INSERT INTO [UserRole]
([Name], NormalizedName, [Description])
VALUES
('Member', 'MEMBER', 'Default role for a user');

INSERT INTO [UserRole]
([Name], NormalizedName, [Description])
VALUES
('Moderator', 'MODERATOR', 'Able to ban members');

INSERT INTO [UserRole]
([Name], NormalizedName, [Description])
VALUES
('Admin', 'ADMIN', 'Has full privileges');

SELECT TOP (1000) [Id]
      ,[Name]
      ,[NormalizedName]
      ,[ConcurrencyStamp]
      ,[Description]
  FROM [GameSource_DB].[dbo].[UserRole]