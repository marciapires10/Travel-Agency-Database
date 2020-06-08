USE [p1g9]
GO

DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@Fname = N'Evan',
		@Lname = N'Mcclain',
		@PhoneNo = N'211897722',
		@NewAccountPwd = N'CLJ76KJO1JB',
		@Email = N'gravida.Aliquam@quis.org',
		@responseMsg = N'Success'

SELECT	'Return Value' = @return_value

GO


DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@Fname = N'Damon',
		@Lname = N'Fuentes',
		@PhoneNo = N'680229936',
		@NewAccountPwd = N'WNV63NAB5IX',
		@Email = N'in@tellusNunclectus.edu',
		@responseMsg = N'Success'

SELECT	'Return Value' = @return_value

GO


DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@Fname = N'Salvador',
		@Lname = N'Noble',
		@PhoneNo = N'679951701',
		@NewAccountPwd = N'XFE33OHT6ZM',
		@Email = N'Sed.dictum.Proin@seddolor.net',
		@responseMsg = N'Success'

SELECT	'Return Value' = @return_value

GO


DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@Fname = N'Phyllis',
		@Lname = N'Bridges',
		@PhoneNo = N'139727682',
		@NewAccountPwd = N'UGO58OPU4CF',
		@Email = N'faucibus.ut@massa.net',
		@responseMsg = N'Success'

SELECT	'Return Value' = @return_value

GO




