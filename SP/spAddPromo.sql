CREATE PROCEDURE TravelAgency.spAddPromo
	@Active BIT,
	@Discount INT,
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON;

			BEGIN TRY
						INSERT INTO TravelAgency.Promo(Active, Discount)
						VALUES (@Active, @Discount)

						SET @responseMsg = 'Success'
			END TRY

			BEGIN CATCH
						SET @responseMsg = error_message()
			END CATCH
	END