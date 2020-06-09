CREATE FUNCTION TravelAgency.GetAccID (@Name VARCHAR(max))
returns INT

AS
	BEGIN
			DECLARE @ID INT
			SET @ID = (Select ID 
			from TravelAgency.Accommodation 
			where Name = @Name)
			RETURN @ID;

	END
go

Select TravelAgency.GetAccID ('Bode LLC')