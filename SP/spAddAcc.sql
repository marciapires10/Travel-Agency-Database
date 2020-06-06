CREATE PROCEDURE TravelAgency.spAddAcc
	@name VARCHAR(max),
	@image VARBINARY(max),
	@description VARCHAR(max),
	@price SMALLMONEY,
	@location VARCHAR(max),
	@responseMsg NVARCHAR(250) output

AS
	BEGIN
			SET NOCOUNT ON;

			BEGIN TRY
						INSERT INTO TravelAgency.Accommodation (Name, Image, Description, Price, CC_Location)
						VALUES (@name, @image, @description, @price, @location)

						SET @responseMsg = 'Success'
			END TRY

			BEGIN CATCH
						SET @responseMsg = error_message()
			END CATCH
END
