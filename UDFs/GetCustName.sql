CREATE FUNCTION TravelAgency.GetCustName (@ID INT)
returns VARCHAR(MAX)

AS
	BEGIN
			DECLARE @Name VARCHAR(max)
			DECLARE @Email VARCHAR(60)

			SET @Email = (Select Email
			from TravelAgency.Customer 
			where CustID = @ID)

			SET @Name = (Select Fname + ' ' + Lname
			from TravelAgency.Person
			where Email = @Email)
			RETURN @Name;

	END

--Select TravelAgency.GetCustName(94)