CREATE PROCEDURE TravelAgency.spEditAgent
		@Fname varchar(max),
		@Lname varchar(max),
		@Email varchar(max),
		@Password varchar(max),
		@PhoneNo INT,
		@responseMsg NVARCHAR(250) output

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


			SET @PwdWithSalt = @Salt + @Password;
			
			BEGIN TRY
						UPDATE TravelAgency.Person
						SET Fname = @Fname, Lname = @Lname, PhoneNo = @PhoneNo
						WHERE Email = @Email

						UPDATE TravelAgency.Agent
						SET Salt = @Salt, Password = HASHBYTES('SHA1', @PwdWithSalt)
						WHERE Email = @Email

						SET @responseMsg = 'Success'

			END TRY

			BEGIN CATCH	
						SET @responseMsg = error_message()
			END CATCH

	END



		