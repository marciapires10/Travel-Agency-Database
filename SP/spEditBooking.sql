CREATE PROCEDURE TravelAgency.EditBooking
		@ID INT,
		@Paid BIT,
		@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON

			BEGIN TRY
				UPDATE TravelAgency.Booking
				SET Paid = '1' 
				WHERE ID = @ID

				SET @responseMsg = 'Success'

			END TRY

			BEGIN CATCH
				SET @responseMsg = error_message()
			END CATCH

	END