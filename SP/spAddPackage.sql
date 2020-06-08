CREATE PROCEDURE TravelAgency.spAddPackage
		@Title VARCHAR(max),
		@Description VARCHAR(max),
		@Duration INT,
		@startDate DATE,
		@endDate DATE,
		@noPersons INT,
		@totalPrice SMALLMONEY,
		@Acomm_ID INT,
		@Promo_ID INT,
		@Flight_ID1 INT,
		@Flight_ID2 INT,
		@Transf_ID INT,
		@opt1 VARCHAR(max)

AS
	BEGIN
			SET NOCOUNT ON;

			BEGIN 
					IF @Description = ''
					BEGIN
							SET @Description = NULL
					END

					IF EXISTS((Select ID from TravelAgency.Accommodation where @Acomm_ID = ID) UNION (Select ID from TravelAgency.Promo where @Promo_ID = ID))
					BEGIN
						INSERT INTO TravelAgency.Package (Title, Description, Duration, startDate, endDate, noPersons, totalPrice, Acomm_ID, Promo_ID)
						VALUES (@Title, @Description, @Duration, @startDate, @endDate, @noPersons, @totalPrice, @Acomm_ID, @Promo_ID)
					END

			END

			BEGIN
					DECLARE @Pack_ID INT
					SET @Pack_ID = SCOPE_IDENTITY()

					IF @opt1 = 'Yes'
					BEGIN
							INSERT INTO TravelAgency.Contains_Transf(Pack_ID, Transf_ID)
							VALUES (@Pack_ID, @Transf_ID)
					END

					INSERT INTO TravelAgency.Contains_Flight(Pack_ID, Flight_ID)
					VALUES (@Pack_ID, @Flight_ID1)

					INSERT INTO TravelAgency.Contains_Flight(Pack_ID, Flight_ID)
					VALUES (@Pack_ID, @Flight_ID2)

			END	
	END
go

--exec TravelAgency.spAddPackage 'oi', 'oi', 8, '2020-06-08', '2020-06-15', 2, 800, 203, 1, 2, 4, 1, 'Yes'
