CREATE PROCEDURE TravelAgency.spDeleteCustomer
	@Email VARCHAR(60),
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON
			BEGIN TRAN
				BEGIN TRY
						
						DECLARE @CustID INT
						SET @CustID = (Select TravelAgency.GetCustID(@Email))

						IF EXISTS(Select Cust_ID from TravelAgency.Booking Where TravelAgency.Booking.Paid = 0)
							BEGIN
									DELETE FROM TravelAgency.Booking WHERE Cust_ID = @CustID
									DELETE FROM TravelAgency.Review WHERE Cust_ID = @CustID
							END
						ELSE
							BEGIN
								UPDATE TravelAgency.Booking
								SET Cust_ID = 0
								WHERE Cust_ID = @CustID

								UPDATE TravelAgency.Review
								SET Cust_ID = 0
								WHERE Cust_ID = @CustID
							END

						DELETE FROM TravelAgency.Customer WHERE Email=@Email
						DELETE FROM TravelAgency.Person WHERE Email=@Email
					


						SET @responseMsg='Success'
				END TRY

				BEGIN CATCH
						SET @responseMsg=error_message()
				END CATCH
			COMMIT TRAN
	END
GO