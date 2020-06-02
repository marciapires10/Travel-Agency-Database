CREATE PROC TravelAgency.VerifyAccount
  @AgID int,
  @AccountPwd VARCHAR(100)
AS
BEGIN
  SET NOCOUNT ON;

  DECLARE @Salt CHAR(25);
  DECLARE @PwdWithSalt VARCHAR(125);
  DECLARE @PwdHash VARBINARY(20);  

  SELECT @Salt = Salt, @PwdHash = Password 
  FROM TravelAgency.Agent WHERE AgID = @AgID;
  
  SET @PwdWithSalt = @Salt + @AccountPwd;

  IF (HASHBYTES('SHA1', @PwdWithSalt) = @PwdHash)
    RETURN 0;
  ELSE
    RETURN 1;

END;
GO 