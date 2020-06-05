CREATE UNIQUE INDEX NDX_SecurityAccounts_AccountName 
    ON TravelAgency.Agent (AgID) INCLUDE (Salt, Password);
GO 

CREATE PROC TravelAgency.CreateAgent
  @NewAgID int,
  @NewAccountPwd VARCHAR(100),
  @Email	VARCHAR(60)
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

  INSERT INTO TravelAgency.Agent 
  (AgID, Salt, Password, Email)
  VALUES (@NewAgID, @Salt, HASHBYTES('SHA1', @PwdWithSalt), @Email);
END;
GO 




