CREATE PROCEDURE TravelAgency.spLoadCustomer
	@size int,
	@noPage int

AS
	BEGIN
		SET NOCOUNT ON

		Select Person.Email, Person.Fname, Person.Lname, Person.phoneNo, CustID, NIF 
		from TravelAgency.Customer Join TravelAgency.Person 
		ON TravelAgency.Customer.Email = TravelAgency.Person.Email
		ORDER BY CustID

		OFFSET @size * (@noPage - 1) ROWS
		FETCH NEXT @size ROWS ONLY OPTION (RECOMPILE)

	END

GO
