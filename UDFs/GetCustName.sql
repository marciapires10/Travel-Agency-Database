CREATE FUNCTION TravelAgency.GetCustName (@ID INT)
returns INT

AS
	BEGIN
			DECLARE @Name INT
			DECLARE @Email VARCHAR(60)

			SET @Email = (Select Email
			from TravelAgency.Customer 
			where CustID = @ID)

			SET @Name = (Select Fname, Lname
			from TravelAgency.Person
			where Email = @Email)
			RETURN @Name;

	END