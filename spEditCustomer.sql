CREATE PROCEDURE TravelAgency.spEditCustomer
	@Email VARCHAR(max),
	@Fname VARCHAR(max),
	@Lname VARCHAR(max),
	@PhoneNo INT,
	@NIF INT,
	@responseMsg NVARCHAR(250) output

AS

BEGIN	
		SET NOCOUNT ON
		
		BEGIN TRY
				UPDATE TravelAgency.Person 
				SET Fname = @Fname, Lname = @Lname, PhoneNo = @PhoneNo
				WHERE Email = @Email

				UPDATE TravelAgency.Customer
				SET NIF = @NIF
				WHERE Email = @Email
				
				SET @responseMsg = 'Success'

		END TRY

		BEGIN CATCH
				SET @responseMsg = error_message()
		END CATCH

END