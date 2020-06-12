CREATE PROCEDURE TravelAgency.spFilterBookings
		@option VARCHAR(20)

AS
	BEGIN
			DECLARE @tempTable table(
				ID		INT not null,
				Paid	BIT not null,
				bookDate DATE not null
			)

			SET NOCOUNT ON;

			BEGIN
				INSERT INTO @tempTable
				SELECT ID, Paid, bookDate FROM TravelAgency.Booking
			END

			IF @option = 'Yes'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE Paid = 1
			END

			IF @option = 'No'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE Paid = 0
			END

			IF @option = 'None'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					ORDER BY ID
			END
	END

GO