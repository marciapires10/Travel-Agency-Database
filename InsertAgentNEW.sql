USE [p1g9]
GO

DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@NewAgID = 1,
		@NewAccountPwd = N'CLJ76KJO1JB',
		@Email = N'gravida.Aliquam@quis.org'

SELECT	'Return Value' = @return_value

GO


DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@NewAgID = 2,
		@NewAccountPwd = N'WNV63NAB5IX',
		@Email = N'in@tellusNunclectus.edu'

SELECT	'Return Value' = @return_value

GO


DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@NewAgID = 3,
		@NewAccountPwd = N'XFE33OHT6ZM',
		@Email = N'Sed.dictum.Proin@seddolor.net'

SELECT	'Return Value' = @return_value

GO


DECLARE	@return_value int

EXEC	@return_value = [TravelAgency].[CreateAgent]
		@NewAgID = 4,
		@NewAccountPwd = N'UGO58OPU4CF',
		@Email = N'faucibus.ut@massa.net'

SELECT	'Return Value' = @return_value

GO


