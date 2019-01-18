--Schedule: Occurs every week on Monday,Wednesday,Friday at 10:00:00.

--STEP1 Send email
DECLARE @table NVARCHAR(MAX);
DECLARE @body NVARCHAR(MAX);
DECLARE @mycursor CURSOR;
DECLARE @mailquery NVARCHAR(MAX);
DECLARE @mailorder NVARCHAR(MAX);

DECLARE @schedule_id INT;
DECLARE @audit_type_name NVARCHAR(100);
DECLARE @audit_year INT;
DECLARE @audit_week INT;
DECLARE @audit_target NVARCHAR(20);
DECLARE @audit_target_area NVARCHAR(50);
DECLARE @audit_target_subarea NVARCHAR(50);
DECLARE @audit_target_supervisor NVARCHAR(100);
DECLARE @audit_target_supervisor_login NVARCHAR(50);
DECLARE @audit_target_supervisor_email NVARCHAR(80);
DECLARE @auditor_login NVARCHAR(50);
DECLARE @auditor_full_name NVARCHAR(101);
DECLARE @auditor_email NVARCHAR(80);
DECLARE @email_date DATETIME;


SET DATEFIRST 1;
DECLARE @dayNr INT;
SELECT @dayNr =  (((DATEPART([DW], GETDATE()) - 1) + @@DATEFIRST) % 7);

IF @dayNr = 1				 --MONDAY
BEGIN
		SET @mycursor = CURSOR
		FOR SELECT DISTINCT [var].[audit_target_supervisor_login],[var].[audit_target_supervisor], [u].[user_email] AS [audit_target_supervisor_email]
			FROM   [vAuditResults] [var]
			LEFT JOIN [vUsers] [u] ON [var].[audit_target_supervisor_login] = [u].[user_login]
			WHERE  [audit_type] IN ('5s_admin', '5s_lpa_prod')
					AND [audit_year] = DATEPART(YEAR, GETDATE())
					AND [audit_week] = DATEPART(WEEK, GETDATE())
					AND [u].[user_email] IS NOT NULL;
		OPEN @mycursor;
		FETCH NEXT FROM @mycursor INTO @audit_target_supervisor_login,@audit_target_supervisor,@audit_target_supervisor_email;
		WHILE @@fetch_status = 0
			BEGIN
				SET @mailquery = N'SELECT [audit_target] AS [PLACE], [auditor_full_name] AS [AUDITOR] 
									FROM [vAuditResults] 
									WHERE [audit_type] IN (''5s_admin'', ''5s_lpa_prod'') 
									AND [audit_year] = DATEPART(YEAR, GETDATE()) 
									AND [audit_week] = DATEPART(WEEK, GETDATE()) 
									AND [audit_target_supervisor_login] = ''' + @audit_target_supervisor_login +  '''';
				SET @mailorder = N'ORDER BY [OBSZAR]';
				EXEC [spQueryToHtmlTable] @html = @table OUTPUT,  @query = @mailquery, @orderBy = @mailorder;
				SET @body = 
'<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	</head>
	<body>
		<p>
			Hello <b>' + @audit_target_supervisor +'</b>!
		</p>
		<p>
			The following audits should take place in your administration area this week:
		</p>
		<p>
			' + @table +'
		</p>
		<p>
			The above message was generated automatically.
			<br/>Please do not reply to it!
		</p>
	</body>
</html>'
				EXEC msdb.dbo.sp_send_dbmail
					@profile_name = 'AppNotifierProfile',
					@recipients = @audit_target_supervisor_email,
					@subject = 'AUDIT 5S/LPA',
					@body = @body,
					@body_format = 'HTML',
					@query_no_truncate = 1,
					@attach_query_result_as_file = 0;

				FETCH NEXT FROM @mycursor INTO @audit_target_supervisor_login,@audit_target_supervisor,@audit_target_supervisor_email;
			END;
		CLOSE @mycursor;
		DEALLOCATE @mycursor;
END;

IF @dayNr = 1 OR @dayNr = 3  --MONDAY / WEDNESDAY
BEGIN
    SET @mycursor = CURSOR
    FOR SELECT
			   [schedule_id],
			   [audit_type_name],
			   [audit_year],
			   [audit_week],
			   [audit_target],
			   [audit_target_area],
			   [audit_target_subarea],
			   [audit_target_supervisor],
			   [auditor_login],
			   [auditor_full_name],
			   [auditor_email],
			   GETDATE()
		FROM   [vAuditResults]
		WHERE  [audit_type] IN ('5s_admin', '5s_lpa_prod')
			   AND [audit_year] = DATEPART(YEAR, GETDATE())
			   AND [audit_week] = DATEPART(WEEK, GETDATE())
			   AND [audit_id] IS NULL
    OPEN @mycursor;
    FETCH NEXT FROM @mycursor INTO @schedule_id, @audit_type_name, @audit_year, @audit_week, @audit_target, @audit_target_area, @audit_target_subarea, @audit_target_supervisor, @auditor_login, @auditor_full_name, @auditor_email, @email_date;
    WHILE @@fetch_status = 0
        BEGIN
            SET @body = 
'<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	</head>
	<body>
		<p>
			Hello <b>' + @auditor_full_name +'</b>!
		</p>
		<p>
			You should perform an audit in the Auditor application.
			<br/>
			Detailed information can be found below.
		</p>
		<p>
			<b>AUDIT:</b>
		</p>
		<table>
			<tr>
				<td style="width: 100px;">
					TYPE
				</td>
				<td>
					' + @audit_type_name + '
				</td>
			</tr>
			<tr>
				<td>
					WEEK
				</td>
				<td>
					' + CONVERT(NVARCHAR(20),@audit_year) + ' W' + CONVERT(NVARCHAR(20),@audit_week) + '
				</td>
			</tr>
			<tr>
				<td>
					PLACE
				</td>
				<td>
					' + @audit_target_area + ' | ' + @audit_target_subarea + ' | ' + @audit_target + '
				</td>
			</tr>
			<tr>
				<td>
					SUPERVISOR
				</td>
				<td>
					' + @audit_target_supervisor + '
				</td>
			</tr>
		</table>
		<p>
			<b>YOUR ACCOUNT:</b>
		</p>
		<table>
			<tr>
				<td style="width: 100px;">
					LOGIN
				</td>
				<td>
					' + @auditor_login + '
				</td>
			</tr>
			<tr>
				<td>
					EMAIL
				</td>
				<td>
					' + @auditor_email + '
				</td>
			</tr>
		</table>
		<p>
			The above message was generated automatically.
			<br/>Please do not reply to it!
		</p>
	</body>
</html>'
			
			EXEC msdb.dbo.sp_send_dbmail
				@profile_name = 'AppNotifierProfile',
				@recipients = @auditor_email,
				@subject = 'REMINDER AUDIT 5S/LPA',
				@body = @body,
				@body_format = 'HTML',
				@query_no_truncate = 1,
				@attach_query_result_as_file = 0;

            FETCH NEXT FROM @mycursor INTO @schedule_id, @audit_type_name, @audit_year, @audit_week, @audit_target, @audit_target_area, @audit_target_subarea, @audit_target_supervisor, @auditor_login, @auditor_full_name, @auditor_email, @email_date;
        END;
    CLOSE @mycursor;
    DEALLOCATE @mycursor;
END;

IF @dayNr = 5				 --FRIDAY
BEGIN
    DECLARE @liczba INT;
    SELECT @liczba=COUNT(*) 
	FROM   [vAuditResults]
	WHERE  [audit_type] IN ('5s_admin', '5s_lpa_prod')
			AND [audit_year] = DATEPART(YEAR, GETDATE())
			AND [audit_week] = DATEPART(WEEK, GETDATE())
			AND [audit_id] IS NULL;
	
    IF (@liczba>0)
    BEGIN
			SET @mailquery = N'SELECT [audit_target] AS [PLACE], [auditor_full_name] AS [AUDITOR] 
									FROM [vAuditResults] 
									WHERE [audit_type] IN (''5s_admin'', ''5s_lpa_prod'') 
									AND [audit_year] = DATEPART(YEAR, GETDATE()) 
									AND [audit_week] = DATEPART(WEEK, GETDATE()) 
									AND [audit_id] IS NULL';
			SET @mailorder = N'ORDER BY [OBSZAR]';
			EXEC [spQueryToHtmlTable] @html = @table OUTPUT,  @query = @mailquery, @orderBy = @mailorder;
			SET @body = 
'<html>
	<head>
		<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	</head>
	<body>
		<p>
			This week, the following audits were not carried out:
		</p>
		<p>
			' + @table +'
		</p>
		<p>
			The above message was generated automatically.
			<br/>Please do not reply to it!
		</p>
	</body>
</html>'
		  EXEC msdb.dbo.sp_send_dbmail
			 @profile_name = 'AppNotifierProfile',
	 		 @recipients = 'email1@test.com;email2@test.com;email3@test.com;', --TOP MANAGEMENT EMAIL LIST, TODO needs to be defined
			 @subject = 'AUDITS 5S/LPA NOT CARRIED OUT',
			 @body = @body,
			 @body_format = 'HTML',
			 @query_no_truncate = 1,
			 @attach_query_result_as_file = 0;
    END;
END;