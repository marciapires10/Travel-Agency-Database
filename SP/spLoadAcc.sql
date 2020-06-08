CREATE PROCEDURE TravelAgency.spLoadAcc
	@size int,
	@noPage int
AS
	BEGIN
			SET NOCOUNT ON

			SELECT * FROM TravelAgency.Accommodation
			ORDER BY ID

			OFFSET @size * (@noPage - 1) ROWS
			FETCH NEXT @size ROWS ONLY OPTION (RECOMPILE)
	END

GO