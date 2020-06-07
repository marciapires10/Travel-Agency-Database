CREATE PROCEDURE TravelAgency.spDisablePromo
	@ID INT,
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON

			BEGIN TRY
				UPDATE TravelAgency.Promo
				SET Active = '0'
				WHERE ID = @ID

				SET @responseMsg = 'Success'

			END TRY

			BEGIN CATCH
				SET @responseMsg = error_message()
			END CATCH

	END

