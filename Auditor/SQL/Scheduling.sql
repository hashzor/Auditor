DECLARE @start_date DATE = DATEADD(DAY,1,GETDATE());
DECLARE @end_date DATE = DATEADD(DAY,90,@start_date);
DECLARE @current_yearweek INT = DATEPART(YEAR,GETDATE())*100 + DATEPART(WEEK,GETDATE());
DECLARE @dates TABLE
(
	[audit_date] DATE,
	[audit_year] INT,
	[audit_month] INT,
	[audit_week] INT
)
;WITH date_range([calc_date])
     AS (
		 SELECT
				@start_date
		 UNION ALL
		 SELECT
				DATEADD(DAY, 1, [calc_date])
		 FROM   [date_range]
		 WHERE  DATEADD(DAY, 1, [calc_date]) <= @end_date
	 )
     INSERT INTO @dates ([audit_date],[audit_year],[audit_month],[audit_week])
		 SELECT
				[calc_date],
				DATEPART(YEAR,[calc_date]),
				DATEPART(MONTH,[calc_date]),
				DATEPART(WEEK,[calc_date])
		 FROM   [date_range]
		 OPTION(MAXRECURSION 0)

--schedule_5s_admin
DECLARE @schedule_name NVARCHAR(20) = 'schedule_5s_admin';
DECLARE @audit_type NVARCHAR(20) = '5s_admin';

DELETE FROM [dbo].[schedules] WHERE [schedule_name] = @schedule_name AND [audit_year]*100+[audit_week]>@current_yearweek;
INSERT INTO [dbo].[schedules] ([schedule_name],[audit_type],[audit_target],[audit_year],[audit_week])
	SELECT @schedule_name AS [schedule_name],
		   [t].[audit_type],
		   [t].[audit_target],
		   [d].[audit_year],
		   [d].[audit_week]
	FROM
	(
		SELECT
			   DISTINCT [audit_year],[audit_week]
		FROM   @dates
		WHERE [audit_year]*100+[audit_week]>@current_yearweek
	) [d]
	CROSS JOIN
	(
		SELECT
			   [audit_type],
			   [audit_target]
		FROM   [dbo].[setts_audit_targets]
		WHERE [audit_type] = @audit_type
	) [t]

EXECUTE [dbo].[AssignAuditors] @schedule_name,@audit_type

--schedule_5s_lpa_prod
SET @schedule_name = 'schedule_5s_lpa_prod';
SET @audit_type = '5s_lpa_prod';

DELETE FROM [dbo].[schedules] WHERE [schedule_name] = @schedule_name AND [audit_year]*100+[audit_week]>@current_yearweek;
INSERT INTO [dbo].[schedules] ([schedule_name],[audit_type],[audit_target],[audit_year],[audit_week])
	SELECT @schedule_name AS [schedule_name],
		   [t].[audit_type],
		   [t].[audit_target],
		   [d].[audit_year],
		   [d].[audit_week]
	FROM
	(
		SELECT
			   DISTINCT [audit_year],[audit_week]
		FROM   @dates
		WHERE [audit_year]*100+[audit_week]>@current_yearweek
	) [d]
	CROSS JOIN
	(
		SELECT
			   [audit_type],
			   [audit_target]
		FROM   [dbo].[setts_audit_targets]
		WHERE [audit_type] = @audit_type
	) [t]

EXECUTE [dbo].[AssignAuditors] @schedule_name,@audit_type


--schedule_5s_lpa_sv
SET @schedule_name = 'schedule_5s_lpa_sv';
SET @audit_type = '5s_lpa_prod';

DELETE FROM [dbo].[schedules] WHERE [schedule_name] = @schedule_name AND [audit_year]*100+[audit_week]>@current_yearweek;
INSERT INTO [dbo].[schedules] ([schedule_name],[audit_type],[audit_year],[audit_week],[auditor_login])
	SELECT @schedule_name AS [schedule_name],
		   [t].[audit_type],
		   [d].[audit_year],
		   [d].[audit_week],
		   [t].[supervisor_login] AS [auditor_login]
	FROM
	(
		SELECT
			   DISTINCT [audit_year],[audit_week]
		FROM   @dates
		WHERE [audit_year]*100+[audit_week]>@current_yearweek
	) [d]
	CROSS JOIN
	(
		SELECT DISTINCT
			  [audit_type],
			  [supervisor_login]
		  FROM [dbo].[setts_audit_targets]
		WHERE [audit_type] = @audit_type
	) [t]

EXECUTE [dbo].[AssignTargets] @schedule_name,@audit_type





