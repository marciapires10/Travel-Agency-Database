USE [p1g9]
GO
/****** Object:  StoredProcedure [TravelAgency].[spAddCostumer]    Script Date: 06/06/2020 00:01:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [TravelAgency].spAddFlight
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
					INSERT INTO TravelAgency.Flight(departTime, arrivalTime, Airline, classType, Price, CC_Depart, CC_Arriv) 
					VALUES (@departTime, @arrivalTime, @Airline, @classType, @Price, @departLoc, @arrivalLoc)

					SET @responseMsg = 'Success'
		END TRY

		BEGIN CATCH
					SET @responseMsg = error_message()
		END CATCH
END