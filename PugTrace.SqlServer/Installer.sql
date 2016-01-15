DECLARE @TARGET_SCHEMA_VERSION INT;
SET @TARGET_SCHEMA_VERSION = 2;

PRINT 'Installing PugTrace SQL objects...';

BEGIN TRANSACTION;

-- Create the database schema if it doesn't exists
IF NOT EXISTS (SELECT [schema_id] FROM [sys].[schemas] WHERE [name] = 'PugTrace')
BEGIN
    EXEC (N'CREATE SCHEMA [PugTrace]');
    PRINT 'Created database schema [PugTrace]';
END
ELSE
    PRINT 'Database schema [PugTrace] already exists';
    
DECLARE @SCHEMA_ID int;
SELECT @SCHEMA_ID = [schema_id] FROM [sys].[schemas] WHERE [name] = 'PugTrace';

-- Create the PugTrace.Schema table if not exists
IF NOT EXISTS(SELECT [object_id] FROM [sys].[tables] 
    WHERE [name] = 'Schema' AND [schema_id] = @SCHEMA_ID)
BEGIN
    CREATE TABLE [PugTrace].[Schema](
        [Version] [int] NOT NULL,
        CONSTRAINT [PK_PugTrace_Schema] PRIMARY KEY CLUSTERED ([Version] ASC)
    );
    PRINT 'Created table [PugTrace].[Schema]';
END
ELSE
    PRINT 'Table [PugTrace].[Schema] already exists';

DECLARE @CURRENT_SCHEMA_VERSION int;
SELECT @CURRENT_SCHEMA_VERSION = [Version] FROM [PugTrace].[Schema];

PRINT 'Current PugTrace schema version: ' + CASE @CURRENT_SCHEMA_VERSION WHEN NULL THEN 'none' ELSE CONVERT(nvarchar, @CURRENT_SCHEMA_VERSION) END;

IF @CURRENT_SCHEMA_VERSION IS NOT NULL AND @CURRENT_SCHEMA_VERSION > @TARGET_SCHEMA_VERSION
BEGIN
    ROLLBACK TRANSACTION;
    RAISERROR(N'PugTrace current database schema version %d is newer than the configured SqlServerStorage schema version %d. Please update to the latest PugTrace.SqlServer NuGet package.', 11, 1,
        @CURRENT_SCHEMA_VERSION, @TARGET_SCHEMA_VERSION);
END
ELSE
BEGIN
    -- Install PugTrace schema objects
    IF @CURRENT_SCHEMA_VERSION IS NULL
    BEGIN
        PRINT 'Installing schema version 1';

		CREATE TABLE [PugTrace].[Trace](
			ApplicationName nvarchar(256) NOT NULL,
			TraceId int NOT NULL PRIMARY KEY IDENTITY,
			[Source] nvarchar(64) NULL,
			Id int NOT NULL default 0,
			EventType nvarchar(32) NOT NULL,
			[UtcDateTime] datetime NOT NULL,
			MachineName nvarchar(32) NOT NULL,
			AppDomainFriendlyName nvarchar(512) NOT NULL,
			ProcessId int NOT NULL default 0,
			ThreadName nvarchar(512) NULL,
			[Message] nvarchar(1500) NULL,
			ActivityId uniqueidentifier NULL,
			RelatedActivityId uniqueidentifier NULL,
			LogicalOperationStack nvarchar(512) NULL,
			Data nvarchar(MAX) NULL,
		)

		SET @CURRENT_SCHEMA_VERSION = 1;
	END

	IF @CURRENT_SCHEMA_VERSION = 1
	BEGIN
		PRINT 'Installing schema version 2';

		ALTER TABLE [PugTrace].[Trace] ADD PrincipalIdentityName nvarchar(32) NULL;

		SET @CURRENT_SCHEMA_VERSION = 2;
	END

	UPDATE [PugTrace].[Schema] SET [Version] = @CURRENT_SCHEMA_VERSION
	IF @@ROWCOUNT = 0 
		INSERT INTO [PugTrace].[Schema] ([Version]) VALUES (@CURRENT_SCHEMA_VERSION) 

	PRINT CHAR(13) + 'PugTrace database schema installed';

    COMMIT TRANSACTION;
    PRINT 'PugTrace SQL objects installed';
END