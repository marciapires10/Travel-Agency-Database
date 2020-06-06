USE [p1g9]
GO
/****** Object:  StoredProcedure [TravelAgency].[spEditCustomer]    Script Date: 06/06/2020 03:44:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [TravelAgency].spEditFlight
	@ID int,
	@departTime SmallDateTime,
	@arrivalTime SmallDateTime,
	@Airline VARCHAR(max),
	@classType VARCHAR(max),
	@Price smallmoney,
	@departLoc VARCHAR(max),
	@arrivalLoc VARCHAR(max),
	@responseMsg NVARCHAR(250) output

AS

BEGIN	
		SET NOCOUNT ON
		
		BEGIN TRY
				UPDATE TravelAgency.Flight
				SET departTime = @departTime, arrivalTime = @arrivalTime, Airline = @Airline, classType = @classtype, Price = @Price, CC_Depart = @departLoc, CC_Arriv = @arrivalLoc
				WHERE ID = @ID

				SET @responseMsg = 'Success'

		END TRY

		BEGIN CATCH
				SET @responseMsg = error_message()
		END CATCH

END