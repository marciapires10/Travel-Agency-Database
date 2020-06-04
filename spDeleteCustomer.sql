CREATE PROCEDURE TravelAgency.spDeleteCustomer
	@Email VARCHAR(60),
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON
			BEGIN TRY
					DELETE FROM TravelAgency.Customer WHERE Email=@Email
					DELETE FROM TravelAgency.Person WHERE Email=@Email
					SET @responseMsg='Success'
			END TRY

			BEGIN CATCH
					SET @responseMsg=error_message()
			END CATCH
	END
GO