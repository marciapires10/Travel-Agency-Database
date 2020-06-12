CREATE SCHEMA TravelAgency;
GO

CREATE TABLE TravelAgency.Person(
	Email			VARCHAR(60) NOT NULL,
	Fname			VARCHAR(20) NOT NULL,
	Lname			VARCHAR(20)	NOT NULL,
	PhoneNo			INT			check(PhoneNo > 100000000),
	PRIMARY KEY(Email),
);


-- Old Agent Table - not in use
CREATE TABLE TravelAgency.Agent(
	AgID			INT			NOT NULL,
	Password		VARCHAR(100)	NOT NULL,
	Email			VARCHAR(60)	NOT NULL,
	PRIMARY KEY(AgID),
	FOREIGN KEY(Email) REFERENCES TravelAgency.Person(Email),
);


-- New Agent Table
CREATE TABLE [TravelAgency].[Agent](
	[AgID] [int] NOT NULL	identity(1,1),
	[Password] [varbinary](20),
	[Salt] CHAR(25),
	[Email] [varchar](60) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[AgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [TravelAgency].[Agent]  WITH CHECK ADD  CONSTRAINT [FK__Agent__Email__20CCCE1C] FOREIGN KEY([Email])
REFERENCES [TravelAgency].[Person] ([Email])
GO
ALTER TABLE [TravelAgency].[Agent] CHECK CONSTRAINT [FK__Agent__Email__20CCCE1C]
GO



CREATE TABLE TravelAgency.Customer(
	CustID			INT			NOT NULL	identity(1,1),
	NIF				INT			check(NIF > 100000000),
	Email			VARCHAR(60)	NOT NULL,
	PRIMARY KEY(CustID),
	FOREIGN KEY(Email) REFERENCES TravelAgency.Person(Email),
);

CREATE TABLE TravelAgency.CC(
	City		VARCHAR(20)		NOT NULL,
	Country		VARCHAR(20)		NOT NULL,
	PRIMARY KEY(City),
);


CREATE TABLE TravelAgency.Accommodation(
	ID			INT				NOT NULL	identity(1,1),
	Name		VARCHAR(40)		NOT NULL	unique,
	Image		VARCHAR(MAX),
	Description VARCHAR(500),
	Price		SMALLMONEY		NOT NULL,
	CC_Location	VARCHAR(20)		NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(CC_Location) REFERENCES TravelAgency.CC(City),
);



CREATE TABLE TravelAgency.Promo(
	ID			INT		NOT NULL identity(1,1),
	ACTIVE		BIT		NOT NULL,
	DISCOUNT	INT		NOT NULL,
	PRIMARY KEY(ID),
);

CREATE TABLE TravelAgency.Package(
	ID				INT				NOT NULL	identity(1,1),
	Title			VARCHAR(40)		NOT NULL,
	Description		VARCHAR(500),
	Duration		INT				NOT NULL,
	startDate		DATE			NOT NULL,
	endDate			DATE			NOT NULL,
	noPersons		INT				NOT NULL,
	totalPrice		SMALLMONEY		NOT NULL,
	Acomm_ID		INT				NOT NULL,
	Promo_ID		INT				NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(Acomm_ID) REFERENCES TravelAgency.Accommodation(ID),
	FOREIGN KEY(Promo_ID) REFERENCES TravelAgency.Promo(ID),
);

CREATE TABLE TravelAgency.Airline(
	ICAO	NCHAR(3)	NOT NULL,
	Name	NVARCHAR(30)	NOT NULL,
	PRIMARY KEY(ICAO),
);

CREATE TABLE TravelAgency.Flight(
	ID			INT				NOT NULL	identity(1,1),
	departTime	SMALLDATETIME	NOT NULL,
	arrivalTime	SMALLDATETIME	NOT NULL,
	Airline		VARCHAR(20)		NOT NULL,
	classType	VARCHAR(12)		NOT NULL,
	Price		SMALLMONEY		NOT NULL,
	CC_Depart	VARCHAR(20)		NOT NULL,
	CC_Arriv	VARCHAR(20)		NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(CC_Depart) REFERENCES TravelAgency.CC(City),
	FOREIGN KEY(CC_Arriv) REFERENCES TravelAgency.CC(City),
);

ALTER TABLE TravelAgency.Flight ALTER COLUMN Airline NCHAR(3) NOT NULL;
ALTER TABLE TravelAgency.Flight ADD FOREIGN KEY(Airline) REFERENCES TravelAgency.Airline(ICAO);


CREATE TABLE TravelAgency.Transfer(
	ID			INT			NOT NULL	identity(1,1),
	Company		VARCHAR(20)	NOT NULL,
	Price		SMALLMONEY	NOT NULL,
	Included	BIT			NOT NULL,
	CC_Depart	VARCHAR(20)	NOT NULL,
	CC_Arriv	VARCHAR(20)	NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(CC_Depart) REFERENCES TravelAgency.CC(City),
	FOREIGN KEY(CC_Arriv) REFERENCES TravelAgency.CC(City),
);


CREATE TABLE TravelAgency.Booking(
	ID			INT				NOT NULL	identity(1,1),
	Paid		BIT				NOT NULL,
	bookDate	DATE			NOT NULL,
	Details		VARCHAR(500),
	Pack_ID		INT				NOT NULL,
	Ag_ID		INT				NOT NULL,
	Cust_ID		INT				NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(Pack_ID) REFERENCES TravelAgency.Package(ID),
	FOREIGN KEY(Ag_ID) REFERENCES [TravelAgency].[Agent]([AgID]),
	FOREIGN KEY(Cust_ID) REFERENCES TravelAgency.Customer(CustID),
);

CREATE TABLE TravelAgency.Review(
	ID			INT				NOT NULL	identity(1,1),
	Description	VARCHAR(500),
	Score		INT				NOT NULL,
	Pack_ID		INT				NOT NULL,
	Cust_ID		INT				NOT NULL,
	PRIMARY KEY(ID),
	FOREIGN KEY(Pack_ID) REFERENCES TravelAgency.Package(ID),
	FOREIGN KEY(Cust_ID) REFERENCES TravelAgency.Customer(CustID),
);


CREATE TABLE TravelAgency.Contains_Transf(
	Pack_ID		INT		NOT NULL,
	Transf_ID	INT		NOT NULL,
	PRIMARY KEY(Pack_ID, Transf_ID),
	FOREIGN KEY(Pack_ID) REFERENCES TravelAgency.Package(ID),
	FOREIGN KEY(Transf_ID) REFERENCES TravelAgency.Transfer(ID),
);


CREATE TABLE TravelAgency.Contains_Flight(
	Pack_ID		INT		NOT NULL,
	Flight_ID	INT		NOT NULL,
	PRIMARY KEY(Pack_ID, Flight_ID),
	FOREIGN KEY(Pack_ID)	REFERENCES TravelAgency.Package(ID),
	FOREIGN KEY(Flight_ID)	REFERENCES TravelAgency.Flight(ID),
);

