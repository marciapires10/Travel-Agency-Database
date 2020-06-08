USE [p1g9]
GO

/****** Object:  StoredProcedure [TravelAgency].[spEditFlight]    Script Date: 08/06/2020 02:27:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [TravelAgency].[spEditTransfer]
	@ID int,
	@Company VARCHAR(max),
	@Included VARCHAR(max),
	@Price smallmoney,
	@departLoc VARCHAR(max),
	@arrivalLoc VARCHAR(max),
	@responseMsg NVARCHAR(250) output

AS

BEGIN	
		SET NOCOUNT ON
		
		BEGIN TRY
				UPDATE TravelAgency.Transfer
				SET Company = @Company, Included = @Included, Price = @Price, CC_Depart = @departLoc, CC_Arriv = @arrivalLoc
				WHERE ID = @ID

				SET @responseMsg = 'Success'

		END TRY

		BEGIN CATCH
				SET @responseMsg = error_message()
		END CATCH

END
GO


