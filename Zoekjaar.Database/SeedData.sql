/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

:r .\InsertLookupType.sql
:r .\InsertLookup.sql
:r .\InsertUser.sql
:r .\InsertGraduate.sql
:r .\InsertCompany.sql
:r .\InsertJob.sql

exec sp_addrolemember @rolename='db_owner', @membername='NT AUTHORITY\NETWORK SERVICE';