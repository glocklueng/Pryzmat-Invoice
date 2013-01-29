INSERT INTO [Pryzmat_2013].[dbo].[Clients]
           ([name]
           ,[city]
           ,[address]
           ,[nip]
           ,[zip])
   SELECT name, city, address, nip, zip from [Pryzmat].[dbo].[Clients];
GO


