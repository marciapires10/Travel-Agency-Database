CREATE PROCEDURE TravelAgency.spFilterPromo
		@option VARCHAR(20)

AS
	BEGIN
			DECLARE @tempTable table(
				ID		INT not null,
				Active	BIT not null,
				Discount	INT not null
			)

			SET NOCOUNT ON;

			BEGIN
				INSERT INTO @tempTable
				SELECT * FROM TravelAgency.Promo
			END

			IF @option = 'DiscountAsc'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE tt.Discount IS NOT NULL
					ORDER BY tt.Discount
			END

			IF @option = 'DiscountDesc'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE tt.Discount IS NOT NULL
					ORDER BY tt.Discount DESC
			END

			IF @option = 'Active'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE Active = 1
			END

			IF @option = 'Not available'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE Active = 0
			END

			IF @option = 'None'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					ORDER BY ID
			END
	END

GO

exec TravelAgency.spFilterPromo DiscountAsc