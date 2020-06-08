USE [p1g9]
GO

/****** Object:  StoredProcedure [TravelAgency].[spAddFlight]    Script Date: 08/06/2020 01:24:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [TravelAgency].[spAddTransfer]
	@Company VARCHAR(max),
	@Price smallmoney,
	@Included bit,
	@departLoc VARCHAR(max),
	@arrivalLoc VARCHAR(max),
	@responseMsg NVARCHAR(250) output

AS

BEGIN
		SET NOCOUNT ON

		BEGIN TRY
					INSERT INTO TravelAgency.Transfer(Company, Included, Price, CC_Depart, CC_Arriv) 
					VALUES (@Company, @Included, @Price, @departLoc, @arrivalLoc)

					SET @responseMsg = 'Success'
		END TRY

		BEGIN CATCH
					SET @responseMsg = error_message()
		END CATCH
END
GO


