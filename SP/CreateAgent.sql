CREATE UNIQUE INDEX NDX_SecurityAccounts_AccountName 
    ON TravelAgency.Agent (AgID) INCLUDE (Salt, Password);
GO 
CREATE PROC TravelAgency.CreateAgent
	@Fname VARCHAR(20),
	@Lname VARCHAR(20),
	@PhoneNo INT,
	@NewAccountPwd VARCHAR(100),
	@Email VARCHAR(60),
	@responseMsg nvarchar(250) output
AS
BEGIN

  SET NOCOUNT ON;

  DECLARE @Salt VARCHAR(25);
  DECLARE @PwdWithSalt VARCHAR(125);

  -- Generate the salt
  DECLARE @Seed int;
  DECLARE @LCV tinyint;
  DECLARE @CTime DATETIME;

  SET @CTime = GETDATE();
  SET @Seed = (DATEPART(hh, @Ctime) * 10000000) + (DATEPART(n, @CTime) * 100000)
      + (DATEPART(s, @CTime) * 1000) + DATEPART(ms, @CTime);
  SET @LCV = 1;
  SET @Salt = CHAR(ROUND((RAND(@Seed) * 94.0) + 32, 3));

  WHILE (@LCV < 25)
  BEGIN
    SET @Salt = @Salt + CHAR(ROUND((RAND() * 94.0) + 32, 3));
	SET @LCV = @LCV + 1;
  END;


  SET @PwdWithSalt = @Salt + @NewAccountPwd;
  
  BEGIN TRY
	 
	  IF EXISTS(Select Email from [TravelAgency].[Agent] Where @Email=Email)
		BEGIN
			PRINT('Already exists Agent with this Email')
		END
	  ELSE
		BEGIN
			INSERT INTO TravelAgency.Agent 
			(Salt, Password, Email)
			VALUES (@Salt, HASHBYTES('SHA1', @PwdWithSalt), @Email)
		END

		
	  IF EXISTS (Select Email From TravelAgency.Person Where @Email=Email)
		BEGIN
		  PRINT('Already exists')
		  SET @responseMsg = error_message()
		END
	  ELSE
		BEGIN
		  INSERT INTO TravelAgency.Person (Email, Fname, Lname, PhoneNo)
		  VALUES (@Email, @Fname, @Lname, @PhoneNo)
		END

	  SET @responseMsg = 'Success'

   END TRY

   BEGIN CATCH
      SET @responseMsg = error_message()
   END CATCH

END;
GO

exec TravelAgency.CreateAgent Marcia, Jesus, 918573945, projetodebd, 'marcia.pires@ua.pt', 'Success'