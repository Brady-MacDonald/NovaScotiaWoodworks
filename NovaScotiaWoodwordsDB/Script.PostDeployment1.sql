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

IF NOT EXISTS (SELECT * FROM dbo.Users)
BEGIN
    INSERT INTO dbo.Users (FirstName, LastName, EmailAddress, UserName, [Password], Salt)
    VALUES 
    ('Brady', 'MacDonald', 'brady.macdonald@dal.ca', 'brady-mac', 'Test1234', '1'),
    ('Ivanka', 'Bashkir', 'ivanna.bashkir@gmail.com', 'ivan', 'Ivanka1234', '2')
END