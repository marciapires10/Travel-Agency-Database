CREATE PROCEDURE TravelAgency.spAddReview
	@Description VARCHAR(max),
	@Score INT,
	@PackID INT,
	@CustID INT,
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON;

			BEGIN TRY
						INSERT INTO TravelAgency.Review(Description, Score, Pack_ID, Cust_ID)
						VALUES(@Description, @Score, @PackID, @CustID)

						SET @responseMsg = 'Success'
			END TRY

			BEGIN CATCH
						SET @responseMsg = error_message()
			END CATCH

	END
