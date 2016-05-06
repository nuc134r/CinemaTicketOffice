SET NOCOUNT ON

DECLARE auditFields CURSOR LOCAL FORWARD_ONLY READ_ONLY FOR
	SELECT 
		name, object_name(id) FROM syscolumns 
	WHERE 
		name IN ('CreatedBy', 'ModifiedBy')
	AND 
		OBJECTPROPERTY(id, N'IsUserTable') = 1
OPEN auditFields

DECLARE @columnName sysname, @tableName sysname, @sql nvarchar(4000), @name varchar(128)

FETCH NEXT FROM auditFields INTO @columnName, @tableName
WHILE @@FETCH_STATUS = 0
BEGIN
	
	SET @name = 'adfDefault'+@tableName+@columnName
	
	IF EXISTS(SELECT * FROM sysobjects where id = object_id(@name) and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)
	BEGIN
		SELECT @sql = 'ALTER TABLE [' + @tableName + '] DROP CONSTRAINT [' + @name + ']'
		EXEC sp_executesql @sql
	END			
	SET @sql = 'ALTER TABLE [' + @tableName + '] ADD CONSTRAINT [' + @name + ']
		DEFAULT dbo.adfGetCurrentUserID() FOR ' + @columnName
	EXEC sp_executesql @sql
	FETCH NEXT FROM auditFields INTO @columnName, @tableName
END

CLOSE auditFields

DEALLOCATE auditFields
GO

SET NOCOUNT ON

DECLARE classes CURSOR FOR SELECT [Class], [TableName] FROM [Class] WHERE [TableName] IS NOT NULL

OPEN classes

DECLARE @Class TClass, @TableName TSysname
DECLARE @Sql nvarchar(4000)
DECLARE @Name sysname

FETCH NEXT FROM classes INTO @Class, @TableName
WHILE @@FETCH_STATUS = 0
BEGIN
	IF EXISTS(SELECT * FROM [sysobjects] WHERE [name] = @TableName AND xtype='U')
	BEGIN
		SET @Name = 'adfDefault' + @TableName + 'Class'
		IF EXISTS(SELECT * FROM sysobjects where id = object_id(@Name) and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)
		BEGIN
			SELECT @Sql = 'ALTER TABLE [' + @TableName + '] DROP CONSTRAINT [' + @Name + ']'
			EXEC sp_executesql @Sql
		END			
		SELECT @Sql = 'ALTER TABLE [' + @TableName + '] ADD CONSTRAINT [' + @Name + '] 
		DEFAULT (' + CAST(@Class as nvarchar(10)) + ') FOR [Class]'
		EXEC sp_executesql @Sql
		
		SET @Name = 'adfDefault' + @TableName + 'Flag'
		IF EXISTS(SELECT * FROM sysobjects where id = object_id(@Name) and OBJECTPROPERTY(id, N'IsDefaultCnst') = 1)
		BEGIN
			SELECT @Sql = 'ALTER TABLE [' + @TableName + '] DROP CONSTRAINT [' + @Name + ']'
			EXEC sp_executesql @Sql
		END			

		SELECT @Sql = 'ALTER TABLE [' + @TableName + '] ADD CONSTRAINT [' + @Name + '] 
		DEFAULT (0) FOR [Flag]'
		EXEC sp_executesql @Sql

		SET @Name = 'adfCheck' + @TableName + 'Class'
		IF EXISTS(SELECT * FROM sysobjects where id = object_id(@Name) and OBJECTPROPERTY(id, N'IsCheckCnst') = 1)
		BEGIN
			SELECT @Sql = 'ALTER TABLE [' + @TableName + '] DROP CONSTRAINT [' + @Name + ']'
			EXEC sp_executesql @Sql
		END			
		SELECT @Sql = 'ALTER TABLE [' + @TableName + '] ADD CONSTRAINT [' + @Name + ']
		CHECK ([Class] = ' + CAST(@Class as nvarchar(10)) + ')'
		EXEC sp_executesql @Sql
		
		-- creating triggers
		-- creating insert trigger		
		SET @Name = 'adftr' + @TableName + 'Insert'
		IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsTrigger') = 1)
		BEGIN
			SET @Sql = 'DROP TRIGGER [' + @Name + ']'
			EXEC sp_executesql @Sql
		END
		SET @Sql = 'CREATE TRIGGER [' + @Name + '] ON [' + @TableName + '] FOR INSERT
AS
BEGIN
    IF @@rowcount = 0
       RETURN

    SET NOCOUNT ON

	DECLARE @CurrentDate DateTime = dbo.adfGetLocalDateByOriginalLogin()

    INSERT INTO [Document] 
        ([ID], [Class], [State], [Flag], [FileAs], [Created], [CreatedBy], [Modified], [ModifiedBy], [Timestamp])
    SELECT
        [ID], [Class], [State], [Flag], [FileAs], @CurrentDate, [CreatedBy], @CurrentDate, [ModifiedBy], [Timestamp]
    FROM
        [inserted]
    RETURN
END
'
		EXEC sp_executesql @Sql

		-- creating update trigger		
		SET @Name = 'adftr' + @TableName + 'Update'
		IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsTrigger') = 1)
		BEGIN
			SET @Sql = 'DROP TRIGGER [' + @Name + ']'
			EXEC sp_executesql @Sql
		END
		SET @Sql = 'CREATE TRIGGER [' + @Name + '] ON [' + @TableName + '] FOR UPDATE
AS
BEGIN
   
    IF @@rowcount = 0
       RETURN

    SET NOCOUNT ON

    UPDATE [Document] 
    SET
        [State] = [inserted].[State], 
        [Flag] = [inserted].[Flag], 
        [FileAs] = [inserted].[FileAs], 
        [Modified] = dbo.adfGetLocalDateByOriginalLogin(), 
        [ModifiedBy] = [inserted].[ModifiedBy], 
        [Timestamp] = [inserted].[Timestamp]
    FROM
        [Document] INNER JOIN [inserted]
            ON ([Document].[ID] = [inserted].[ID] AND [Document].[Class] = [inserted].[Class])
    RETURN
END
'
		EXEC sp_executesql @Sql

		-- creating delete trigger		
		SET @Name = 'adftr' + @TableName + 'Delete'
		IF EXISTS(SELECT * FROM sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsTrigger') = 1)
		BEGIN
			SET @Sql = 'DROP TRIGGER [' + @Name + ']'
			EXEC sp_executesql @Sql
		END
		SET @Sql = 'CREATE TRIGGER [' + @Name + '] ON [' + @TableName + '] FOR DELETE
AS
BEGIN
    IF @@rowcount = 0
       RETURN

    SET NOCOUNT ON
    -- delete links
    DELETE [Attachment]
    FROM
		[Attachment] INNER JOIN [deleted]
			ON ([Attachment].[LinkDocumentClass] = [deleted].[Class] AND [Attachment].[LinkDocumentID] = [deleted].[ID])
	WHERE
		[Attachment].[AttachmentTypeID] = 2
	IF @@ERROR != 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END
		
    DELETE [Document] 
    FROM
        [Document] INNER JOIN [deleted]
            ON ([Document].[ID] = [deleted].[ID] AND [Document].[Class] = [deleted].[Class])
	IF @@ERROR != 0
	BEGIN
		ROLLBACK TRANSACTION
		RETURN
	END

    RETURN
END
'
		EXEC sp_executesql @Sql
		
		-- create flag update procedure
		
		SET @Name = 'adf' + @TableName + 'FlagUpdate'
		IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(@Name) and OBJECTPROPERTY(id, N'IsProcedure') = 1)
		BEGIN
			SET @Sql = 'DROP PROCEDURE [' + @Name + ']'
			EXEC sp_executesql @Sql
		END
		
		SET @Sql = 'CREATE PROCEDURE [' + @Name + '] @ID TIdentifier, @Flag TFlag
AS
	UPDATE [' + @TableName + '] SET [Flag]=@Flag WHERE [ID]=@ID AND [Flag] != @Flag
	RETURN @@error
'
		EXEC sp_executesql @Sql
		
		SET @Sql = 'GRANT EXECUTE ON [' + @Name + '] TO PUBLIC'
		
		EXEC sp_executesql @Sql
		
	END
	FETCH NEXT FROM classes INTO @Class, @TableName
END
CLOSE classes
DEALLOCATE classes
GO