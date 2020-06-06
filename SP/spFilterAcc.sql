CREATE PROCEDURE TravelAgency.spFilterAcc
		@option VARCHAR(20),
		@dest	VARCHAR(20)

AS
	BEGIN
			DECLARE @tempTable table(
				ID		INT not null,
				Name	VARCHAR(40) not null,
				Image	VARBINARY(max),
				Description VARCHAR(50),
				Price SMALLMONEY not null,
				CC_Location VARCHAR(max) not null
			)

			SET NOCOUNT ON;

			IF @dest <> 'None'
			BEGIN
					INSERT INTO @tempTable (ID, Name, Image, Description, Price, CC_Location)
					SELECT * FROM TravelAgency.Accommodation where CC_Location like '%'+@dest+'%'
			END

			ELSE
			BEGIN
					INSERT INTO @tempTable
					SELECT * FROM TravelAgency.Accommodation 
			END

			IF @option = 'PriceAsc'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE tt.Price IS NOT NULL
					ORDER BY tt.Price ASC
			END

			IF @option = 'PriceDesc'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE tt.Price IS NOT NULL
					ORDER BY tt.Price DESC
			END

			IF @option = 'None'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					ORDER BY ID
			END
	END

GO

--exec TravelAgency.spFilterAcc PriceAsc, Madrid