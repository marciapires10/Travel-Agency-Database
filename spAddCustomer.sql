CREATE PROCEDURE TravelAgency.spAddCostumer
	@Email VARCHAR(max),
	@Fname VARCHAR(max),
	@Lname VARCHAR(max),
	@PhoneNo INT,
	@CustId INT,
	@NIF INT,
	@responseMsg NVARCHAR(250) output

AS

BEGIN
		SET NOCOUNT ON

		BEGIN TRY
					INSERT INTO TravelAgency.Person (Email, Fname, Lname, PhoneNo) 
					VALUES (@Email, @Fname, @Lname, @PhoneNo)

					INSERT INTO TravelAgency.Customer(CustID, NIF, Email)
					VALUES (@CustId, @NIF, @Email)

					SET @responseMsg = 'Success'
		END TRY

		BEGIN CATCH
					SET @responseMsg = error_message()
		END CATCH
END