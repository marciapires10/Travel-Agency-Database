CREATE PROCEDURE TravelAgency.spFilterCustomer
		@Fname VARCHAR(20), 
		@Lname VARCHAR(20)

AS
	BEGIN
			DECLARE @tempTable table (
				Email VARCHAR(max) not null,
				Fname VARCHAR(max) not null,
				Lname VARCHAR(max) not null,
				PhoneNo INT,
				CustID INT not null,
				NIF INT
			)

			SET NOCOUNT ON;

			IF @Fname <> 'None'
			BEGIN
					INSERT INTO @tempTable (Email, Fname, Lname, PhoneNO, CustID, NIF)
					SELECT Person.Email, Person.Fname, Person.Lname, Person.phoneNo, CustID, NIF FROM TravelAgency.Customer Join TravelAgency.Person ON TravelAgency.Customer.Email = TravelAgency.Person.Email
					WHERE Fname like '%'+@fname+'%'
			END

			ELSE
			BEGIN
					INSERT INTO @tempTable SELECT Person.Email, Person.Fname, Person.Lname, Person.phoneNo, CustID, NIF FROM TravelAgency.Customer Join TravelAgency.Person ON TravelAgency.Customer.Email = TravelAgency.Person.Email
			END

			SELECT tt.* FROM @tempTable tt
	END

exec TravelAgency.spFilterCustomer Marcia, Pires