CREATE PROCEDURE TravelAgency.spAddBook
	@Paid BIT,
	@bookDate Date,
	@Details VARCHAR(max),
	@Pack_ID INT,
	@Ag_ID INT,
	@Cust_ID INT,
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON;

			BEGIN TRY
						INSERT INTO TravelAgency.Booking(Paid, bookDate, Details, Pack_ID, Ag_ID, Cust_ID)
						VALUES (@Paid, @bookDate, @Details, @Pack_ID, @Ag_ID, @Cust_ID)

						SET @responseMsg = 'Success'
			END TRY

			BEGIN CATCH
						SET @responseMsg = error_message()
			END CATCH
	END
			