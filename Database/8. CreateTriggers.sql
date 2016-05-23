SET NOCOUNT ON;

DECLARE 
	@Tables TABLE 
	( 
		Name NVARCHAR(128) NOT NULL
	)

INSERT INTO @Tables (Name)
VALUES 
	('Showtime'),
	('Movie'),
	('Genre'),
	('Auditorium'),
	('Logo'),
	('Ticket')

DECLARE TablesCursor CURSOR FOR SELECT Name FROM @Tables

DECLARE
	@TableName NVARCHAR(128),
	@Name NVARCHAR(128),
	@Sql NVARCHAR(4000),
	@IdValue NVARCHAR(4)
 
OPEN TablesCursor
FETCH NEXT FROM TablesCursor INTO @TableName

WHILE @@FETCH_STATUS=0
BEGIN
	-- If table has [Id] column then we will take it into [dbo].[Log]
	IF EXISTS(SELECT * 
			  FROM sys.columns 
			  WHERE Name = N'Id' AND Object_ID = Object_ID(@TableName))
	BEGIN
		SET @IdValue = '[Id]'
	END
	ELSE 
	BEGIN
		SET @IdValue = 'NULL'
	END

	/* INSERT TRIGGER */

	SET @Name = @TableName + 'InsertTrigger'

	IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsTrigger') = 1)
	BEGIN
		SET @Sql = 'DROP TRIGGER [' + @Name + ']'
		EXEC (@Sql)
	END

	SET @Sql = 'CREATE TRIGGER [' + @Name + '] ON [' + @TableName + '] FOR INSERT
				AS
				BEGIN
					SET NOCOUNT ON

					INSERT INTO [Log]
					(
						[User], 
						[EntityTable], 
						[EntityId], 
						[OperationType]
					)
					SELECT
						CURRENT_USER, 
						N''' + @TableName + ''', 
						' + @IdValue + ',
						1
					FROM
						[inserted]

					RETURN
				END'
	EXEC (@Sql)

	SET @Name = @TableName + 'UpdateTrigger'
	
	/* UPDATE TRIGGER */

	IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsTrigger') = 1)
	BEGIN
		SET @Sql = 'DROP TRIGGER [' + @Name + ']'
		EXEC (@Sql)
	END

	SET @Sql = 'CREATE TRIGGER [' + @Name + '] ON [' + @TableName + '] FOR UPDATE
			AS
			BEGIN
				SET NOCOUNT ON

				INSERT INTO [Log]
				(
					[User], 
					[EntityTable], 
					[EntityId], 
					[OperationType]
				)
				SELECT
					CURRENT_USER, 
					N''' + @TableName + ''', 
					' + @IdValue + ',
					0
				FROM
					[inserted]

				RETURN
			END'
	EXEC (@Sql)

	/* DELETE TRIGGER */

	SET @Name = @TableName + 'DeleteTrigger'
	
	IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsTrigger') = 1)
	BEGIN
		SET @Sql = 'DROP TRIGGER [' + @Name + ']'
		EXEC (@Sql)
	END

	SET @Sql = 'CREATE TRIGGER [' + @Name + '] ON [' + @TableName + '] FOR DELETE
			AS
			BEGIN
				SET NOCOUNT ON

				INSERT INTO [Log]
				(
					[User], 
					[EntityTable], 
					[EntityId], 
					[OperationType]
				)
				SELECT
					CURRENT_USER, 
					N''' + @TableName + ''', 
					' + @IdValue + ',
					-1
				FROM
					[deleted]

				RETURN
			END'
	EXEC (@Sql)

	FETCH NEXT FROM TablesCursor INTO @TableName
END
CLOSE TablesCursor
DEALLOCATE TablesCursor
GO