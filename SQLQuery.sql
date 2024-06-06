create database RewardGame_490

alter database RewardGame_490
set multi_user

use RewardGame_490

CREATE TABLE Users(
	userID int Identity(1,1),
	username varchar(30),
	email varchar(80),
	password varchar(30)
	CONSTRAINT pk_users PRIMARY KEY (userID)
)

drop TABLE Transactions

CREATE TABLE Wallet(
	walletID int Identity(1,1),
	userID int,
	balance decimal(7,2),
	chancesLeft smallint
	CONSTRAINT pk_wallet PRIMARY KEY(walletID)
	CONSTRAINT fk_wallet_userid FOREIGN KEY (userID) REFERENCES Users(userid)
)

CREATE table Transactions(
	transactionID int identity(1,1),
	walletID int,
	transactionAmount decimal(5,2),
	transactionType char(1),
	transactionTime DateTime,
	closingAmount decimal(7,2)
	CONSTRAINT pk_transactions PRIMARY KEY (transactionID)
	CONSTRAINT fk_transactions_walletID FOREIGN KEY(walletID) REFERENCES Wallet (walletID)
)



	Alter proc CheckTransactionLimit
	 @userID int,
	 @output int output
	 As
	 BEGIN
			declare @firstTransactionID as int;
			select top 1 @firstTransactionID = transactionID 
			from Transactions as t
			inner join wallet as w
			on t.walletID = w.walletID
			where w.userID = @userID			

			select @output = SUM(transactionAmount) from 
			Transactions as t
			inner join wallet as w
			on w.walletID = t.walletID
			inner join Users as u
			on u.userID = w.userID
			where u.userID = @userID AND cast(t.transactionTime as date) = cast(GETDate() as date) AND transactionType = 'C' AND transactionID != @firstTransactionID
	 END

	

	alter proc getSum
	@userID int
	as
	begin
		declare @sum int;
		exec dbo.CheckTransactionLimit @userID=@userID, @output = @sum out
		select @sum as SUM
	end

exec dbo.getSum @userID=3

select * from transactions