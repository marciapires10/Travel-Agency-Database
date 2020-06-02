CREATE PROC TravelAgency.VerifyAgent
  @Email VARCHAR(60),
  @AccountPwd VARCHAR(100)
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @Salt CHAR(25);
  DECLARE @PwdWithSalt VARCHAR(125);
  DECLARE @PwdHash VARBINARY(20);  

  SELECT @Salt = Salt, @PwdHash = Password 
  FROM TravelAgency.Agent WHERE Email = @Email;
  
  SET @PwdWithSalt = @Salt + @AccountPwd;

  IF (HASHBYTES('SHA1', @PwdWithSalt) = @PwdHash)
    RETURN 1;
  ELSE
    RETURN 0;

END;
GO 