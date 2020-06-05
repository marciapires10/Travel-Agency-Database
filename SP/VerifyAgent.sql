CREATE PROC TravelAgency.VerifyAgent
  @Email VARCHAR(60),
  @AccountPwd VARCHAR(100),
  @Result int OUTPUT
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
	set @Result = 1;
    --RETURN 1;
  ELSE
	set @Result = 0;
    --RETURN 0;

END;
GO 