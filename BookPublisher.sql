USE master
GO

DROP DATABASE BookPublisher

CREATE DATABASE BookPublisher
GO

USE BookPublisher
GO

CREATE TABLE AccountUser(
  UserID nvarchar(20) primary key,
  UserPassword nvarchar(90) not null,
  UserFullName nvarchar(100), 
  UserRole int 
)
GO
INSERT INTO AccountUser VALUES(N'martin',N'123', N'Martin', 2);
INSERT INTO AccountUser VALUES(N'admin',N'123', N'Administrator', 1);
GO

CREATE TABLE Publisher(
  PublisherID nvarchar(20) primary key,
  PublisherName nvarchar(100) not null,
  Description nvarchar(200)
)
GO

CREATE TABLE Book (
 BookID nvarchar(20) primary key,
 BookName nvarchar(150) not null,
 Quantity int,
 AuthorName nvarchar(150),
 PublisherID nvarchar(20) references Publisher(PublisherID) on delete cascade on update cascade
)
GO

INSERT INTO Publisher VALUES(N'PU0001',N'McGraw-Hill', N'McGraw-Hill Description')
INSERT INTO Publisher VALUES(N'PU0002',N'Macmillan Publishers', N'Macmillan Publishers Description')
INSERT INTO Publisher VALUES(N'PU0003',N'Houghton Mifflin Harcourt', N'McGraw-Hill Description')
INSERT INTO Publisher VALUES(N'PU0004',N'Cengage Learning', N'Cengage Learning Description')

INSERT INTO Book VALUES(N'BK0001',N'The Sun Also Rises', 100, N'Ernest Miller Hemingway', N'PU0001')
INSERT INTO Book VALUES(N'BK0002',N'In Our Time', 250, N'Ernest Miller Hemingway', N'PU0003')
INSERT INTO Book VALUES(N'BK0003',N'The Old Man and the Sea', 400, N'Ernest Miller Hemingway', N'PU0004')
INSERT INTO Book VALUES(N'BK0004',N'A Friend of Kafka', 200, N'Franz Kafka', N'PU0002')
INSERT INTO Book VALUES(N'BK0005',N'The Trial', 120, N'Franz Kafka', N'PU0004')
INSERT INTO Book VALUES(N'BK0006',N'A Letter to Elise', 310, N'Franz Kafka', N'PU0002')
INSERT INTO Book VALUES(N'BK0007',N'The Metamorphosis', 100, N'Franz Kafka', N'PU0002')

SELECT * FROM dbo.AccountUser