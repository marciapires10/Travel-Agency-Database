USE [p1g9]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [TravelAgency].spAddNewCC
	@City VARCHAR(max),
	@Country VARCHAR(max),
	@responseMsg NVARCHAR(250) output

AS

BEGIN
		SET NOCOUNT ON

		BEGIN TRY
					INSERT INTO TravelAgency.CC (City, Country) 
					VALUES (@City, @Country)

					SET @responseMsg = 'Success'
		END TRY

		BEGIN CATCH
					SET @responseMsg = error_message()
		END CATCH
END
GO


