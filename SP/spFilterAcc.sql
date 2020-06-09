CREATE PROCEDURE TravelAgency.spFilterAcc
		@size int,
		@noPage int,
		@option VARCHAR(20),
		@dest	VARCHAR(20)

AS
	BEGIN
			DECLARE @tempTable table(
				ID		INT not null,
				Name	VARCHAR(max) not null,
				Image	VARCHAR(max),
				Description VARCHAR(max),
				Price VARCHAR(max) not null,
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

					OFFSET @size * (@noPage - 1) ROWS
					FETCH NEXT @size ROWS ONLY OPTION (RECOMPILE)
			END

			IF @option = 'PriceDesc'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					WHERE tt.Price IS NOT NULL
					ORDER BY tt.Price DESC

					OFFSET @size * (@noPage - 1) ROWS
					FETCH NEXT @size ROWS ONLY OPTION (RECOMPILE)
			END

			IF @option = 'None'
			BEGIN
					SELECT tt.* FROM @tempTable tt
					ORDER BY ID

					OFFSET @size * (@noPage - 1) ROWS
					FETCH NEXT @size ROWS ONLY OPTION (RECOMPILE)
			END
	END

GO

exec TravelAgency.spFilterAcc 12, 1, 'None', 'None'