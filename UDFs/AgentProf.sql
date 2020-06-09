CREATE FUNCTION TravelAgency.AgentProf (@Email varchar(60))
RETURNS TABLE

AS
	RETURN(Select Person.Email, Person.Fname, Person.Lname, Person.phoneNo, AgID, Password, Salt 
			from TravelAgency.Agent Join TravelAgency.Person 
			ON TravelAgency.Agent.Email = TravelAgency.Person.Email
			WHERE TravelAgency.Person.Email = @Email)

go

--select * from TravelAgency.AgentProf ('marcia.pires@ua.pt')

