INSERT INTO TravelAgency.Agent([AgID],[Password],[Email]) VALUES(1,'CLJ76KJO1JB','gravida.Aliquam@quis.org'),(2,'WNV63NAB5IX','in@tellusNunclectus.edu'),(3,'XFE33OHT6ZM','Sed.dictum.Proin@seddolor.net'),(4,'UGO58OPU4CF','faucibus.ut@massa.net');

USE p1g9; 
GO 

EXEC TravelAgency.CreateAccount @NewAgID = 1, @NewAccountPwd = 'CLJ76KJO1JB', @Email = 'gravida.Aliquam@quis.org';
GO 

SELECT * FROM TravelAgency.SecurityAccounts;

DECLARE @Result TINYINT;

EXEC @Result = TravelAgency.VerifyAccount @AgID = 1, @AccountPwd = 'CLJ76KJO1JB';
SELECT @Result;


EXEC @Result = TravelAgency.VerifyAccount @AgID = 1, @AccountPwd = 'WeakP4ssw0rd!';
SELECT @Result;
GO 




INSERT INTO Agent([AgID],[Password],[Email]) VALUES(5,'KDH55IBK1GC','vulputate@Nullamutnisi.org'),(6,'MSO18BUA2MS','diam.luctus@rutrumFuscedolor.com'),(7,'TTG43JTO2XV','tincidunt@nec.com'),(8,'OBX18ZIN7CL','dui.quis.accumsan@nequeInornare.ca');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(9,'AQG67HFR3JC','orci.consectetuer@lorem.net'),(10,'UPT50ETI2RW','commodo.at.libero@Fuscealiquet.net'),(11,'DNW91ZLV5ET','neque@Crasinterdum.edu'),(12,'HLO97EKW9BC','leo@in.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(13,'SEN12HXX5OE','non.enim.Mauris@mauris.org'),(14,'DQF11JUK6DA','Curabitur.ut.odio@Morbi.com'),(15,'LCN45TRG1MU','libero@luctusaliquetodio.co.uk'),(16,'GGE32MMK5AV','lectus.rutrum@ut.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(17,'TWF13DMC5KI','nec.tempus@Proinvel.edu'),(18,'BKH01MYT1ZR','dolor@In.edu'),(19,'YSX49NIU6TS','vulputate@Nullamutnisi.org'),(20,'MQI13JXC8RS','feugiat@penatibusetmagnis.net');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(21,'RRD72ZVZ9FB','erat.eget@egestas.co.uk'),(22,'NIA33VJP6XC','Pellentesque.habitant@etipsum.org'),(23,'YWU15OMS8FP','libero.Donec@quis.co.uk'),(24,'ZVT05WSG4JV','Etiam@Aenean.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(25,'TBB75PIE7DH','enim.diam@enim.ca'),(26,'EUH05OZK9YK','dolor.nonummy@eu.co.uk'),(27,'PRZ02TRV2RM','dui.quis.accumsan@nequeInornare.ca'),(28,'PFV28FUT5BT','laoreet.lectus.quis@ipsumsodalespurus.co.uk');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(29,'TES96OCQ1XF','nunc.est.mollis@magnaaneque.net'),(30,'EUU28ENW3LG','in@tellusNunclectus.edu'),(31,'ZZL13SGA9QD','Aenean@Sed.co.uk'),(32,'FDD44WMT8JO','Aliquam@Quisqueaclibero.com');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(33,'PNA71IEC9EH','urna@sitamet.co.uk'),(34,'MEZ65APJ2WL','Aliquam.adipiscing@diam.ca'),(35,'ZFN67PXC8PG','odio.a@scelerisquesed.ca'),(36,'XMX00GWS5HF','lectus.rutrum@ut.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(37,'LYS15PMZ5BV','libero.Donec@quis.co.uk'),(38,'HLM38KWF8OW','sapien.molestie.orci@vitaesodalesat.ca'),(39,'EEU03HRR3OT','eleifend.nec@lobortisClass.co.uk'),(40,'HZW82LIJ4VG','lacus.varius.et@nislQuisque.co.uk');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(41,'GGY36TZI5TL','risus.varius@pede.net'),(42,'TQP82MNX0JZ','Cras@venenatisamagna.ca'),(43,'LBZ45KYJ6VQ','Etiam@Aenean.org'),(44,'ZAM91MAA9QX','sollicitudin.a@iaculisneceleifend.edu');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(45,'ENI89FGZ8BB','Aliquam@Quisqueaclibero.com'),(46,'TLC31IHY4RI','dui@nequepellentesque.edu'),(47,'SXR48OYM3YQ','mauris.eu@mifelisadipiscing.ca'),(48,'MFU12FIU1ET','accumsan@Nullam.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(49,'CIJ80JBM1PY','leo@ametloremsemper.edu'),(50,'CXZ85ZJX8LM','Aenean@Sed.co.uk'),(51,'LWC51YTL3LC','lorem.ipsum@Morbisitamet.co.uk'),(52,'GQX64HCV3OG','enim.diam@enim.ca');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(53,'KDT82RRU8OC','velit.justo@semmolestie.com'),(54,'OTE60KQT1GA','urna@sitamet.co.uk'),(55,'VJI89ODO6UK','lacus@volutpat.edu'),(56,'UFB08XFX2MA','Cras@venenatisamagna.ca');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(57,'IOD81PXU5WZ','Nulla@ipsumnunc.ca'),(58,'ROV86MAT7VI','feugiat@penatibusetmagnis.net'),(59,'COY80QWC3LR','nulla.Donec.non@dapibusligula.co.uk'),(60,'KRS98KYN2JG','Nunc@Donecsollicitudin.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(61,'SRF05VAF3BC','Pellentesque.habitant@etipsum.org'),(62,'KSD92MJB2SH','neque@Crasinterdum.edu'),(63,'BEE93TXQ3SN','ut@convallisestvitae.net'),(64,'HFD15SHZ3JK','commodo.at.libero@Fuscealiquet.net');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(65,'APN03PEO3ZQ','tincidunt@nec.com'),(66,'LAH38EHX7UG','in.consequat@mi.edu'),(67,'ISQ05VIM7GJ','non.enim.Mauris@mauris.org'),(68,'SJM28QRU3TT','Pellentesque.habitant.morbi@acmetusvitae.net');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(69,'RIX91AKG6YD','vulputate@Nullamutnisi.org'),(70,'ZMK92TUM9JH','aliquet.Proin@mifelisadipiscing.edu'),(71,'AMR54PRN9BB','magnis@quis.ca'),(72,'KNP01XLG3HD','tincidunt@nec.com');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(73,'OPP53QCN6GX','velit.justo@semmolestie.com'),(74,'VDI31BUN7YB','ligula@feugiattellus.edu'),(75,'BSV29QHH0MR','lacus.varius.et@nislQuisque.co.uk'),(76,'INR31RSR3ZA','vitae@velarcuCurabitur.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(77,'CAT12WTN2YH','nunc.est.mollis@magnaaneque.net'),(78,'YBC38OCN1KT','lectus.ante.dictum@lectussitamet.net'),(79,'KKB89QGX1QM','urna@sitamet.co.uk'),(80,'KZW62IIA8KH','non.enim.Mauris@mauris.org');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(81,'UOC71ZAZ9ZT','Donec.sollicitudin.adipiscing@Integertinciduntaliquam.co.uk'),(82,'CDI35EJJ2TZ','diam.luctus@rutrumFuscedolor.com'),(83,'HVX38JSG9VX','mauris.eu@mifelisadipiscing.ca'),(84,'PAL94RAR4OR','urna@sitamet.co.uk');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(85,'NAV99EWK5IO','sapien@dui.co.uk'),(86,'FYP80QNW0HS','leo@in.org'),(87,'GQP82XXN6DH','dui@nequepellentesque.edu'),(88,'MAV25CBG1JM','dis.parturient@amalesuadaid.net');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(89,'AOB41DIX0BO','Curabitur.ut.odio@Morbi.com'),(90,'PUP32UIR9MU','neque@Crasinterdum.edu'),(91,'VCC61TVB1FJ','placerat.velit.Quisque@ac.co.uk'),(92,'ASS45NUM7NA','risus.varius@pede.net');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(93,'RMM79BEH7MJ','dolor@In.edu'),(94,'JVI62EDM9GC','ornare.Fusce@DonectinciduntDonec.org'),(95,'VGY90FNK8KB','Donec.sollicitudin.adipiscing@Integertinciduntaliquam.co.uk'),(96,'VHD54MID3PW','dis.parturient@amalesuadaid.net');
INSERT INTO Agent([AgID],[Password],[Email]) VALUES(97,'CFC10MSP9SR','commodo.at.libero@Fuscealiquet.net'),(98,'DTY65OFD1KO','dui.Suspendisse@atiaculis.net'),(99,'RWN59HLV5KL','erat.eget@egestas.co.uk'),(100,'RRL75YNS8CW','sollicitudin.a@iaculisneceleifend.edu');
