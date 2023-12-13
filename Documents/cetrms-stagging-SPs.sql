use ph18157139172_cetrms
/****** Object:  StoredProcedure [cet_uerms].[sp_AddNewCity]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 28-02-2023
-- Description:	Stored Procedure to Add new City
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddNewCity] 
	-- Add the parameters for the stored procedure here
	@StateCode nchar(10), 
	@CityName nvarchar(200)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @ValidateStateCode int
	SELECT @ValidateStateCode = count(*) from CETStates where @StateCode = StateCode
	DECLARE @MaxCityCode int 
	DECLARE @NewCityCode int

	if @ValidateStateCode > 0
	begin
	select @MaxCityCode = MAX(CityCode) from CETCity where StateCode = @StateCode

	Select @NewCityCode = @MaxCityCode + 1

	insert into CETCity (CityCode, CityName, StateCode) 
		values (FORMAT(@NewCityCode, '00000'), @CityName, @StateCode)

	end
	else
		SET @NewCityCode = 0
	select @NewCityCode 'NewCityCode'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AddNewCountry]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 02-05-2023
-- Description:	Stored Procedure to add new country
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddNewCountry] 
	-- Add the parameters for the stored procedure here
	@CountryName nvarchar(128), 
	@StateName nvarchar(128),
	@CurrencyName nvarchar(3),
	@CurrencySymbol nvarchar(3)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IsDuplicateName int
	DECLARE @RecID int
	DECLARE @CountryCode nvarchar(10)
	DECLARE @StateCode nvarchar(10)

	SET @CountryCode = '1'

	select @IsDuplicateName = count(*) from CETStates where CountryName = @CountryName
	if @IsDuplicateName is null SET @IsDuplicateName = 0

    -- Insert statements for procedure here
	if @IsDuplicateName = 0
	begin
		SELECT @RecID = MAX(RecID) from CETStates

		SELECT @CountryCode = StateCode from CETStates where RecID = @RecID

		SET @CountryCode = SUBSTRING(@CountryCode, 1, 3)
			
		SET @CountryCode = CONVERT(nvarchar(10),(CONVERT(int, @CountryCode)+1))

		SET @CountryCode = RIGHT('000'+CAST(@CountryCode AS VARCHAR(3)),3)

		SET @StateCode = @CountryCode +'001'

		INSERT INTO [UEStates]
				   ([CountryName]
				   ,[StateName]
				   ,[StateCode]
				   ,[CurrencyName]
				   ,[CurrencySymbol])
			 VALUES
				   (@CountryName
				   ,@StateName
				   ,@StateCode
				   ,@CurrencyName
				   ,@CurrencySymbol)
	end

	Select @CountryCode 'Code'			--Durgesh

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AddNewStaffMember]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Dharmin
-- Create date: 06-04-2022
-- Description:	Stored procedure to add Accounting Team staff
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddNewStaffMember] 
	-- Add the parameters for the stored procedure here
	@Name nvarchar(100),
	@Address nvarchar(MAX),
	@MobileNo nchar(10),
	@Email nvarchar(max),
	@ProductCode nvarchar(50),
	@Designation nvarchar(50),
	@TeamId int,
	@StaffPhoto varbinary(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
        DECLARE @MemberId  nvarchar(13);
	DECLARE @CustSerialNo nvarchar(8);
	DECLARE @TeamCode nvarchar(4);

	SET @TeamCode = '0'+CONVERT(nvarchar(2), @TeamId)

	SELECT @CustSerialNo = count(*) from CETStaff where TeamID=@TeamId

	if @CustSerialNo is Null
		SET @CustSerialNo = 1
	else
		SET @CustSerialNo = @CustSerialNo + 1


	DECLARE @CustSerialNoLength int
	SET @CustSerialNoLength = 3 - LEN(@CustSerialNo)

	SET @CustSerialNo =
	CASE
		WHEN @CustSerialNoLength = 1 THEN '0'+@CustSerialNo
		WHEN @CustSerialNoLength = 2 THEN '00'+@CustSerialNo
	END


	SET @MemberId = CONCAT(
						RTRIM(LTRIM(SUBSTRING(CONVERT(nchar(4), YEAR(GETDATE())), 3,2))), 
						RTRIM(LTRIM(CONVERT(nchar(2),@TeamCode))),
						RTRIM(LTRIM(CONVERT(nchar(4),@CustSerialNo))))


	SELECT @MemberId

    -- Insert statements for procedure here
		INSERT INTO [UEStaff]
				   ([userid]
				   ,[Name]
				   ,[Address]
				   ,[MobileNo]
				   ,[Email]
				   ,[Designation]
				   ,[TeamID]
				   ,[UserStatus]
				   ,[StaffPhoto])
			 VALUES
				   (@MemberId
				   ,@Name
				   ,@Address
				   ,@MobileNo
				   ,@Email
				   ,@Designation
				   ,@TeamId
				   ,1
				   ,@StaffPhoto)

		INSERT INTO [MISUsers]
				   ([UserID]
				   ,[Password]
				   ,[UserType]
				   ,[UserRights]
				   ,[UserName]
				   ,[UserStatus])
			 VALUES
				   (@MemberId
				   ,@MemberId
				   ,@TeamId
				   ,1
				   ,@Name
				   ,1)
		
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AddNewState]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 03-05-2023
-- Description:	Stored Procedure to add new state
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddNewState] 
	-- Add the parameters for the stored procedure here
	@CountryName nvarchar(128), 
	@StateName nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @IsDuplicateName int
	DECLARE @RecID int
	DECLARE @CountryCode nvarchar(10)
	DECLARE @StateCode nvarchar(10)
	DECLARE @CurrencyName nvarchar(3)
	DECLARE @CurrencySymbol nvarchar(3)


	SET @CountryCode = '1'

	select @IsDuplicateName = count(*) from CETStates where CountryName = @CountryName and StateName = @StateName
	if @IsDuplicateName is null SET @IsDuplicateName = 0

    -- Insert statements for procedure here
	if @IsDuplicateName = 0
	begin
		SELECT @RecID = RecID from CETStates where CountryName = @CountryName
		SELECT @CountryCode = StateCode, @CurrencyName = CurrencyName, @CurrencySymbol = CurrencySymbol from CETStates where RecID = @RecID
		
		SET @StateCode = SUBSTRING(@CountryCode,4,3)
		SET @CountryCode = SUBSTRING(@CountryCode, 1, 3)
		
		SET @StateCode = CONVERT(nvarchar(10),(CONVERT(int, @StateCode)+1))

		SET @StateCode = RIGHT('000'+CAST(@StateCode AS VARCHAR(3)),3)

		SET @StateCode = @CountryCode +@StateCode

		INSERT INTO [UEStates]
				   ([CountryName]
				   ,[StateName]
				   ,[StateCode]
				   ,[CurrencyName]
				   ,[CurrencySymbol])
			 VALUES
				   (@CountryName
				   ,@StateName
				   ,@StateCode
				   ,@CurrencyName
				   ,@CurrencySymbol)
	end

	Select @StateCode 'Code'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AddPaymentLog]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 21-04-2023
-- Description:	Stored Procedure to Insert Payment Log
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddPaymentLog] 
	-- Add the parameters for the stored procedure here
	@PaymentRecID int,
	@LogDateTime datetime,
	@PaymentStatus int,
	@PaymentLog nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [UEPaymentLog]
				([PaymentRecID]
				,[LogDateTime]
				,[PaymentStatus]
				,[PaymentLog])
			VALUES
				(@PaymentRecID
				,@LogDateTime
				,@PaymentStatus
				,@PaymentLog)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AddTestimonial]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 25-04-2023
-- Description:	Stored Procedure to add testimonial
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddTestimonial] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@ResponseDate datetime,
	@Rating int,
	@ResponseMessage nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [UETestimonials]
			   ([CETClientID]
			   ,[ResponseDate]
			   ,[Rating]
			   ,[ResponseMessage]
			   ,[IsShown])
		 VALUES
			   (@CETClientID
			   ,@ResponseDate
			   ,@Rating
			   ,@ResponseMessage
			   ,0)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AddVacancy]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 20-02-2023
-- Description:	Stored Procedure to add vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AddVacancy] 
	-- Add the parameters for the stored procedure here
      @UEEmployerId int,
      @VacancyName nvarchar(200),
      @PrimaryLocation nvarchar(200),
      @JobType nchar(10),
      @EmployementStatus nvarchar(50),
      @CandidatesRequired int,
      @RequiredMinExp int,
      @RequiredMinQualification nvarchar(50),
      @VacancyDetails nvarchar(max),
      @SalaryOffered float,
	  @SalaryCycle int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @EmployerVerificaiton int
	DECLARE @EmployerName nvarchar(200)
	DECLARE @AdminNotificationMessage nvarchar(512)

	SELECT @EmployerVerificaiton = count(*), @EmployerName = BusinessName from CETEmployer where EmployerID = @UEEmployerId group by BusinessName

	if @EmployerVerificaiton is null SET @EmployerVerificaiton = -2

    -- Insert statements for procedure here
	if @EmployerVerificaiton <> -2
	begin
	INSERT INTO [CETVacancies]
			   ([CETEmployerId]
			   ,[VacancyStatusTypeId]
			   ,[VacancyName]
			   ,[VacancyCode]
			   ,[PrimaryLocation]
			   ,[JobType]
			   ,[EmployementStatus]
			   ,[CandidatesRequired]
			   ,[RequiredMinExp]
			   ,[RequiredMinQualification]
			   ,[PostingDate]
			   ,[VacancyDetails]
			   ,[SalaryOffered]
			   ,[SalaryCycle]
			   ,[FilledSeats])
		 VALUES
			   (@UEEmployerId,
			   1,
			   @VacancyName,
			   null, 
			   @PrimaryLocation, 
			   @JobType,
			   @EmployementStatus,
			   @CandidatesRequired,
			   @RequiredMinExp,
			   @RequiredMinQualification,
			   GETDATE(),
			   @VacancyDetails,
			   @SalaryOffered,
			   @SalaryCycle,
			   0)

		   SET @EmployerVerificaiton = SCOPE_IDENTITY()

			--SET @AdminNotificationMessage = 'New Vacancy added by : ' + @EmployerName
			--EXEC sp_InsertNewNotification 
			--		@NotificationType=3, 
			--		@NotificationMessage = @AdminNotificationMessage,
			--		@CETClientID = '-1'

		   Update CETEmployer SET EmployerStatus = 4  where EmployerID = @UEEmployerId
		   Update CETClient SET ClientStatusTypeID = 4  where ClientID = @UEEmployerId
	end
	SELECT @EmployerVerificaiton
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AuthenticateUEClient]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Prashant Sharma
-- Create date: 09-02-2023
-- Description:	Stored procedure to authenticate CETClient
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AuthenticateUEClient] 
	-- Add the parameters for the stored procedure here
	@UserId nvarchar(50), 
	@Password nvarchar(MAX),
	@ClientType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @UserType int
	SET @UserType = -1
	DECLARE @RecCount int
	SET @RecCount = 0

	SELECT @RecCount = count(*) From CETClient where AuthenticationID = @UserId and @ClientType = ClientTypeID

    -- Insert statements for procedure here
	if @RecCount = 1
	begin
		SELECT @RecCount = count(*) From CETClient where AuthenticationID = @UserId and @ClientType = ClientTypeID and Password = @Password  --UserID replace with [ClientID]  By Krishna Bharwad

		if @RecCount = 1
			SELECT @UserType = ClientTypeID From CETClient where AuthenticationID = @UserId and @ClientType = ClientTypeID and Password = @Password    --UserID replace with [ClientID] By Krishna Bharwad
		else
			SET @UserType = -2
	end

	SELECT @UserType

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_AuthenticateUser]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 18-02-2022
-- Description:	Stored procedure to authenticate user
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_AuthenticateUser] 
	-- Add the parameters for the stored procedure here
	@UserId nvarchar(50), 
	@Password nvarchar(MAX)  
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @UserType int
	SET @UserType = -1
	DECLARE @RecCount int
	SET @RecCount = 0

	SELECT @RecCount = count(*) From [MISUsers] where UserID = @UserId  

    -- Insert statements for procedure here
	if @RecCount = 1
	begin
		--SELECT @RecCount = count(*) From MISUsers where UserID = @UserId and Password = @Password  

		SELECT @RecCount=count(*) From [MISUsers] where UserID = @UserId 
		and Password = @Password  COLLATE Latin1_General_CS_AS

		if @RecCount = 1
			SET @UserType = 1
		else
			SET @UserType = -2
	end

	SELECT @UserType

	

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_CandidateMobileAppDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 10-05-2023
-- Description:	Stored Procedure For CandidateMobileAppDashboard
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_CandidateMobileAppDashboard] 
	-- Add the parameters for the stored procedure here
	@CandidateId int 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @TotalVacancies int
	Declare @TotalJobApplications int 
	Declare @TotalScheduledInterviews int
	Declare @AwaitedResults int

	Declare @JobApplicationId int

	Select @JobApplicationId = JobApplicationId from JobApplications where CandidateID = @CandidateId

	Select @TotalVacancies = Count(*) from CETVacancies where VacancyStatusTypeId in (1,5)
	if @TotalVacancies is null set  @TotalVacancies = 0

	Select @TotalJobApplications = Count(*) from JobApplications where CandidateID = @CandidateId
	if @TotalJobApplications is null set @TotalJobApplications = 0

	Select @TotalScheduledInterviews = Count(*) from CETInterviews where InterviewStatus = 2 and JobApplicationId = @JobApplicationId
	if @TotalScheduledInterviews is null set @TotalScheduledInterviews = 0

	Select @AwaitedResults = Count(*) from JobApplications where ApplicationStatus = 4 and CandidateID =  @CandidateId
	if @AwaitedResults is null set @AwaitedResults = 0

	Select @TotalVacancies 'TotalVacancies',
	       @TotalJobApplications 'TotalJobApplications',
	       @TotalScheduledInterviews 'TotalScheduledInterviews',
	       @AwaitedResults 'AwaitedResults'
 
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_CandidateSignUp]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 13-02-2023
-- Description:	Stored Procedure for Candidate signup.
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_CandidateSignUp] 
	-- Add the parameters for the stored procedure here
	@UEAuthenticationType int = 0, 
	@AuthenticationID nvarchar(50),
	@AuthenticationName nvarchar(512),
	@AuthenticationProfilePicURL nvarchar(512),
	@UEAdminID nvarchar(50),
	@Password nvarchar(MAX),
	@Email nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @SignUpStatus int
	DECLARE @RecordCount int
	DECLARE @ClientID int
	DECLARE @ReferralCode nvarchar(MAX)
	DECLARE @new_id VARCHAR(255)
	DECLARE @AdminNotificationMessage nvarchar(512)
	DECLARE @CandidateNotificationMessage nvarchar(512)

    -- Insert statements for procedure here

	SELECT @RecordCount = count(*) from CETClient where AuthenticationID = @AuthenticationID and ClientTypeID = 2

	if @RecordCount >= 1 
	begin
		SELECT @ClientID = ClientID, @SignUpStatus = ClientStatusTypeID from CETClient where AuthenticationID = @AuthenticationID  and ClientTypeID = 2
	end
	else
	begin


		SELECT @new_id = new_id FROM newid


		SELECT @ReferralCode = CAST((ABS(CHECKSUM(@new_id))%10) AS VARCHAR(1)) + 
		CHAR(ASCII('a')+(ABS(CHECKSUM(@new_id))%25)) +
		CHAR(ASCII('A')+(ABS(CHECKSUM(@new_id))%25)) +
		LEFT(@new_id,3)


		INSERT INTO [CETClient]
				   ([ClientTypeID]
				   ,[ClientStatusTypeID]
				   ,[CETAdminID]
				   ,[LatestReceiptId]
				   ,[Password]
				   ,[AuthenticationType]
				   ,[AuthenticationID]
				   ,[AuthenticationName]
				   ,[AuthenticationProfileURL]
				   ,[SignedUpOn])
			 VALUES
				   (2
				   ,1
				   ,@UEAdminID
				   ,0
				   ,@Password
				   ,@UEAuthenticationType
				   ,@AuthenticationID
				   ,@AuthenticationName
				   ,@AuthenticationProfilePicURL
				   ,GETDATE())	
			SET @ClientID = SCOPE_IDENTITY()

			INSERT INTO CETCandidate
					   ([CandidateId]
					   ,[Name]
					   ,[Status]
					   ,[RegistrationDate]
					   ,[ReferralCode]
					   ,[CandidateEmail])
				 VALUES
					   (@ClientID
					   ,@AuthenticationName
					   ,1
					   ,GETDATE()
					   ,@ReferralCode
					   ,@Email)

			--SET @AdminNotificationMessage = 'New Candidate Sign up. Name: ' + @AuthenticationName
			--EXEC sp_InsertNewNotification 
			--		@NotificationType=3, 
			--		@NotificationMessage = @AdminNotificationMessage,
			--		@CETClientID = '-1'

			--SET @CandidateNotificationMessage = 'Congratulations for on-boarding. Please complete your signup process.'
			--EXEC sp_InsertNewNotification 
			--		@NotificationType=2, 
			--		@NotificationMessage = @CandidateNotificationMessage,
			--		@CETClientID = @ClientID
			SET @SignUpStatus = 1
	end

	SELECT @ClientID 'ClientId', @SignUpStatus 'ClientStatus'
	
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_ChangePassword]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Krishna
-- Create date: 22/02/2023
-- Description:	Create sp_ChangePassword
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_ChangePassword]
	-- Add the parameters for the stored procedure here
	@UserID nvarchar(50),
	@OldPassword nvarchar(MAX),
	@NewPassword nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RetValue int;
	DECLARE @RecCount int;

	SET @RetValue = -1;

	SELECT @RecCount = count(*) FROM MISUsers where @UserID = UserID and @OldPassword = Password

	if @RecCount = 1 
	begin
		UPDATE MISUsers SET Password = @NewPassword where UserID = @UserID
		SET @RetValue = 1;
	end

    -- Insert statements for procedure here
	SELECT @RetValue
	
	
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_ChangeTestimonialStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-04-2023
-- Description:	Stored Procedure to Update Status of Testimonial
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_ChangeTestimonialStatus] 
	-- Add the parameters for the stored procedure here
	@TestimonialID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @PreviousStatus int
	DECLARE @TotalVisible int
	DECLARE @LastTestimonialId int

	SELECT @PreviousStatus = IsShown from CETTestimonials where RecID = @TestimonialID

    -- Insert statements for procedure here

	if @PreviousStatus = 0
	begin
		SELECT @TotalVisible = count(*) from CETTestimonials where IsShown = 1
		if @TotalVisible >= 5
		begin
			SELECT @LastTestimonialId = Min(RecID) from CETTestimonials where IsShown = 1 
			UPDATE CETTestimonials SET IsShown = 0 where RecID = @LastTestimonialId 
		end
		UPDATE CETTestimonials SET IsShown = 1 where RecID = @TestimonialID
	end
	else
	begin
		UPDATE CETTestimonials SET IsShown = 0 where RecID = @TestimonialID
	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_CreateNewZoomMeeting]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 24-02-2023
-- Description:	Stored Procedure to Create New Zoom Meeting
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_CreateNewZoomMeeting] 
	-- Add the parameters for the stored procedure here
	@InterviewID int = 0, 
	@UERemarks nvarchar(max),
	@PreferredDateTime datetime,		--Durgesh
	@ZVCRQ_duration nvarchar(max),
	@ZVCRQ_start_time nvarchar(max),
	@ZVCRQ_timezone nvarchar(max),
	@ZVCRQ_topic nvarchar(max),
	@ZVCRQ_CompleteRequest nvarchar(max),
	@ZVCRS_uuid nvarchar(max),
	@ZVCRS_id nvarchar(max),
	@ZVCRS_host_id nvarchar(max),
	@ZVCRS_host_email nvarchar(max),
	@ZVCRS_topic nvarchar(max),
	@ZVCRS_type nvarchar(max),
	@ZVCRS_status nvarchar(max),
	@ZVCRS_start_time nvarchar(max),
	@ZVCRS_duration nvarchar(max),
	@ZVCRS_timezone nvarchar(max),
	@ZVCRS_created_at nvarchar(max),
	@ZVCRS_start_url nvarchar(max),
	@ZVCRS_join_url nvarchar(max),
	@ZVCRS_password nvarchar(max),
	@ZVCRS_h323_password nvarchar(max),
	@ZVCRS_pstn_password nvarchar(max),
	@ZVCRS_encrypted_password nvarchar(max),
	@ZVCRS_pre_schedule nvarchar(max),
	@ZVCRS_CompleteRequest nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE [CETInterviews]
	   SET [InterviewStatus] = 2
		  ,[CETRemarks] = @UERemarks
		  ,[PreferredDateTime] = @PreferredDateTime		--Durgesh
		  ,[ZVCRQ_duration] = @ZVCRQ_duration
		  ,[ZVCRQ_password] = @ZVCRQ_start_time
		  ,[ZVCRQ_start_time] = @ZVCRQ_timezone
		  ,[ZVCRQ_timezone] = @ZVCRQ_timezone
		  ,[ZVCRQ_topic] = @ZVCRQ_topic
		  ,[ZVCRQ_CompleteRequest] = @ZVCRQ_CompleteRequest
		  ,[ZVCRS_uuid] = @ZVCRS_uuid
		  ,[ZVCRS_id] = @ZVCRS_id
		  ,[ZVCRS_host_id] = @ZVCRS_host_id
		  ,[ZVCRS_host_email] = @ZVCRS_host_email
		  ,[ZVCRS_topic] = @ZVCRS_topic
		  ,[ZVCRS_type] = @ZVCRS_type
		  ,[ZVCRS_status] = @ZVCRS_status
		  ,[ZVCRS_start_time] = @ZVCRS_start_time
		  ,[ZVCRS_duration] = @ZVCRS_duration
		  ,[ZVCRS_timezone] = @ZVCRS_timezone
		  ,[ZVCRS_created_at] = @ZVCRS_created_at
		  ,[ZVCRS_start_url] = @ZVCRS_start_url
		  ,[ZVCRS_join_url] = @ZVCRS_join_url
		  ,[ZVCRS_password] = @ZVCRS_password
		  ,[ZVCRS_h323_password] = @ZVCRS_h323_password
		  ,[ZVCRS_pstn_password] = @ZVCRS_pstn_password
		  ,[ZVCRS_encrypted_password] = @ZVCRS_encrypted_password
		  ,[ZVCRS_pre_schedule] = @ZVCRS_pre_schedule
		  ,[ZVCRS_CompleteRequest] = @ZVCRS_CompleteRequest
	 WHERE @InterviewID = InterviewID
	 Select PreferredDateTime from CETInterviews where InterviewID = @InterviewID;

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_EmployerSignUp]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 13-02-2023
-- Description:	Stored Procedure for employer signup.
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_EmployerSignUp] 
	-- Add the parameters for the stored procedure here
	@UEAuthenticationType int = 0, 
	@AuthenticationID nvarchar(50),
	@AuthenticationName nvarchar(512),
	@AuthenticationProfilePicURL nvarchar(512),
	@UEAdminID nvarchar(200),
	@Password nvarchar(MAX),
	@Email nvarchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @SignUpStatus int
	DECLARE @RecordCount int
	DECLARE @ClientID int
	DECLARE @AdminNotificationMessage nvarchar(512)
	DECLARE @EmployerNotificationMessage nvarchar(512)


    -- Insert statements for procedure here

	SELECT @RecordCount = count(*) from CETClient where AuthenticationID = @AuthenticationID and ClientTypeID = 1

	if @RecordCount >= 1 
	begin
		SELECT @ClientID = ClientID, @SignUpStatus = ClientStatusTypeID from CETClient where AuthenticationID = @AuthenticationID and ClientTypeID = 1
	end
	else
	begin
		INSERT INTO [CETClient]
				   ([ClientTypeID]
				   ,[ClientStatusTypeID]
				   ,[CETAdminID]
				   ,[LatestReceiptId]
				   ,[Password]
				   ,[AuthenticationType]
				   ,[AuthenticationID]
				   ,[AuthenticationName]
				   ,[AuthenticationProfileURL]
				   ,[SignedUpOn])
			 VALUES
				   (1
				   ,1
				   ,@UEAdminID
				   ,0
				   ,@Password
				   ,@UEAuthenticationType
				   ,@AuthenticationID
				   ,@AuthenticationName
				   ,@AuthenticationProfilePicURL
				   ,GETDATE())	
			SET @ClientID = SCOPE_IDENTITY()

			INSERT INTO [CETEmployer]
					   ([EmployerID]
					   ,[Name]
					   ,[EmployerStatus]
					   ,[RegisteredOn]
					   ,[Email])
				 VALUES
					   (@ClientID
					   ,@AuthenticationName
					   ,1
					   ,GETDATE()
					   ,@Email)

			--SET @AdminNotificationMessage = 'New Candidate Sign up. Name: ' + @AuthenticationName
			--EXEC sp_InsertNewNotification 
			--		@NotificationType=3, 
			--		@NotificationMessage = @AdminNotificationMessage,
			--		@CETClientID = '-1'

			--SET @EmployerNotificationMessage = 'Congratulations for on-boarding. Please complete your signup process.'
			--EXEC sp_InsertNewNotification 
			--		@NotificationType=2, 
			--		@NotificationMessage = @EmployerNotificationMessage,
			--		@CETClientID = @ClientID

			SET @SignUpStatus = 1
	end

	SELECT @ClientID 'ClientId', @SignUpStatus 'ClientStatus'
	
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GeCandidateListByLocation]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 09-02-2023
-- Description:	Stored Procedure to get list of candidate
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GeCandidateListByLocation] 
	-- Add the parameters for the stored procedure here
	@JobLocation nvarchar(200),
	@CandidateStatus int=0													--Edited By Krishna Bharwad
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if	@JobLocation='all'							--Krishna  
begin												--Krishna
SELECT [CandidateId]
      ,[Name]
      ,[GivenName]
      ,[LegalGuardianName]
      ,[Surname]
      ,[CandidateEmail]
      ,[RegistrationDate]
      ,[CandidateIntro]
      ,[DateOfbirth]
      ,[ContactNumberCountryCode]
      ,[ContactNumber]
      ,[Gender]
      ,[NoticePeriod]
      ,[Status]
      ,[ValidationOTP]
      ,[ReferralCode]
      ,[Photo]
      ,[PermanentAddress]
      ,[CurrentAddress]
      ,[MaritalStatus]
      ,[Nationality]
      ,[ResumeFileType]
      ,[ResumeFileName]
      ,[ResumeData]
      ,[LastUpdatedOn]
      ,[PassportFileType]
      ,[PassportFileName]
      ,[PassportData]
      ,[VisaTypeID]
      ,[VisaDetails]
      ,[VisaValidUpto]
      ,[VisaFileType]
      ,[VisaFileName]
      ,[VisaData]
      ,[MotherName]
      ,[SpouseName]
      ,[PassportIssueDate]
      ,[PassportExpiryDate]
      ,[PassportNumber]
      ,[PassportIssueLocation]
      ,[HighestQualification]
      ,[UniversityName]
      ,[UniversityLocation]
      ,[QualificationYear]
      ,[CandidateCast]
      ,[TotalExperienceMonths]
      ,[IsCandidateReferred]
      ,[PermanentStateCode]
      ,[PermanentCityCode]
      ,[CurrentStateCode]
      ,[CurrentCityCode]
	  ,case 
	  when @CandidateStatus=0 then 'all'
	  when @CandidateStatus=1 then 'New Registration'
	  when @CandidateStatus=2 then 'Candidate Completed Details'
	  when @CandidateStatus=3 then 'Candidate Under Selection Process'
	  when @CandidateStatus=5 then 'Candidate Final Selected'
	  when @CandidateStatus=6 then 'Candidate Rejected'
	  END 'CandidateStatus'
	 --UEStates.StateName,
	-- CETStates.CountryName
	,[VerifyEmail]
  FROM [CETCandidate]
  --inner join CETStates on CETStates.StateCode = CETCandidate.CurrentStateCode
	 where -- CurrentStateCode = IIF(@JobLocation = 'all', CurrentStateCode, @JobLocation) ANd
	 [CETCandidate].[Status]!= 1		 --Edited By Krishna Bharwad
	 

end											--Krishna
else			
begin										--Krishna
SELECT [CandidateId]
      ,[Name]
      ,[GivenName]
      ,[LegalGuardianName]
      ,[Surname]
      ,[CandidateEmail]
      ,[RegistrationDate]
      ,[CandidateIntro]
      ,[DateOfbirth]
      ,[ContactNumberCountryCode]
      ,[ContactNumber]
      ,[Gender]
      ,[NoticePeriod]
      ,[Status]
      ,[ValidationOTP]
      ,[ReferralCode]
      ,[Photo]
      ,[PermanentAddress]
      ,[CurrentAddress]
      ,[MaritalStatus]
      ,[Nationality]
      ,[ResumeFileType]
      ,[ResumeFileName]
      ,[ResumeData]
      ,[LastUpdatedOn]
      ,[PassportFileType]
      ,[PassportFileName]
      ,[PassportData]
      ,[VisaTypeID]
      ,[VisaDetails]
      ,[VisaValidUpto]
      ,[VisaFileType]
      ,[VisaFileName]
      ,[VisaData]
      ,[MotherName]
      ,[SpouseName]
      ,[PassportIssueDate]
      ,[PassportExpiryDate]
      ,[PassportNumber]
      ,[PassportIssueLocation]
      ,[HighestQualification]
      ,[UniversityName]
      ,[UniversityLocation]
      ,[QualificationYear]
      ,[CandidateCast]
      ,[TotalExperienceMonths]
      ,[IsCandidateReferred]
      ,[PermanentStateCode]
      ,[PermanentCityCode]
      ,[CurrentStateCode]
      ,[CurrentCityCode]
	  ,case 
	  when @CandidateStatus=0 then 'all'
	  when @CandidateStatus=1 then 'New Registration'
	  when @CandidateStatus=2 then 'Candidate Completed Details'
	  when @CandidateStatus=3 then 'Candidate Under Selection Process'
	  when @CandidateStatus=5 then 'Candidate Final Selected'
	  when @CandidateStatus=6 then 'Candidate Rejected'
	  END 'CandidateStatus',
	  CETStates.StateName,
	  CETStates.CountryName
  FROM [CETCandidate]
  inner join CETStates on CETStates.StateCode = CETCandidate.CurrentStateCode
	 where CurrentStateCode = IIF(@JobLocation = 'all', CurrentStateCode, @JobLocation) ANd
	 [CETCandidate].[Status] != 1		 --Edited By Krishna Bharwad
	  

end									--Krishna
	 
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GeCandidateListByLocation2]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 09-02-2023
-- Description:	Stored Procedure to get list of candidate
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GeCandidateListByLocation2] 
	-- Add the parameters for the stored procedure here
	@JobLocation nvarchar(200),
	@CandidateStatus int=0													--Edited By Krishna Bharwad
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if	@JobLocation='all'							--Krishna  
begin												--Krishna
SELECT [CandidateId]
      ,[Name]
      ,[GivenName]
      ,[LegalGuardianName]
      ,[Surname]
      ,[CandidateEmail]
      ,[RegistrationDate]
      ,[CandidateIntro]
      ,[DateOfbirth]
      ,[ContactNumberCountryCode]
      ,[ContactNumber]
      ,[Gender]
      ,[NoticePeriod]
      ,[Status]
      ,[ValidationOTP]
      ,[ReferralCode]
      ,[Photo]
      ,[PermanentAddress]
      ,[CurrentAddress]
      ,[MaritalStatus]
      ,[Nationality]
      ,[ResumeFileType]
      ,[ResumeFileName]
      ,[ResumeData]
      ,[LastUpdatedOn]
      ,[PassportFileType]
      ,[PassportFileName]
      ,[PassportData]
      ,[VisaTypeID]
      ,[VisaDetails]
      ,[VisaValidUpto]
      ,[VisaFileType]
      ,[VisaFileName]
      ,[VisaData]
      ,[MotherName]
      ,[SpouseName]
      ,[PassportIssueDate]
      ,[PassportExpiryDate]
      ,[PassportNumber]
      ,[PassportIssueLocation]
      ,[HighestQualification]
      ,[UniversityName]
      ,[UniversityLocation]
      ,[QualificationYear]
      ,[CandidateCast]
      ,[TotalExperienceMonths]
      ,[IsCandidateReferred]
      ,[PermanentStateCode]
      ,[PermanentCityCode]
      ,[CurrentStateCode]
      ,[CurrentCityCode]
	  ,case 
	  when @CandidateStatus=0 then 'all'
	  when @CandidateStatus=1 then 'New Registration'
	  when @CandidateStatus=2 then 'Candidate Completed Details'
	  when @CandidateStatus=3 then 'Candidate Under Selection Process'
	  when @CandidateStatus=5 then 'Candidate Final Selected'
	  when @CandidateStatus=6 then 'Candidate Rejected'
	  END 'CandidateStatus'
	 ,cet_uerms.fn_GetStateName(CurrentStateCode) 'StateName'
	 ,cet_uerms.fn_GetCountryName(CurrentStateCode) 'CountryName'
	,[VerifyEmail]
  FROM [CETCandidate]
  --inner join CETStates on CETStates.StateCode = CETCandidate.CurrentStateCode
	 where -- CurrentStateCode = IIF(@JobLocation = 'all', CurrentStateCode, @JobLocation) ANd
	[CETCandidate].[Status] = IIF(@CandidateStatus=0,[CETCandidate].[Status],@CandidateStatus)		 --Edited By Krishna Bharwad
	 

end											--Krishna
else			
begin										--Krishna
SELECT [CandidateId]
      ,[Name]
      ,[GivenName]
      ,[LegalGuardianName]
      ,[Surname]
      ,[CandidateEmail]
      ,[RegistrationDate]
      ,[CandidateIntro]
      ,[DateOfbirth]
      ,[ContactNumberCountryCode]
      ,[ContactNumber]
      ,[Gender]
      ,[NoticePeriod]
      ,[Status]
      ,[ValidationOTP]
      ,[ReferralCode]
      ,[Photo]
      ,[PermanentAddress]
      ,[CurrentAddress]
      ,[MaritalStatus]
      ,[Nationality]
      ,[ResumeFileType]
      ,[ResumeFileName]
      ,[ResumeData]
      ,[LastUpdatedOn]
      ,[PassportFileType]
      ,[PassportFileName]
      ,[PassportData]
      ,[VisaTypeID]
      ,[VisaDetails]
      ,[VisaValidUpto]
      ,[VisaFileType]
      ,[VisaFileName]
      ,[VisaData]
      ,[MotherName]
      ,[SpouseName]
      ,[PassportIssueDate]
      ,[PassportExpiryDate]
      ,[PassportNumber]
      ,[PassportIssueLocation]
      ,[HighestQualification]
      ,[UniversityName]
      ,[UniversityLocation]
      ,[QualificationYear]
      ,[CandidateCast]
      ,[TotalExperienceMonths]
      ,[IsCandidateReferred]
      ,[PermanentStateCode]
      ,[PermanentCityCode]
      ,[CurrentStateCode]
      ,[CurrentCityCode]
	  ,case 
	  when @CandidateStatus=0 then 'all'
	  when @CandidateStatus=1 then 'New Registration'
	  when @CandidateStatus=2 then 'Candidate Completed Details'
	  when @CandidateStatus=3 then 'Candidate Under Selection Process'
	  when @CandidateStatus=5 then 'Candidate Final Selected'
	  when @CandidateStatus=6 then 'Candidate Rejected'
	  END 'CandidateStatus'
	  ,cet_uerms.fn_GetStateName(CurrentStateCode) 'StateName'
	  ,cet_uerms.fn_GetCountryName(CurrentStateCode) 'CountryName'
  FROM [CETCandidate]
  inner join CETStates on CETStates.StateCode = CETCandidate.CurrentStateCode
	 where CurrentStateCode = IIF(@JobLocation = 'all', CurrentStateCode, @JobLocation) ANd
	 [CETCandidate].[Status] = IIF(@CandidateStatus=0,[CETCandidate].[Status],@CandidateStatus)		 --Edited By Krishna Bharwad
	  

end									--Krishna
	 
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateBriefProfile]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 15-03-2023
-- Description:	Stored Procedure to Get Candidate Brief Profile
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateBriefProfile] 
	-- Add the parameters for the stored procedure here
	@CandidateID nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @TotalYears int
	DECLARE @TotalMonths int
	DECLARE @CandidateTotalExp nvarchar(MAX)
	DECLARE @CandidateHighestQualificaiton nvarchar(MAX)

	SET @TotalYears = 0
	SET @TotalMonths = 0
	SET @CandidateTotalExp = 'Fresher'

    -- Candidate's Highest Qualification
	SELECT @CandidateHighestQualificaiton = (' and Completed ' + HighestQualification + ' from ' + UniversityName)
	FROM CETCandidate where CandidateId = @CandidateID

    -- Candidate's Current Position
	DECLARE @TotalExperienceMonths int
	SELECT @TotalExperienceMonths = TotalExperienceMonths From CETCandidate where CandidateId = @CandidateID

	if @TotalExperienceMonths is NULL SET @TotalExperienceMonths=0

	if @TotalExperienceMonths=0		
	begin
		SET @CandidateTotalExp = ' Fresher '
	end
	else
	begin
		SET @CandidateTotalExp = 'Having total '

		SET @TotalYears = CONVERT(int, @TotalExperienceMonths / 12)
		SET @TotalMonths = CONVERT(int, @TotalExperienceMonths % 12)

		if @TotalYears != 0
			SET @CandidateTotalExp = @CandidateTotalExp + CONVERT(nvarchar(2), @TotalYears) +' years'

		if @TotalMonths != 0
		begin
			if @TotalYears != 0
				SET @CandidateTotalExp = @CandidateTotalExp + ' and ' + CONVERT(nvarchar(2), @TotalMonths) + ' months'
			else
				SET @CandidateTotalExp = @CandidateTotalExp + CONVERT(nvarchar(2), @TotalMonths) + ' months'
		end

		SET @CandidateTotalExp = @CandidateTotalExp + ' of experience '
	end

	Select (@CandidateTotalExp + @CandidateHighestQualificaiton) 'Profile'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateContactDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Contact Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateContactDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from EmployerID
	else
	begin

	SELECT [CandidateId]
      ,[ContactNumberCountryCode]
      ,[ContactNumber]
      ,[CurrentStateCode]
	  ,[CurrentCityCode]
      ,[PermanentAddress]
      ,[PermanentStateCode]
	  ,[PermanentCityCode]
      ,[CurrentAddress]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID
		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 10-02-2023
-- Description:	Stored Procedure to get Candidate parameters for dashboard
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateDashboard] 
	-- Add the parameters for the stored procedure here
	@CandidateLocation nvarchar(512)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @NewRegistration int
	DECLARE @CandidateCompletedDetails int
	DECLARE @CandidateUnderSelectionProcess int
	DECLARE @CandidateFinalSelected int
	DECLARE @CandidateRejected int

	SELECT @NewRegistration = count(*) from CETCandidate where CETCandidate.Status = 1 
	if @NewRegistration is null SET @NewRegistration = 0

	SELECT @CandidateCompletedDetails = count(*) from CETCandidate where CETCandidate.Status != 1 
	if @CandidateCompletedDetails is null SET @CandidateCompletedDetails = 0

	SELECT @CandidateUnderSelectionProcess = count(*) from CETCandidate where CETCandidate.Status = 5 
	if @CandidateUnderSelectionProcess is null SET @CandidateUnderSelectionProcess = 0

	SELECT @CandidateFinalSelected = count(*) from CETCandidate where CETCandidate.Status = 8 
	if @CandidateFinalSelected is null SET @CandidateFinalSelected = 0

	SELECT @CandidateRejected = count(*) from CETCandidate where CETCandidate.Status = 9    --Before Status of @CandidateRejected=6 
	if @CandidateRejected is null SET @CandidateRejected = 0

	select
		@NewRegistration 'NewRegistration',
		@CandidateCompletedDetails 'CandidateCompletedDetails',
		@CandidateUnderSelectionProcess 'CandidateUnderSelectionProcess',
		@CandidateFinalSelected 'CandidateFinalSelected',
		@CandidateRejected 'CandidateRejected'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateFullDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateFullDetails] 
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from EmployerID
	else
	begin

	SELECT [CandidateId]
		  ,[Name]
		  ,[GivenName]
		  ,[LegalGuardianName]
		  ,[Surname]
		  ,[CandidateEmail]
		  ,[RegistrationDate]
		  ,[CandidateIntro]
		  ,[DateOfbirth]
		  ,[ContactNumberCountryCode]
		  ,[ContactNumber]
		  ,[Gender]
		  ,[CurrentStateCode]
		  ,[CurrentCityCode]
		  ,[NoticePeriod]
		  ,[Status]
		  ,[ValidationOTP]
		  ,[ReferralCode]
		  ,[Photo]
		  ,[PermanentAddress]
		  ,[PermanentStateCode]
		  ,[PermanentCityCode]
		  ,[CurrentAddress]
		  ,[MaritalStatus]
		  ,[Nationality]
		  ,[ResumeFileType]
		  ,[ResumeFileName]
		  ,[ResumeData]
		  ,[LastUpdatedOn]
		  ,[PassportFileType]
		  ,[PassportFileName]
		  ,[PassportData]
		  ,[VisaTypeID]
		  ,[VisaDetails]
		  ,[VisaValidUpto]
		  ,[VisaFileType]
		  ,[VisaFileName]
		  ,[VisaData]
		  ,[MotherName]
		  ,[SpouseName]
		  ,[PassportIssueDate]
		  ,[PassportExpiryDate]
		  ,[PassportNumber]
		  ,[PassportIssueLocation]
		  ,[HighestQualification]
		  ,[UniversityName]
		  ,[UniversityLocation]
		  ,[QualificationYear]
		  ,[CandidateCast]
		  ,[TotalExperienceMonths]
		  ,[IsCandidateReferred]
		  ,[BankAccountNumber]
		  ,[IFSCCode]
		  ,[VerifyEmail]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID
		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateFullDetailsForReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateFullDetailsForReport] 
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here

	SELECT [CandidateId]
		  ,[Name]
		  ,[GivenName]
		  ,[LegalGuardianName]
		  ,[Surname]
		  ,[CandidateEmail]
		  ,format([RegistrationDate],'dd/MM/yyyy hh:mm:ss') 'RegistrationDate'
		  ,[CandidateIntro]
		  ,Format([DateOfbirth], 'dd-MMM-yyyy') 'DateOfbirth'
		  ,[ContactNumberCountryCode]
		  ,[ContactNumber]
		  ,(case when [Gender] = 1 then 'Female' when [Gender] = 2 then 'Male' END) 'Gender'
		  ,CONCAT(cet_uerms.fn_GetStateName([CurrentStateCode]), ',', cet_uerms.fn_GetCountryName([CurrentStateCode])) 'CurrentStateCode'
		  ,cet_uerms.fn_GetCityName([CurrentCityCode],[CurrentStateCode]) 'CurrentCityCode'
		  ,[NoticePeriod]
		  ,[Status]
		  ,[ValidationOTP]
		  ,[ReferralCode]
		  ,[Photo]
		  ,[PermanentAddress]
		  ,cet_uerms.fn_GetStateName([PermanentStateCode]) 'PermanentStateCode'
		  ,cet_uerms.fn_GetCityName([PermanentCityCode],[PermanentStateCode]) 'PermanentCityCode'
		  ,[CurrentAddress]
		  ,[MaritalStatus]
		  ,[Nationality]
		  ,[ResumeFileType]
		  ,[ResumeFileName]
		  ,[ResumeData]
		  ,FORMAT([LastUpdatedOn],'dd/MM/yyyy hh:mm:ss') 'LastUpdatedOn'
		  ,[PassportFileType]
		  ,[PassportFileName]
		  ,[PassportData]
		  ,[VisaTypeID]
		  ,[VisaDetails]
		  ,FORMAT([VisaValidUpto], 'dd-MMM-yyyy') 'VisaValidUpto'
		  ,[VisaFileType]
		  ,[VisaFileName]
		  ,[VisaData]
		  ,[MotherName]
		  ,[SpouseName]
		  ,Format([PassportIssueDate], 'dd-MMM-yyyy') 'PassportIssueDate'
		  ,Format([PassportExpiryDate], 'dd-MMM-yyyy') 'PassportExpiryDate'
		  ,[PassportNumber]
		  ,CONCAT(cet_uerms.fn_GetStateName([PassportIssueLocation]),',',cet_uerms.fn_getCountryName([PassportIssueLocation])) 'PassportIssueLocation'
		  ,[HighestQualification]
		  ,[UniversityName]
		  ,CONCAT(cet_uerms.fn_GetStateName([UniversityLocation]),',',cet_uerms.fn_GetCountryName([UniversityLocation]))'UniversityLocation'
		  ,[QualificationYear]
		  ,[CandidateCast]
		  ,[IsCandidateReferred]
		  ,cet_uerms.fn_GetCandidateBriefProfile(@CandidateID) 'BriefPRofile'
		  ,[VerifyEmail]
		  ,cet_uerms.fn_GetExpInYears([TotalExperienceMonths]) 'TotalExperienceYears'
		  ,[TotalExperienceMonths]
	  FROM [CETCandidate]
		WHERE @CandidateID = CandidateID

	
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateOtherDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Other Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateOtherDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from EmployerID
	else
	begin

	SELECT [CandidateId]
		  ,[ResumeFileType]
		  ,[ResumeFileName]
		  ,[ResumeData]
	      ,[NoticePeriod]
		  ,[TotalExperienceMonths]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID
		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidatePassportDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Passport Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidatePassportDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from EmployerID
	else
	begin

	SELECT [CandidateId]
		  ,[GivenName]
		  ,[LegalGuardianName]
		  ,[Surname]
		  ,[PassportFileType]
		  ,[PassportFileName]
		  ,[PassportData]
		  ,[MotherName]
		  ,[SpouseName]
		  ,[PassportIssueDate]
		  ,[PassportExpiryDate]
		  ,[PassportNumber]
		  ,[PassportIssueLocation]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID
		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidatePersonalDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Personal Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidatePersonalDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateID = @CandidateID group by Status

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from CandidateID
	else
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateID

	SELECT [CandidateId]
		  ,[Name]
		  ,[CandidateEmail]
		  ,[RegistrationDate]
		  ,[CandidateIntro]
		  ,[DateOfbirth]
		  ,[Gender]
		  ,[Status]
		  ,[ValidationOTP]
		  ,[ReferralCode]
		  ,[Photo]
		  ,[MaritalStatus]
		  ,[Nationality]
		  ,[LastUpdatedOn]
		  ,[CandidateCast]
		  ,[TotalExperienceMonths]
		  ,[IsCandidateReferred]
		  ,[Photo]
		  ,[VerifyEmail]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID


	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateID
	if @CandidateStatus = 1
	begin
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateID

		-- Prashant : 16-03-2023 : Insert Due payment for first time profile update and send notification
		EXEC sp_InsertDuePayment @Currency='USD', @Amount=50, @PaymentType=2, @CETClientID=@CandidateID
	end

		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateQualificationDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Qualification Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateQualificationDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from EmployerID
	else
	begin

	SELECT [CandidateId]
		  ,[HighestQualification]
		  ,[UniversityName]
		  ,[UniversityLocation]
		  ,[QualificationYear]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID
		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCandidateVisaDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Get Candidate Visa Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCandidateVisaDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETCandidate where CandidateID = @CandidateID

    -- Insert statements for procedure here
	if @RecCount <1
		SELECT 0 'QueryStatus'-- Failed to get any record from EmployerID
	else
	begin

	SELECT [CandidateId]
		  ,[VisaTypeID]
		  ,[VisaDetails]
		  ,[VisaValidUpto]
		  ,[VisaFileType]
		  ,[VisaFileName]
		  ,[VisaData]
	  FROM [CETCandidate]
			WHERE @CandidateID = CandidateID
		SELECT 1 'QueryStatus' -- Sucessfully fetched data

	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetClientDetailsForInvoice]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 03-04-2023
-- Description:	Stored Procedure to Client Details for Invoice
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetClientDetailsForInvoice] 
	-- Add the parameters for the stored procedure here
	@CETClientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ClientName nvarchar(512)
	DECLARE @BusinessName nvarchar(512)
	DECLARE @ClientType int
	DECLARE @ClientAddress nvarchar(max)
	DECLARE @Phone nvarchar(20)
	DECLARE @Email nvarchar(512)

    -- Insert statements for procedure here
	SELECT @ClientType = ClientTypeID from CETClient where @CETClientID = ClientID

	if @ClientType = 1 --Employer
	begin
		SELECT @ClientName = Name,
				@BusinessName = BusinessName,
				@Phone = WhatsAppNumber,
				@Email = email
		From CETEmployer
		where EmployerID = @CETClientID
	end
	else -- Candidate
	begin
		SELECT @ClientName = Name,
				@BusinessName = '',
				@Phone = CONCAT('+',ContactNumberCountryCode,'-', ContactNumber),
				@Email = CandidateEmail
		From CETCandidate
		where CandidateID = @CETClientID
	end

	SELECT @ClientName 'ClientName', @BusinessName 'BusinessName', @ClientAddress 'ClientAddress', @Phone 'Phone', @Email 'Email'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCompanyDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 10-06-2022
-- Description:	Stored procedure to get company details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCompanyDetails] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [CompanyBillingName]
      ,[BillingAddress]
      ,[BillingDistrict]
      ,[BillingState]
      ,[BillingCountry]
      ,[GSTNumber]
	  ,[WhatsaAppMobileNo]
	  ,CompanyWebURL
	  ,SupportEmail 'SupportEmailAddress'
	  ,SupportContactNumber
	  ,NoReplyEmail
	  ,SupportEmail
	  ,NewsLetterEmail
	  ,AccountsEmail
	  ,InfoEmail
	  ,ReferralRegistrationLink
  FROM [UECompanyDetails]
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetCountryList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 28-02-2023
-- Description:	Stored Procedure to Get List of Countries
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetCountryList] 
	-- Add the parameters for the stored procedure here

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TempCountryTable Table(RecID int identity(1,1), CountryCode nchar(20), CountryName nvarchar(50), CurrencyName nvarchar(50), CurrencySymbol nvarchar(50))

	insert into @TempCountryTable
		select distinct Substring(StateCode, 0, 4), CountryName, CurrencyName, CurrencySymbol from CETStates	

	select CountryCode, CountryName, CurrencyName, CurrencySymbol from @TempCountryTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetEmployerDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 10-02-2023
-- Description:	Stored Procedure to get Employer parameters for dashboard
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetEmployerDashboard] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @EmployerOnBoarded int;
	DECLARE @ActiveEmployerWithOpenVacancy int
	DECLARE @InactiveEmployerWithFilledVacacny int
	DECLARE @EmployersWithNoVacancy int
	DECLARE @EmployerWithVacancy int
	Declare @EmployersInProcessVacancies int

	SELECT @EmployerOnBoarded = count(*) from CETEmployer where EmployerStatus != 1
	if @EmployerOnBoarded is null SET @EmployerOnBoarded = 0

	SELECT @ActiveEmployerWithOpenVacancy = count(*) from CETEmployer where EmployerStatus = 4 

	SELECT @InactiveEmployerWithFilledVacacny = count(*) from CETEmployer where EmployerStatus = 7 
	if @InactiveEmployerWithFilledVacacny is null SET @InactiveEmployerWithFilledVacacny = 0

	--SELECT @EmployerWithVacancy = count(*) from CETVacancies group by CETEmployerId
	--if @EmployerWithVacancy is null SET @EmployerWithVacancy = 0

	--SET @EmployersWithNoVacancy = @EmployerOnBoarded - @EmployerWithVacancy
	--if @EmployersWithNoVacancy is null SET @EmployersWithNoVacancy = 0

	
	SELECT @EmployersWithNoVacancy = count(*) from CETEmployer where EmployerStatus In(3)
	if @EmployersWithNoVacancy is null SET @EmployersWithNoVacancy = 0
	
	SELECT @EmployersInProcessVacancies = count(*) from CETEmployer where EmployerStatus = 5 
	if @EmployersInProcessVacancies is null SET @EmployersInProcessVacancies = 0

	select
		@EmployerOnBoarded 'EmployerOnBoarded',
		@ActiveEmployerWithOpenVacancy 'ActiveEmployersWithOpenVacancy',
		@InactiveEmployerWithFilledVacacny 'InactiveEmployerWithFilledVacacny',
		@EmployersWithNoVacancy 'EmployersWithNoVacancy',
		@EmployersInProcessVacancies  'EmployersInProcessVacancies'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetEmployerDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-02-2023
-- Description:	Stored Procedure to Get Employer Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetEmployerDetails] 
	-- Add the parameters for the stored procedure here
	@EmployerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETEmployer where EmployerID = @EmployerID

    -- Insert statements for procedure here
	

		SELECT [EmployerID]
			  ,[Name]
			  ,[BusinessName]
			  ,[Address]
			  ,[Email]
			  ,[WhatsAppNumber]				--BusinessLocation field removed by Durgesh
			  ,[BusinessLogo]
			  ,[EmployerStatus]				--EmployerStatus added by durgesh
			  ,[LocationStateCode]
			  ,[LocationCityCode]
			  ,[RegisteredOn]
			  ,[UpdateOn]
			  ,[VerifyEmail]
		  FROM [CETEmployer]
			WHERE @EmployerID = EmployerID
	
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetEmployerDetailsForReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Krishna Bharwad
-- Create date: 13-04-2023
-- Description:	Stored Procedure to Get Employer Details for report
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetEmployerDetailsForReport] 
	-- Add the parameters for the stored procedure here
	@EmployerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETEmployer where EmployerID = @EmployerID

    -- Insert statements for procedure here
	

		SELECT [EmployerID]
			  ,[Name]
			  ,[BusinessName]
			  ,[Address]
			  ,[Email]
			  ,[WhatsAppNumber]				--BusinessLocation field removed by Durgesh
			  ,[BusinessLogo]
			  ,[EmployerStatus]				--EmployerStatus added by durgesh
			  ,cet_uerms.fn_GetStateName([LocationStateCode]) 'LocationStateCode'
			 -- ,[LocationStateCode]
			 ,cet_uerms.fn_GetCityName([LocationCityCode],[LocationStateCode]) 'LocationCityCode'
			  --,[LocationCityCode]
			  ,FORMAT([RegisteredOn],'dd/MM/yyyy hh:mm:ss') 'RegisteredOn'
			  ,[UpdateOn]
			  ,[VerifyEmail]
		  FROM [CETEmployer]
			WHERE @EmployerID = EmployerID
	
END



GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetEmployerList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 03-03-2023
-- Description:	Stored Procedure to Get Employer List
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetEmployerList] 
	-- Add the parameters for the stored procedure here
	@EmployerStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	if @EmployerStatus = 4
	begin
		SELECT [EmployerID]
			  ,[Name]
			  ,[BusinessName]
			  ,[Address]
			  ,[Email]
			  ,[WhatsAppNumber]				
			  ,[BusinessLogo]
			  ,[LocationStateCode]
			  ,[LocationCityCode]
			  ,[RegisteredOn]
			  ,[UpdateOn]
			  ,[VerifyEmail]
		  FROM [CETEmployer]
			WHERE EmployerStatus in (4,5) -- If employer status is open then pick inprocess employers also
	end
	if @EmployerStatus = -1
	begin
			SELECT [EmployerID]
			  ,[Name]
			  ,[BusinessName]
			  ,[Address]
			  ,[Email]
			  ,[WhatsAppNumber]				
			  ,[BusinessLogo]
			  ,[LocationStateCode]
			  ,[LocationCityCode]
			  ,[RegisteredOn]
			  ,[UpdateOn]
			  ,[VerifyEmail]
		  FROM [CETEmployer]
			WHERE EmployerStatus != 1
	end
	else
	begin

		SELECT [EmployerID]
			  ,[Name]
			  ,[BusinessName]
			  ,[Address]
			  ,[Email]
			  ,[WhatsAppNumber]				
			  ,[BusinessLogo]
			  ,[LocationStateCode]
			  ,[LocationCityCode]
			  ,[RegisteredOn]
			  ,[UpdateOn]
			  ,[VerifyEmail]
		  FROM [CETEmployer]
			WHERE EmployerStatus = @EmployerStatus
	end
END


GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetEmployerListByLocation]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 03-03-2023
-- Description:	Stored Procedure to Get Employer List
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetEmployerListByLocation] 
	-- Add the parameters for the stored procedure here
	@BusinessLocation nvarchar(50),
	@EmployerStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	--SET @BusinessLocation=NULL

		if @EmployerStatus = -1

			SELECT [EmployerID]
				  ,[Name]
				  ,[BusinessName]
				  ,[Address]
				  ,[Email]
				  ,[WhatsAppNumber]				
				  ,[BusinessLogo]
				  ,[LocationStateCode]
				  ,[LocationCityCode]
				  ,[EmployerStatus]							--Edited by  Durgesh
				  ,[RegisteredOn]
				  ,[UpdateOn]
				  ,case 
				   when @EmployerStatus=-1 then 'Employer On Boarded'
				   when @EmployerStatus=4 then 'Active Employer With Open Vacancy'
				   when @EmployerStatus=7 then 'Inactive Employer With Filled Vacacny'
				   when @EmployerStatus=1 then 'Employers With No Vacancy'
				   when @EmployerStatus=5 then 'InProcess Vacancies'
				   when @EmployerStatus=2 then 'Registration Complete'
					END 'EmployerStatus',
					 cet_uerms.fn_GetStateName(LocationStateCode) 'StateName',				
					 cet_uerms.fn_GetCountryName(LocationStateCode) 'CountryName'
				  ,[VerifyEmail]
			   
			  FROM [CETEmployer]
				WHERE EmployerStatus != 1
					AND LocationStateCode = IIF(@BusinessLocation ='all', LocationStateCode, @BusinessLocation)
			else
				SELECT [EmployerID]
				  ,[Name]
				  ,[BusinessName]
				  ,[Address]
				  ,[Email]
				  ,[WhatsAppNumber]				
				  ,[BusinessLogo]
				  ,[LocationStateCode]
				  ,[LocationCityCode]
				  ,[EmployerStatus]							--Edited by  Durgesh
				  ,[RegisteredOn]
				  ,[UpdateOn]
				  ,case 
				   when @EmployerStatus=-1 then 'Employer On Boarded'
				   when @EmployerStatus=4 then 'Active Employer With Open Vacancy'
				   when @EmployerStatus=7 then 'Inactive Employer With Filled Vacacny'
				   when @EmployerStatus=1 then 'Employers With No Vacancy'
				   when @EmployerStatus=5 then 'InProcess Vacancies'
				   when @EmployerStatus=2 then 'Registration Complete'
					END 'EmployerStatus',
					 cet_uerms.fn_GetStateName(LocationStateCode) 'StateName',				
					 cet_uerms.fn_GetCountryName(LocationStateCode) 'CountryName'
				  ,[VerifyEmail]
			   
			  FROM [CETEmployer]
				WHERE EmployerStatus =  @EmployerStatus
				AND LocationStateCode = IIF(@BusinessLocation ='all', LocationStateCode, @BusinessLocation)

		--end
		--else
		--begin
		--SELECT [EmployerID]
		--	  ,[Name]
		--	  ,[BusinessName]
		--	  ,[Address]
		--	  ,[Email]
		--	  ,[WhatsAppNumber]				
		--	  ,[BusinessLogo]
		--	  ,[LocationStateCode]
		--	  ,[LocationCityCode]
		--	  ,[EmployerStatus]							--Edited by  Durgesh
		--	  ,[RegisteredOn]
		--	  ,[UpdateOn]
		--	  ,case 
		--	   when @EmployerStatus=-1 then 'Employer On Boarded'
		--	   when @EmployerStatus=4 then 'Active Employer With Open Vacancy'
		--	   when @EmployerStatus=7 then 'Inactive Employer With Filled Vacacny'
		--	   when @EmployerStatus=1 then 'Employers With No Vacancy'
	 --          when @EmployerStatus=5 then 'InProcess Vacancies'
	 --          when @EmployerStatus=2 then 'Registration Complete'
		--		END 'EmployerStatus',								
		--	 [fn_GetStateName]([LocationStateCode]),						--Added By Krishna 
		--	[fn_GetCountryName] ([LocationCityCode])						--Added By Krishna 
		--	  ,[VerifyEmail]
		--     FROM [CETEmployer]
		--	WHERE EmployerStatus = IIF(@EmployerStatus = -1,[CETEmployer].EmployerStatus, @EmployerStatus)
		--	AND LocationStateCode = IIF(@BusinessLocation ='all', LocationStateCode, @BusinessLocation)
		--end --k	
		--	if @EmployerStatus=11			--k
		--	begin
		--		SELECT [EmployerID]
		--	  ,[Name]
		--	  ,[BusinessName]
		--	  ,[Address]
		--	  ,[Email]
		--	  ,[WhatsAppNumber]				
		--	  ,[BusinessLogo]
		--	  ,[LocationStateCode]
		--	  ,[LocationCityCode]
		--	  ,[EmployerStatus]							--Edited by  Durgesh
		--	  ,[RegisteredOn]
		--	  ,[UpdateOn]
		--	  ,case 
		--	   when @EmployerStatus=-1 then 'Employer On Boarded'
		--	   when @EmployerStatus=4 then 'Active Employer With Open Vacancy'
		--	   when @EmployerStatus=7 then 'Inactive Employer With Filled Vacacny'
		--	   when @EmployerStatus=1 then 'Employers With No Vacancy'
	 --          when @EmployerStatus=5 then 'InProcess Vacancies'
	 --          when @EmployerStatus=2 then 'Registration Complete'
		--		END 'EmployerStatus',								
		--	[fn_GetStateName]([LocationStateCode]),						--Added By Krishna 
		--	 [fn_GetCountryName] ([LocationCityCode])						--Added By Krishna 
		--	  ,[VerifyEmail]
		--     FROM [CETEmployer]
		--	 where EmployerStatus IN(1,2,3) 
			
			
		--end		
END

GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetEmployerMobileAppDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 07-04-2023
-- Description:	Stored Procedure to Get Dashboard data for employer dashboard
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetEmployerMobileAppDashboard] 
	-- Add the parameters for the stored procedure here
	@EmployerID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TotalVacancies int
	DECLARE @OpenVacancies int
	DECLARE @ScheduledInterview int
	DECLARE @ApplicationReceived int
	DECLARE @ActionPending int
	DECLARE @TotalHired int

	SELECT @TotalVacancies = count(*) from CETVacancies where @EmployerID = CETEmployerId
	if @TotalVacancies is null SET @TotalVacancies = 0

	SELECT @OpenVacancies = count(*) from CETVacancies where VacancyStatusTypeId = 1 AND @EmployerID = CETEmployerId
	if @OpenVacancies is null SET @OpenVacancies = 0


	DECLARE @VacancyList Table (RecID int identity(1,1), VacancyId int)
	insert into @VacancyList
		select VacancyId from CETVacancies where CETEmployerId = IIF(@EmployerID = 0, CETEmployerID, @EmployerID)

	DECLARE @TempInterviewTable Table 
	([InterviewID] [int] NOT NULL,
	[JobApplicationID] [int] NULL,
	[PreferredDateTime] [datetime] NULL,
	[InterviewStatus] [int] NULL,
	[InterviewDuration] [int] NULL,
	[EmployerRemarks] [nvarchar](max) NULL,
	[CETRemarks] [nvarchar](max) NULL,
	[CandidateRemarks] [nvarchar](max) NULL,
	[ZVCRQ_duration] [nvarchar](max) NULL,
	[ZVCRQ_password] [nvarchar](max) NULL,
	[ZVCRQ_start_time] [nvarchar](max) NULL,
	[ZVCRQ_timezone] [nvarchar](max) NULL,
	[ZVCRQ_topic] [nvarchar](max) NULL,
	[ZVCRQ_CompleteRequest] [nvarchar](max) NULL,
	[ZVCRS_uuid] [nvarchar](max) NULL,
	[ZVCRS_id] [nvarchar](max) NULL,
	[ZVCRS_host_id] [nvarchar](max) NULL,
	[ZVCRS_host_email] [nvarchar](max) NULL,
	[ZVCRS_topic] [nvarchar](max) NULL,
	[ZVCRS_type] [nvarchar](max) NULL,
	[ZVCRS_status] [nvarchar](max) NULL,
	[ZVCRS_start_time] [nvarchar](max) NULL,
	[ZVCRS_duration] [nvarchar](max) NULL,
	[ZVCRS_timezone] [nvarchar](max) NULL,
	[ZVCRS_created_at] [nvarchar](max) NULL,
	[ZVCRS_start_url] [nvarchar](max) NULL,
	[ZVCRS_join_url] [nvarchar](max) NULL,
	[ZVCRS_password] [nvarchar](max) NULL,
	[ZVCRS_h323_password] [nvarchar](max) NULL,
	[ZVCRS_pstn_password] [nvarchar](max) NULL,
	[ZVCRS_encrypted_password] [nvarchar](max) NULL,
	[ZVCRS_pre_schedule] [nvarchar](max) NULL,
	[ZVCRS_CompleteRequest] [nvarchar](max) NULL,
	[ZVCMS_Status] [nvarchar](max) NULL,
	[ZVCMS_duration] [nvarchar](max) NULL,
	[ZVCMS_start_time] [nvarchar](max) NULL,
	[ZVCMS_end_time] [nvarchar](max) NULL,
	[ZVCMS_host_id] [nvarchar](max) NULL,
	[ZVCMS_dept] [nvarchar](max) NULL,
	[ZVCMS_participants_count] [nvarchar](max) NULL,
	[ZVCMS_source] [nvarchar](max) NULL,
	[ZVCMS_topic] [nvarchar](max) NULL,
	[ZVCMS_total_minutes] [nvarchar](max) NULL,
	[ZVCMS_type] [nvarchar](max) NULL,
	[ZVCMS_user_email] [nvarchar](max) NULL,
	[ZVCMS_user_name] [nvarchar](max) NULL,
	[ChosenTimeZone] [nvarchar](50) NULL,
	[DurationInMinutes] [int] NULL)

	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @VacancyList

	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @TempVacancyId int

	while @Counter <= @RecCount
	begin
		
		SELECT @TempVacancyId = VacancyId from @VacancyList where RecID = @Counter

		insert into @TempInterviewTable
			exec sp_GetInterviewListByVacancyId @VacancyId = @TempVacancyId, @InterviewStatus = -1

		SET @Counter = @Counter + 1
	end

		select @ScheduledInterview = count(*) from @TempInterviewTable where InterviewStatus != 1


	-------------------
	SET NOCOUNT ON;
	DECLARE @VacancyListTable Table (RecID int identity(1,1), VacancyID int)

	insert into @VacancyListTable 
		select VacancyID from CETVacancies where CETVacancies.CETEmployerId = @EmployerID

	DECLARE @TempJobApplicationTable Table ([JobApplicationID] [int] NOT NULL,
	[VacancyID] [int] NULL,
	[CandidateID] [int] NULL,
	[ApplicationStatus] [int] NULL,
	[CompanyRemarks] [nvarchar](max) NULL,
	[AppliedOn] [datetime] NULL,
	[RemarksOn] [datetime] NULL,
	[OfferLetterGeneratedOn] [datetime] NULL,
	[OfferLetterData] [varbinary](MAX) NULL)

	SELECT @RecCount = count(*) from @VacancyListTable

	SET @Counter = 1

	DECLARE @VacancyId int

	while @Counter <= @RecCount
	begin
		
		SELECT @VacancyId = VacancyID from @VacancyListTable where RecID = @Counter

		insert into @TempJobApplicationTable
			select * from JobApplications where @VacancyId = VacancyID AND ApplicationStatus Not IN (6,10)

		SET @Counter = @Counter + 1
	end

	select @ApplicationReceived=count(*) from @TempJobApplicationTable

	------------------
	SET @Counter = 1

	Delete @TempJobApplicationTable
	while @Counter <= @RecCount
	begin
		
		SELECT @VacancyId = VacancyID from @VacancyListTable where RecID = @Counter

		insert into @TempJobApplicationTable
			select * from JobApplications where @VacancyId = VacancyID and ApplicationStatus = 1

		SET @Counter = @Counter + 1
	end

	select @ActionPending=count(*) from @TempJobApplicationTable

	---------------
	------------------
	SET @Counter = 1

	Delete @TempJobApplicationTable
	while @Counter <= @RecCount
	begin
		
		SELECT @VacancyId = VacancyID from @VacancyListTable where RecID = @Counter

		insert into @TempJobApplicationTable
			select * from JobApplications where @VacancyId = VacancyID and (ApplicationStatus =  5 or ApplicationStatus =13  or ApplicationStatus = 15)

		SET @Counter = @Counter + 1
	end

	select @TotalHired=count(*) from @TempJobApplicationTable

	---------------
	select
		@TotalVacancies 'TotalVacancies',
		@OpenVacancies 'OpenVacancies',
		@ScheduledInterview 'ScheduleInterview',
		@ApplicationReceived 'ApplicationReceived',
		@ActionPending 'ActionPending',
		@TotalHired 'TotalHired'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetIndexPageParameters]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 06-06-2023
-- Description:	Stored Procedure to get Index page parameters
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetIndexPageParameters] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Temp int;
	DECLARE @CandidatesOnBoarded int;
	DECLARE @EmployersOnBoarded int
	DECLARE @Locations int
	DECLARE @VacanciesPublished int
	DECLARE @ScheduledInterviews int
	Declare @JobApplications int

	SET @Locations = 0

	SELECT @CandidatesOnBoarded = count(*) from CETCandidate where Status != 1 	
	if @CandidatesOnBoarded is null SET @CandidatesOnBoarded = 0

	SELECT @EmployersOnBoarded = count(*) from CETEmployer where EmployerStatus != 1 	
	if @EmployersOnBoarded is null SET @EmployersOnBoarded = 0

	SELECT @Temp = count(DISTINCT PrimaryLocation) from CETVacancies 
	SET @Locations = @Locations + @Temp

	SELECT @Temp = count(DISTINCT LocationCityCode) from CETEmployer
	SET @Locations = @Locations + @Temp

	SELECT @Temp = count(DISTINCT CurrentCityCode) from CETCandidate
	SET @Locations = @Locations + @Temp

	SELECT @VacanciesPublished = count(*) from CETVacancies

	SELECT @ScheduledInterviews = count(*) from CETInterviews where InterviewStatus != 1

	SELECT @JobApplications = count(*) from JobApplications

	select
		@CandidatesOnBoarded 'CandidatesOnBoarded',
		@EmployersOnBoarded 'EmployersOnBoarded',
		@Locations 'Locations',
		@VacanciesPublished 'VacanciesPublished',
		@ScheduledInterviews  'ScheduledInterviews',
		@JobApplications  'JobApplication'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 13-03-2023
-- Description:	Stored Procedure to get Interview parameters for dashboard
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewDashboard] 
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ProposedInterviews int
	DECLARE @ScheduledInterviews int
	DECLARE @CompletedInterviews int
	DECLARE @DroppedInterviews int
	DECLARE @CancelledInterviews int

	SELECT @ProposedInterviews = count(*) from CETInterviews where CETInterviews.InterviewStatus = 1 
	if @ProposedInterviews is null SET @ProposedInterviews = 0

	SELECT @ScheduledInterviews = count(*) from CETInterviews where CETInterviews.InterviewStatus = 2 
	if @ScheduledInterviews is null SET @ScheduledInterviews = 0

	SELECT @CompletedInterviews = count(*) from CETInterviews where CETInterviews.InterviewStatus = 4 
	if @CompletedInterviews is null SET @CompletedInterviews = 0

	SELECT @DroppedInterviews = count(*) from CETInterviews where CETInterviews.InterviewStatus = 5 
	if @DroppedInterviews is null SET @DroppedInterviews = 0

	SELECT @CancelledInterviews = count(*) from CETInterviews where CETInterviews.InterviewStatus = 6 
	if @CancelledInterviews is null SET @CancelledInterviews = 0


	select
		@ProposedInterviews 'ProposedInterviews',
		@ScheduledInterviews 'ScheduledInterviews',
		@CompletedInterviews 'CompletedInterviews',
		@DroppedInterviews 'DroppedInterviews',
		@CancelledInterviews 'CancelledInterviews'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewDetails] 
	-- Add the parameters for the stored procedure here
	@InterviewID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @RecCount int
	SELECT @RecCount = count(*) from CETInterviews where InterviewID = @InterviewID
	DECLARE @Status int


	if @RecCount >= 0
	begin

    -- Insert statements for procedure here
		SELECT [InterviewID]
			  ,[JobApplicationID]
			  ,[PreferredDateTime]
			  ,[InterviewStatus]
			  ,[InterviewDuration]
			  ,[EmployerRemarks]
			  ,[CETRemarks]
			  ,[CandidateRemarks]
			  ,[ZVCRQ_duration]
			  ,[ZVCRQ_password]
			  ,[ZVCRQ_start_time]
			  ,[ZVCRQ_timezone]
			  ,[ZVCRQ_topic]
			  ,[ZVCRQ_CompleteRequest]
			  ,[ZVCRS_uuid]
			  ,[ZVCRS_id]
			  ,[ZVCRS_host_id]
			  ,[ZVCRS_host_email]
			  ,[ZVCRS_topic]
			  ,[ZVCRS_type]
			  ,[ZVCRS_status]
			  ,[ZVCRS_start_time]
			  ,[ZVCRS_duration]
			  ,[ZVCRS_timezone]
			  ,[ZVCRS_created_at]
			  ,[ZVCRS_start_url]
			  ,[ZVCRS_join_url]
			  ,[ZVCRS_password]
			  ,[ZVCRS_h323_password]
			  ,[ZVCRS_pstn_password]
			  ,[ZVCRS_encrypted_password]
			  ,[ZVCRS_pre_schedule]
			  ,[ZVCRS_CompleteRequest]
			  ,[ZVCMS_Status]
			  ,[ZVCMS_duration]
			  ,[ZVCMS_start_time]
			  ,[ZVCMS_end_time]
			  ,[ZVCMS_host_id]
			  ,[ZVCMS_dept]
			  ,[ZVCMS_participants_count]
			  ,[ZVCMS_source]
			  ,[ZVCMS_topic]
			  ,[ZVCMS_total_minutes]
			  ,[ZVCMS_type]
			  ,[ZVCMS_user_email]
			  ,[ZVCMS_user_name]
			  ,[ChosenTimeZone]
			  ,[DurationInMinutes]
		  FROM [CETInterviews]
		  where InterviewID = @InterviewID

		  SET @Status = 1
	end
	else
		SET @Status = 0



END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewListByCandidateId]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview List By Candidate
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewListByCandidateId] 
	-- Add the parameters for the stored procedure here
	@CandidateID int,
	@InterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	DECLARE @JobApplicationList Table (RecID int identity(1,1), JobApplicationID int)

	insert into @JobApplicationList 
		select JobApplicationID from JobApplications where JobApplications.CandidateID = @CandidateID

	DECLARE @TempInterviewTable Table 
	([InterviewID] [int] NOT NULL,
	[JobApplicationID] [int] NULL,
	[PreferredDateTime] [datetime] NULL,
	[InterviewStatus] [int] NULL,
	[InterviewDuration] [int] NULL,
	[EmployerRemarks] [nvarchar](max) NULL,
	[CETRemarks] [nvarchar](max) NULL,
	[CandidateRemarks] [nvarchar](max) NULL,
	[ZVCRQ_duration] [nvarchar](max) NULL,
	[ZVCRQ_password] [nvarchar](max) NULL,
	[ZVCRQ_start_time] [nvarchar](max) NULL,
	[ZVCRQ_timezone] [nvarchar](max) NULL,
	[ZVCRQ_topic] [nvarchar](max) NULL,
	[ZVCRQ_CompleteRequest] [nvarchar](max) NULL,
	[ZVCRS_uuid] [nvarchar](max) NULL,
	[ZVCRS_id] [nvarchar](max) NULL,
	[ZVCRS_host_id] [nvarchar](max) NULL,
	[ZVCRS_host_email] [nvarchar](max) NULL,
	[ZVCRS_topic] [nvarchar](max) NULL,
	[ZVCRS_type] [nvarchar](max) NULL,
	[ZVCRS_status] [nvarchar](max) NULL,
	[ZVCRS_start_time] [nvarchar](max) NULL,
	[ZVCRS_duration] [nvarchar](max) NULL,
	[ZVCRS_timezone] [nvarchar](max) NULL,
	[ZVCRS_created_at] [nvarchar](max) NULL,
	[ZVCRS_start_url] [nvarchar](max) NULL,
	[ZVCRS_join_url] [nvarchar](max) NULL,
	[ZVCRS_password] [nvarchar](max) NULL,
	[ZVCRS_h323_password] [nvarchar](max) NULL,
	[ZVCRS_pstn_password] [nvarchar](max) NULL,
	[ZVCRS_encrypted_password] [nvarchar](max) NULL,
	[ZVCRS_pre_schedule] [nvarchar](max) NULL,
	[ZVCRS_CompleteRequest] [nvarchar](max) NULL,
	[ZVCMS_Status] [nvarchar](max) NULL,
	[ZVCMS_duration] [nvarchar](max) NULL,
	[ZVCMS_start_time] [nvarchar](max) NULL,
	[ZVCMS_end_time] [nvarchar](max) NULL,
	[ZVCMS_host_id] [nvarchar](max) NULL,
	[ZVCMS_dept] [nvarchar](max) NULL,
	[ZVCMS_participants_count] [nvarchar](max) NULL,
	[ZVCMS_source] [nvarchar](max) NULL,
	[ZVCMS_topic] [nvarchar](max) NULL,
	[ZVCMS_total_minutes] [nvarchar](max) NULL,
	[ZVCMS_type] [nvarchar](max) NULL,
	[ZVCMS_user_email] [nvarchar](max) NULL,
	[ZVCMS_user_name] [nvarchar](max) NULL,
	[ChosenTimeZone] [nvarchar](50) NULL,
	
	[DurationInMinutes] [int] NULL)

	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @JobApplicationList

	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @JobApplicationID int

	while @Counter <= @RecCount
	begin
		
		SELECT @JobApplicationID = JobApplicationID from @JobApplicationList where RecID = @Counter

		insert into @TempInterviewTable
			select * from CETInterviews where JobApplicationID = @JobApplicationID 
			and InterviewStatus = IIF(@InterviewStatus = -1, InterviewStatus, @InterviewStatus)

		SET @Counter = @Counter + 1
	end

	select * from @TempInterviewTable


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewListByDate]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview List By Vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewListByDate] 
	-- Add the parameters for the stored procedure here
	@InterviewDate Date,
	@InterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	select * from CETInterviews where ZVCRQ_start_time = @InterviewDate
	and InterviewStatus = IIF(@InterviewStatus = -1, InterviewStatus, @InterviewStatus)



END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewListByEmployerId]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview List By Employer
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewListByEmployerId] 
	-- Add the parameters for the stored procedure here
	@EmployerID int,
	@EInterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	DECLARE @VacancyList Table (RecID int identity(1,1), VacancyId int)
	insert into @VacancyList
		select VacancyId from CETVacancies where CETEmployerId = IIF(@EmployerID = -1, CETEmployerID, @EmployerID)

	DECLARE @TempInterviewTable Table 
	([InterviewID] [int] NOT NULL,
	[JobApplicationID] [int] NULL,
	[PreferredDateTime] [datetime] NULL,
	[InterviewStatus] [int] NULL,
	[InterviewDuration] [int] NULL,
	[EmployerRemarks] [nvarchar](max) NULL,
	[CETRemarks] [nvarchar](max) NULL,
	[CandidateRemarks] [nvarchar](max) NULL,
	[ZVCRQ_duration] [nvarchar](max) NULL,
	[ZVCRQ_password] [nvarchar](max) NULL,
	[ZVCRQ_start_time] [nvarchar](max) NULL,
	[ZVCRQ_timezone] [nvarchar](max) NULL,
	[ZVCRQ_topic] [nvarchar](max) NULL,
	[ZVCRQ_CompleteRequest] [nvarchar](max) NULL,
	[ZVCRS_uuid] [nvarchar](max) NULL,
	[ZVCRS_id] [nvarchar](max) NULL,
	[ZVCRS_host_id] [nvarchar](max) NULL,
	[ZVCRS_host_email] [nvarchar](max) NULL,
	[ZVCRS_topic] [nvarchar](max) NULL,
	[ZVCRS_type] [nvarchar](max) NULL,
	[ZVCRS_status] [nvarchar](max) NULL,
	[ZVCRS_start_time] [nvarchar](max) NULL,
	[ZVCRS_duration] [nvarchar](max) NULL,
	[ZVCRS_timezone] [nvarchar](max) NULL,
	[ZVCRS_created_at] [nvarchar](max) NULL,
	[ZVCRS_start_url] [nvarchar](max) NULL,
	[ZVCRS_join_url] [nvarchar](max) NULL,
	[ZVCRS_password] [nvarchar](max) NULL,
	[ZVCRS_h323_password] [nvarchar](max) NULL,
	[ZVCRS_pstn_password] [nvarchar](max) NULL,
	[ZVCRS_encrypted_password] [nvarchar](max) NULL,
	[ZVCRS_pre_schedule] [nvarchar](max) NULL,
	[ZVCRS_CompleteRequest] [nvarchar](max) NULL,
	[ZVCMS_Status] [nvarchar](max) NULL,
	[ZVCMS_duration] [nvarchar](max) NULL,
	[ZVCMS_start_time] [nvarchar](max) NULL,
	[ZVCMS_end_time] [nvarchar](max) NULL,
	[ZVCMS_host_id] [nvarchar](max) NULL,
	[ZVCMS_dept] [nvarchar](max) NULL,
	[ZVCMS_participants_count] [nvarchar](max) NULL,
	[ZVCMS_source] [nvarchar](max) NULL,
	[ZVCMS_topic] [nvarchar](max) NULL,
	[ZVCMS_total_minutes] [nvarchar](max) NULL,
	[ZVCMS_type] [nvarchar](max) NULL,
	[ZVCMS_user_email] [nvarchar](max) NULL,
	[ZVCMS_user_name] [nvarchar](max) NULL,
	[ChosenTimeZone] [nvarchar](50) NULL,
	[DurationInMinutes] [int] NULL)

	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @VacancyList

	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @TempVacancyId int

	while @Counter <= @RecCount
	begin
		
		SELECT @TempVacancyId = VacancyId from @VacancyList where RecID = @Counter

		insert into @TempInterviewTable
			exec sp_GetInterviewListByVacancyId @VacancyId = @TempVacancyId, @InterviewStatus = @EInterviewStatus

		SET @Counter = @Counter + 1
	end

	select * from @TempInterviewTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewListByStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 13-03-2023
-- Description:	Stored Procedure to Get Interview List By Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewListByStatus] 
	-- Add the parameters for the stored procedure here
	@InterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	select * from CETInterviews where InterviewStatus = IIF(@InterviewStatus = -1, InterviewStatus, @InterviewStatus)



END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewListByStatusForReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		KRISHNA
-- Create date: 13-03-2023
-- Description:	Stored Procedure to Get Interview List By Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewListByStatusForReport] 
	-- Add the parameters for the stored procedure here
	@InterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	select [InterviewID]
		  ,[ZVCMS_topic]
		  ,[PreferredDateTime]
		  ,[ZVCRS_status]
		  ,[JobApplicationID]
		  ,[ZVCRS_start_time]
	,case
	when [InterviewStatus] = 1 then 'Proposed Interview'
	when [InterviewStatus] = 2 then 'Scheduled Interview'
	when [InterviewStatus] = 3 then 'Interview Started'
	when [InterviewStatus] = 4 then 'Interview Completed'
	when [InterviewStatus] = 5 then 'Interview Dropped'
	when [InterviewStatus] = 6 then 'Interview Cancelled'
	when [InterviewStatus] = 7 then 'Interview Rejected'
	END 'InterviewStatus'
	from CETInterviews where InterviewStatus = IIF(@InterviewStatus = -1, InterviewStatus, @InterviewStatus)



END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetInterviewListByVacancyId]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview List By Vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetInterviewListByVacancyId] 
	-- Add the parameters for the stored procedure here
	@VacancyId int,
	@InterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	DECLARE @JobApplicationList Table (RecID int identity(1,1), JobApplicationID int)

	insert into @JobApplicationList 
		select JobApplicationID from JobApplications where JobApplications.VacancyID = iif(@VacancyId = -1, JobApplications.VacancyID, @VacancyId)

	DECLARE @TempInterviewTable Table 
	([InterviewID] [int] NOT NULL,
	[JobApplicationID] [int] NULL,
	[PreferredDateTime] [datetime] NULL,
	[InterviewStatus] [int] NULL,
	[InterviewDuration] [int] NULL,
	[EmployerRemarks] [nvarchar](max) NULL,
	[CETRemarks] [nvarchar](max) NULL,
	[CandidateRemarks] [nvarchar](max) NULL,
	[ZVCRQ_duration] [nvarchar](max) NULL,
	[ZVCRQ_password] [nvarchar](max) NULL,
	[ZVCRQ_start_time] [nvarchar](max) NULL,
	[ZVCRQ_timezone] [nvarchar](max) NULL,
	[ZVCRQ_topic] [nvarchar](max) NULL,
	[ZVCRQ_CompleteRequest] [nvarchar](max) NULL,
	[ZVCRS_uuid] [nvarchar](max) NULL,
	[ZVCRS_id] [nvarchar](max) NULL,
	[ZVCRS_host_id] [nvarchar](max) NULL,
	[ZVCRS_host_email] [nvarchar](max) NULL,
	[ZVCRS_topic] [nvarchar](max) NULL,
	[ZVCRS_type] [nvarchar](max) NULL,
	[ZVCRS_status] [nvarchar](max) NULL,
	[ZVCRS_start_time] [nvarchar](max) NULL,
	[ZVCRS_duration] [nvarchar](max) NULL,
	[ZVCRS_timezone] [nvarchar](max) NULL,
	[ZVCRS_created_at] [nvarchar](max) NULL,
	[ZVCRS_start_url] [nvarchar](max) NULL,
	[ZVCRS_join_url] [nvarchar](max) NULL,
	[ZVCRS_password] [nvarchar](max) NULL,
	[ZVCRS_h323_password] [nvarchar](max) NULL,
	[ZVCRS_pstn_password] [nvarchar](max) NULL,
	[ZVCRS_encrypted_password] [nvarchar](max) NULL,
	[ZVCRS_pre_schedule] [nvarchar](max) NULL,
	[ZVCRS_CompleteRequest] [nvarchar](max) NULL,
	[ZVCMS_Status] [nvarchar](max) NULL,
	[ZVCMS_duration] [nvarchar](max) NULL,
	[ZVCMS_start_time] [nvarchar](max) NULL,
	[ZVCMS_end_time] [nvarchar](max) NULL,
	[ZVCMS_host_id] [nvarchar](max) NULL,
	[ZVCMS_dept] [nvarchar](max) NULL,
	[ZVCMS_participants_count] [nvarchar](max) NULL,
	[ZVCMS_source] [nvarchar](max) NULL,
	[ZVCMS_topic] [nvarchar](max) NULL,
	[ZVCMS_total_minutes] [nvarchar](max) NULL,
	[ZVCMS_type] [nvarchar](max) NULL,
	[ZVCMS_user_email] [nvarchar](max) NULL,
	[ZVCMS_user_name] [nvarchar](max) NULL,
	[ChosenTimeZone] [nvarchar](50) NULL,
	[DurationInMinutes] [int] NULL)

	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @JobApplicationList

	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @JobApplicationID int

	while @Counter <= @RecCount
	begin
		
		SELECT @JobApplicationID = JobApplicationID from @JobApplicationList where RecID = @Counter

		insert into @TempInterviewTable
			select * from CETInterviews where JobApplicationID = @JobApplicationID
			and InterviewStatus = IIF(@InterviewStatus = -1, InterviewStatus, @InterviewStatus)


		SET @Counter = @Counter + 1
	end

	select * from @TempInterviewTable


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetJobApplicationDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 21-02-2023
-- Description:	Stored Procedure to Get Job Application Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetJobApplicationDetails] 
	-- Add the parameters for the stored procedure here
	@JobApplicationID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [JobApplicationID]
		  ,[VacancyID]
		  ,[CandidateID]
		  ,[ApplicationStatus]
		  ,[CompanyRemarks]
		  ,[AppliedOn]
		  ,[RemarksOn]
		  ,[OfferLetterGeneratedOn]
		  ,[OfferLetterData]
	  FROM [JobApplications]
	  where @JobApplicationID = JobApplicationID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedCandidateDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 15-04-2023
-- Description:	Stored Procedure to Get Candidate Masked Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedCandidateDetails] 
	-- Add the parameters for the stored procedure here
	@JobApplicationID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @VacancyID int
	DECLARE @VacancyName nvarchar(512)
	DECLARE @InterviewStatus int

	SELECT @VacancyID = VacancyId from JobApplications where JobApplicationID = @JobApplicationID
	SELECT @VacancyName = VacancyName from CETVacancies where VacancyID = @VacancyID
	Select @InterviewStatus = InterviewStatus from CETInterviews where JobApplicationID = @JobApplicationID	--Durgesh

    -- Insert statements for procedure here
			SELECT 
				@VacancyName 'VacancyName',
				CETCandidate.[CandidateId]
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'Location'
				,CETCandidate.[Photo]
				,CETCandidate.[Status]
				,cet_uerms.fn_GetCandidateBriefProfile(JobApplications.CandidateID) 'BriefProfile'
				,@InterviewStatus 'InterviewStatus'					--Durgesh
				,JobApplications.ApplicationStatus
			FROM JobApplications
			inner join CETCandidate on CETCandidate.CandidateId = JobApplications.CandidateID
			where JobApplicationID = @JobApplicationID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedCandidateListByEmployer]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 19-04-2023
-- Description:	Stored Procedure to Get Mask Candidate List by Employer
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedCandidateListByEmployer] 
	-- Add the parameters for the stored procedure here
	@EmployerID int = 0, 
	@CandidateStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	


	DECLARE @TempVacancyTable Table(RecID int identity(1,1), VacancyID int, VacancyName nvarchar(512))
	insert into @TempVacancyTable
		select VacancyID, VacancyName from CETVacancies where CETEmployerId = @EmployerID
	
	DECLARE @TempMaskedTable Table(
			VacancyName nvarchar(512),
			CandidateID int, 
			Age int, 
			Gender nvarchar(20), 
			CandidateLocation nvarchar(128), 
			Photo [varbinary](max) NULL,
			Status int,
			BriefProfile nvarchar(512),
			JobApplicationID int,
			JobApplicationStatus int)	--Durgesh


	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @TempVacancyTable

	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @TempVacancyId int

	while @Counter <= @RecCount
	begin
		
		SELECT @TempVacancyId = VacancyId from @TempVacancyTable where RecID = @Counter

		
		insert into @TempMaskedTable
			exec sp_GetMaskedCandidateListByVacancy @VacancyId = @TempVacancyId, @CandidateStatus = @CandidateStatus

		SET @Counter = @Counter + 1
	end

    -- Insert statements for procedure here

	select 
		VacancyName 'VacancyName',
		CandidateId,
		Age,
		Gender,
		CandidateLocation 'Location',
		Photo,
		Status,
		BriefProfile,
		JobApplicationID,
		JobApplicationStatus		--Durgesh
	from @TempMaskedTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedCandidateListByLocation]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 01-05-2023
-- Description:	Stored Procedure to Get Mask Candidate List by Location
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedCandidateListByLocation] 
	-- Add the parameters for the stored procedure here
	@LocationCode nvarchar(200),
	@CandidateStatus int = 0
AS
BEGIN
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if	@LocationCode='all'							--Krishna  
begin												--Krishna
			SELECT 
				CETCandidate.[CandidateId]
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'Location'
				,CETCandidate.[Photo]
				,CETCandidate.[Status]
				--,CETCandidate.[Name]								--Added By Krishna
				,cet_uerms.fn_GetCandidateBriefProfile(CandidateID) 'BriefProfile'
			FROM CETCandidate
			 where [CETCandidate].[Status]=IIF(@CandidateStatus=0,[CETCandidate].[Status],@CandidateStatus)		 --Edited By Krishna Bharwad

end											--Krishna
else			
begin										--Krishna
			SELECT 
				CETCandidate.[CandidateId]
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'Location'
				,CETCandidate.[Photo]
				,CETCandidate.[Status]
				--,CETCandidate.[Name]						--Added By Krishna
				,cet_uerms.fn_GetCandidateBriefProfile(CandidateID) 'BriefProfile'
			FROM CETCandidate
			 where CurrentStateCode = IIF(@LocationCode = 'all', CurrentStateCode, @LocationCode) AND
			 [CETCandidate].[Status]=IIF(@CandidateStatus=0,[CETCandidate].[Status],@CandidateStatus)		 --Edited By Krishna Bharwad

end									--Krishna

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedCandidateListByVacancy]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 15-04-2023
-- Description:	Stored Procedure to Get Mask Candidate List by Vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedCandidateListByVacancy] 
	-- Add the parameters for the stored procedure here
	@VacancyID int = 0, 
	@CandidateStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @VacancyName nvarchar(512)
	SELECT @VacancyName = VacancyName from CETVacancies where VacancyID = @VacancyID
	DECLARE @TempCandidateTable Table (CandidateID int, JobApplID int)

	insert @TempCandidateTable select CandidateID, JobApplicationID from JobApplications where VacancyID = @VacancyID

	DECLARE @TempMaskedTable Table(
			CandidateID int, 
			Age int, 
			Gender nvarchar(20), 
			CandidateLocation nvarchar(128), 
			Photo [varbinary](max) NULL,
			Status int,
			BriefProfile nvarchar(512),
			JobApplicationID int,
			JobApplicationStatus int)


	DECLARE @JobApplID VARCHAR(50) -- database name 

	DECLARE db_cursor CURSOR FOR 
	SELECT JobApplID 
	FROM @TempCandidateTable 

	OPEN db_cursor  
	FETCH NEXT FROM db_cursor INTO @JobApplID  

	WHILE @@FETCH_STATUS = 0  
	BEGIN 

		insert into @TempMaskedTable
			SELECT 
				CETCandidate.[CandidateId]
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'Location'
				,CETCandidate.[Photo]
				,CETCandidate.[Status]
				,cet_uerms.fn_GetCandidateBriefProfile(JobApplications.CandidateID) 'BriefProfile'
				,@JobApplID
				,JobApplications.ApplicationStatus
			FROM JobApplications
			inner join CETCandidate on CETCandidate.CandidateId = JobApplications.CandidateID
			where JobApplicationID = @JobApplID

		  FETCH NEXT FROM db_cursor INTO @JobApplID 
	END 

	CLOSE db_cursor  
	DEALLOCATE db_cursor 


    -- Insert statements for procedure here

	select 
		@VacancyName 'VacancyName',
		CandidateId,
		Age,
		Gender,
		CandidateLocation 'Location',
		Photo,
		Status,
		BriefProfile,
		JobApplicationID,
		JobApplicationStatus
	from @TempMaskedTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedInterviewDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 20-04-2023
-- Description:	Stored Procedure to get masked interview Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedInterviewDetails] 
	-- Add the parameters for the stored procedure here
	@InteviewID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
			select 
				CETVacancies.VacancyName 'VacancyName',
				CETCandidate.[CandidateId] 'CandidateID'
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'CandidateLocation'
				,CETCandidate.[Photo] 'Photo'
				,CETCandidate.[Status] 'Status'
				,cet_uerms.fn_GetCandidateBriefProfile(JobApplications.CandidateID) 'BriefProfile'
				,CETInterviews.JobApplicationID 'JobApplicationID'
				,CETInterviews.InterviewID 'InterviewID'
				,CETInterviews.ZVCRS_start_time 'InterviewScheduleDate'
				,CETInterviews.ZVCRS_timezone 'InteviewTimeZone'
				,CETInterviews.ZVCRS_id 'ZoomMeetingId'
				,CETInterviews.ZVCRS_start_url 'ZoomStartUrl'
				,CETInterviews.ZVCMS_Status 'ZoomMeetingStatus'
			from CETInterviews
			inner join JobApplications on JobApplications.JobApplicationID = CETInterviews.JobApplicationID
			inner join CETCandidate on CETCandidate.CandidateId = JobApplications.CandidateID
			inner join CETVacancies on CETVacancies.VacancyID = JobApplications.VacancyID
			where CETInterviews.InterviewID = @InteviewID and CETInterviews.ZVCRS_id is not null

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedInterviewListByCandidateId]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview List By Candidate
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedInterviewListByCandidateId] 
	-- Add the parameters for the stored procedure here
	@CandidateID int,
	@EInterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from

	DECLARE @JobApplicationList Table(RecID int identity(1,1), JAppID int)

	insert into @JobApplicationList
			select JobApplicationID from JobApplications where CandidateID = @CandidateID

	DECLARE @RecCount int
	DECLARE @Counter int

	DECLARE @TempMaskedTable Table(
			VacancyName nvarchar(512),
			CandidateID int, 
			Age int, 
			Gender nvarchar(20), 
			CandidateLocation nvarchar(128), 
			Photo [varbinary](max) NULL,
			Status int,
			BriefProfile nvarchar(512),
			JobApplicationID int,
			InterviewID int,
			InterviewScheduleDate DateTime,
			InteviewTimeZone nvarchar(128),
			ZoomMeetingId nvarchar(128),
			ZoomJoinUrl nvarchar(max),
			ZoomMeetingPassword nvarchar(max),
			ZoomMeetingStatus nvarchar(128))

	SELECT @RecCount = count(*) from @JobApplicationList

	SET @Counter = 1

	DECLARE @TempJobApplicationId int

	while @Counter <= @RecCount
	begin
		
		SELECT @TempJobApplicationId = JAppID from @JobApplicationList where RecID = @Counter
		
		insert into @TempMaskedTable
			select 
				CETVacancies.VacancyName,
				CETCandidate.[CandidateId]
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'Location'
				,CETCandidate.[Photo]
				,CETCandidate.[Status]
				,cet_uerms.fn_GetCandidateBriefProfile(JobApplications.CandidateID) 'BriefProfile'
				,@TempJobApplicationId
				,CETInterviews.InterviewID
				,CETInterviews.ZVCRS_start_time
				,CETInterviews.ZVCRS_timezone
				,CETInterviews.ZVCRS_id
				,CETInterviews.ZVCRS_join_url
				,CETInterviews.ZVCRS_encrypted_password
				,CETInterviews.ZVCMS_Status
			from CETInterviews
			inner join JobApplications on JobApplications.JobApplicationID = CETInterviews.JobApplicationID
			inner join CETCandidate on CETCandidate.CandidateId = JobApplications.CandidateID
			inner join CETVacancies on CETVacancies.VacancyID = JobApplications.VacancyID
			where CETInterviews.JobApplicationID = @TempJobApplicationId and CETInterviews.ZVCRS_id is not null

		SET @Counter = @Counter + 1
	end

	Select 
		VacancyName,
		CandidateID, 
		Age, 
		Gender, 
		CandidateLocation, 
		Photo,
		Status,
		BriefProfile,
		JobApplicationID,
		InterviewID,
		InterviewScheduleDate,
		InteviewTimeZone,
		ZoomMeetingId,
		ZoomJoinUrl,
		ZoomMeetingPassword,
		ZoomMeetingStatus
	FROM @TempMaskedTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetMaskedInterviewListByEmployerId]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-03-2023
-- Description:	Stored Procedure to Get Interview List By Employer
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetMaskedInterviewListByEmployerId] 
	-- Add the parameters for the stored procedure here
	@EmployerID int,
	@EInterviewStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	DECLARE @VacancyList Table (RecID int identity(1,1), VacancyId int)
	Declare @VacancyName nvarchar 
	insert into @VacancyList
		select VacancyId from CETVacancies where CETEmployerId = IIF(@EmployerID = 0, CETEmployerID, @EmployerID)

	DECLARE @JobApplicationList Table(RecID int identity(1,1), JAppID int)

	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @VacancyList
	
	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @TempVacancyId int

	while @Counter <= @RecCount
	begin
		
		SELECT @TempVacancyId = VacancyId from @VacancyList where RecID = @Counter

		insert into @JobApplicationList
			select JobApplicationID from JobApplications where VacancyID = @TempVacancyId

		SET @Counter = @Counter + 1
	end
	DECLARE @TempMaskedTable Table(
			VacancyName nvarchar(512),
			CandidateID int, 
			Age int, 
			Gender nvarchar(20), 
			CandidateLocation nvarchar(128), 
			Photo [varbinary](max) NULL,
			Status int,
			BriefProfile nvarchar(512),
			JobApplicationID int,
			InterviewID int,
			InterviewScheduleDate DateTime,
			InteviewTimeZone nvarchar(128),
			ZoomMeetingId nvarchar(128),
			ZoomStartUrl nvarchar(max),
			ZoomMeetingStatus nvarchar(128))

	SELECT @RecCount = count(*) from @JobApplicationList

	SET @Counter = 1

	DECLARE @TempJobApplicationId int

	while @Counter <= @RecCount
	begin
		
		SELECT @TempJobApplicationId = JAppID from @JobApplicationList where RecID = @Counter
		
		insert into @TempMaskedTable
			select 
				CETVacancies.VacancyName,
				CETCandidate.[CandidateId]
				,cet_uerms.fn_CalculateAge(CETCandidate.[DateOfbirth]) 'Age'
				,(case when CETCandidate.[Gender] = 1 then 'Female' when [Gender]=2 then 'Male' end) 'Gender'
				,CONCAT(cet_uerms.fn_GetCityName(CETCandidate.[CurrentCityCode],CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetStateName(CETCandidate.[CurrentStateCode]), ',', cet_uerms.fn_GetCountryName(CETCandidate.[CurrentStateCode])) 'Location'
				,CETCandidate.[Photo]
				,CETCandidate.[Status]
				,cet_uerms.fn_GetCandidateBriefProfile(JobApplications.CandidateID) 'BriefProfile'
				,@TempJobApplicationId
				,CETInterviews.InterviewID
				,CETInterviews.ZVCRS_start_time
				,CETInterviews.ZVCRS_timezone
				,CETInterviews.ZVCRS_id
				,CETInterviews.ZVCRS_start_url
				,CETInterviews.ZVCMS_Status
			from CETInterviews
			inner join JobApplications on JobApplications.JobApplicationID = CETInterviews.JobApplicationID
			inner join CETCandidate on CETCandidate.CandidateId = JobApplications.CandidateID
			inner join CETVacancies on CETVacancies.VacancyID = JobApplications.VacancyID
			where CETInterviews.JobApplicationID = @TempJobApplicationId and CETInterviews.ZVCRS_id is not null

		SET @Counter = @Counter + 1
	end
	Select @VacancyName = VacancyName from @TempMaskedTable
	Select 
		VacancyName,
		CandidateID, 
		Age, 
		Gender, 
		CandidateLocation, 
		Photo,
		Status,
		BriefProfile,
		JobApplicationID,
		InterviewID,
		InterviewScheduleDate,
		InteviewTimeZone,
		ZoomMeetingId,
		ZoomStartUrl,
		ZoomMeetingStatus
	FROM @TempMaskedTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetNotificationDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 17-03-2023
-- Description:	Stored Procedure to Get Notification Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetNotificationDetails] 
	-- Add the parameters for the stored procedure here
	@NotificationID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [NotificationID]
      ,[NotificationTime]
      ,[NotificationType]
      ,[NotificationMessage]
      ,[CETClientID]
      ,[NotificationStatus]
  FROM [CETNotification]
  where NotificationID = @NotificationID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetNotificationList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 17-03-2023
-- Description:	Stored Procedure to Get Notification List
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetNotificationList] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@NotificationType int,
	@NotificationStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT [NotificationID]
			  ,[NotificationTime]
			  ,[NotificationType]
			  ,[NotificationMessage]
			  ,[CETClientID]
			  ,[NotificationStatus] 
			  ,[HyperLink]
		  FROM [CETNotification]
		  
		  where CETClientID = @CETClientID
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
				AND NotificationStatus = IIF(@NotificationStatus = '-1', NotificationStatus, @NotificationStatus) 

			ORDER BY [NotificationStatus] ASC         --Add By Krishna
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetNotificationSummary]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 17-03-2023
-- Description:	Stored Procedure to Get Notification Summary
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetNotificationSummary] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@NotificationType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TotalNotification int
	DECLARE @NotificationDrafted int
	DECLARE @NotificationSent int
	DECLARE @NotificationUnread int
	DECLARE @NotificationRead int
	DECLARE @NotificationDropped int

    -- Insert statements for procedure here
		SELECT @TotalNotification = count(*)
		  FROM [CETNotification]
		  where CETClientID = IIF(@CETClientID = '-1', CETClientID, @CETClientID)
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
		IF @TotalNotification is null SET @TotalNotification = 0


		SELECT @NotificationDrafted = count(*)
		  FROM [CETNotification]
		  where CETClientID = IIF(@CETClientID = '-1', CETClientID, @CETClientID)
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
				AND NotificationStatus = 1
		IF @NotificationDrafted is null SET @NotificationDrafted = 0

		SELECT @NotificationSent = count(*)
		  FROM [CETNotification]
		  where CETClientID = IIF(@CETClientID = '-1', CETClientID, @CETClientID)
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
				AND NotificationStatus = 2
		IF @NotificationSent is null SET @NotificationSent = 0

		SELECT @NotificationUnread = count(*)
		  FROM [CETNotification]
		  where CETClientID = IIF(@CETClientID = '-1', CETClientID, @CETClientID)
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
				AND NotificationStatus = 3
		IF @NotificationUnread is null SET @NotificationUnread = 0

		SELECT @NotificationRead = count(*)
		  FROM [CETNotification]
		  where CETClientID = IIF(@CETClientID = '-1', CETClientID, @CETClientID)
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
				AND NotificationStatus = 4
		IF @NotificationRead is null SET @NotificationRead = 0

		SELECT @NotificationDropped = count(*)
		  FROM [CETNotification]
		  where CETClientID = IIF(@CETClientID = '-1', CETClientID, @CETClientID)
				AND NotificationType = IIF(@NotificationType = '-1', NotificationType, @NotificationType)
				AND NotificationStatus = 5
		IF @NotificationDropped is null SET @NotificationDropped = 0

		SELECT @TotalNotification 'TotalNotification',
			@NotificationDrafted 'NotificationDrafted', 
			@NotificationSent 'NotificationSent',
			@NotificationUnread 'NotificationUnread',
			@NotificationRead 'NotificationRead',
			@NotificationDropped 'NotificationDropped'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetOpenVacancyListByLocation]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




-- =============================================
-- Author:		Prashant Sharma
-- Create date: 26-06-2023
-- Description:	Stored Procedure to get list of open vacancies by Employer
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetOpenVacancyListByLocation] 
	-- Add the parameters for the stored procedure here
	@JobLocation nvarchar(200),
	@VacancyStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if @JobLocation='all'							--Added By Krishna
	begin
SELECT [VacancyID]
      ,[CETEmployerId]
      ,[CETVacancies].[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,CETVacancyStatusType.VacancyStatusTypeName
	  ,case 
	  when @VacancyStatus=0 then 'Total Vacancies'
	  when @VacancyStatus=1 then 'Open Vacancies'
	  when @VacancyStatus=5 then 'Vacancies Under Scheduled Interview'
	  when @VacancyStatus=4 then 'Close Vacancies After Final Selection'
	  END 'VacancyStatus',
	  cet_uerms.fn_GetStateName(PrimaryLocation) 'StateName',
	  cet_uerms.fn_GetCountryName(PrimaryLocation) 'CountryName',
	  CETStates.CurrencySymbol
	--  CETStates.StateName,
	 -- CETStates.CountryName
  FROM [CETRMS].[CETVacancies]
  --inner join CETStates on CETStates.StateCode = CETVacancies.PrimaryLocation
  inner join CETVacancyStatusType on CETVacancyStatusType.VacancyStatusTypeId = CETVacancies.VacancyStatusTypeId
  inner join CETStates on CETStates.StateCode = CETVacancies.PrimaryLocation
	  where [CETVacancies].VacancyStatusTypeId IN (1,5)
	  --AND PrimaryLocation = IIF(@JobLocation = 'all', PrimaryLocation, @JobLocation)

	  ORDER BY [VacancyID] DESC				--Add by Krishna

	  end
	  else										--Added By Krishna
	  begin

	  SELECT [VacancyID]
      ,[CETEmployerId]
      ,[CETVacancies].[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,CETVacancyStatusType.VacancyStatusTypeName
	  ,case 
	  when @VacancyStatus=0 then 'Total Vacancies'
	  when @VacancyStatus=1 then 'Open Vacancies'
	  when @VacancyStatus=5 then 'Vacancies Under Scheduled Interview'
	  when @VacancyStatus=4 then 'Close Vacancies After Final Selection'
	  END 'VacancyStatus',
	 CETStates.StateName,
	 CETStates.CountryName,
	 CETStates.CurrencySymbol
	 FROM [CETRMS].[CETVacancies]
	 inner join CETStates on CETStates.StateCode = CETVacancies.PrimaryLocation
	 inner join CETVacancyStatusType on CETVacancyStatusType.VacancyStatusTypeId = CETVacancies.VacancyStatusTypeId
	 where [CETVacancies].VacancyStatusTypeId IN (1,5)
	 AND PrimaryLocation = IIF(@JobLocation = 'all', PrimaryLocation, @JobLocation)

	 ORDER BY [VacancyID] DESC			--Add by Krishna
	  end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-03-2023
-- Description:	Stored Procedure to Get Payment Dashboard Data
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentDashboard] 
	-- Add the parameters for the stored procedure here
	@CETClientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TotalPaymentRequested float
	DECLARE @TotalPaymentReceived float
	DECLARE @TotalOutstandingPaymentWithinTimeLimit float
	DECLARE @TotalOutstandingPaymentOutOfTimeLimit float
	DECLARE @TotalPaymentDueFinalization float
	DECLARE @TotalPaymentNotificationGenerated int
	DECLARE @TotalPaymentTransactionsDone int
	DECLARE @TotalPaymentTransactionsDue int
	DECLARE @TotalReferralDetailsTransfer int			--Add by Krishna

    -- Insert statements for procedure here
	SELECT @TotalPaymentRequested = sum(Amount) from CETPayments
		WHERE CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalPaymentRequested is null SET @TotalPaymentRequested = 0

	SELECT @TotalPaymentReceived = sum(Amount) from CETPayments where PaymentStatus = 4
		AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalPaymentReceived is null SET @TotalPaymentReceived = 0

	SELECT @TotalOutstandingPaymentWithinTimeLimit = sum(Amount) from CETPayments where PaymentStatus = 1
		AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalOutstandingPaymentWithinTimeLimit is null SET @TotalOutstandingPaymentWithinTimeLimit = 0

	SELECT @TotalOutstandingPaymentOutOfTimeLimit = sum(Amount) from CETPayments where PaymentStatus = 2
		AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalOutstandingPaymentOutOfTimeLimit is null SET @TotalOutstandingPaymentOutOfTimeLimit = 0

	SELECT @TotalPaymentDueFinalization = sum(Amount) from CETPayments where PaymentStatus = 2
		AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalPaymentDueFinalization is null SET @TotalPaymentDueFinalization = 0

	SELECT @TotalPaymentNotificationGenerated = count(*) from CETPayments
		WHERE CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalPaymentNotificationGenerated is null SET @TotalPaymentNotificationGenerated = 0

	SELECT @TotalPaymentTransactionsDone = count(*) from CETPayments where PaymentStatus = 4
		AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalPaymentTransactionsDone is null SET @TotalPaymentTransactionsDone = 0

	SELECT @TotalPaymentTransactionsDue = count(*) from CETPayments where PaymentStatus in (1,2)
		AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
	if @TotalPaymentTransactionsDue is null SET @TotalPaymentTransactionsDue = 0


	SELECT @TotalReferralDetailsTransfer=count(*) from CETReferral where ReferralStatus=4		--Add by Krishna
	if 	@TotalReferralDetailsTransfer is null SET @TotalReferralDetailsTransfer=0				--Add by Krishna


	select @TotalPaymentRequested 'TotalPaymentRequested'
			,@TotalPaymentReceived 'TotalPaymentReceived'
			,@TotalOutstandingPaymentWithinTimeLimit 'TotalOutstandingPaymentWithinTimeLimit'
			,@TotalOutstandingPaymentOutOfTimeLimit 'TotalOutstandingPaymentOutOfTimeLimit'
			,@TotalPaymentDueFinalization 'TotalPaymentDueFinalization'
			,@TotalPaymentNotificationGenerated 'TotalPaymentNotificationGenerated'
			,@TotalPaymentTransactionsDone 'TotalPaymentTransactionsDone'
			,@TotalPaymentTransactionsDue 'TotalPaymentTransactionsDue'
			,@TotalReferralDetailsTransfer 'TotalReferralDetailsTransfer'		--Krishna
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentDashboardByPeriod]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-03-2023
-- Description:	Stored Procedure to Get Payment Dashboard Data vy Period
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentDashboardByPeriod] 
	-- Add the parameters for the stored procedure here
	@FromDate Date,
	@ToDate Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TotalPaymentRequested float
	DECLARE @TotalPaymentReceived float
	DECLARE @TotalOutstandingPaymentWithinTimeLimit float
	DECLARE @TotalOutstandingPaymentOutOfTimeLimit float
	DECLARE @TotalPaymentDueFinalization float
	DECLARE @TotalPaymentNotificationGenerated int
	DECLARE @TotalPaymentTransactionsDone int
	DECLARE @TotalPaymentTransactionsDue int

    -- Insert statements for procedure here
	SELECT @TotalPaymentRequested = sum(Amount) from CETPayments
		WHERE PaymentDate between @FromDate and @ToDate
	if @TotalPaymentRequested is null SET @TotalPaymentRequested = 0

	SELECT @TotalPaymentReceived = sum(Amount) from CETPayments where PaymentStatus = 3
		AND PaymentDate between @FromDate and @ToDate
	if @TotalPaymentReceived is null SET @TotalPaymentReceived = 0

	SELECT @TotalOutstandingPaymentWithinTimeLimit = sum(Amount) from CETPayments where PaymentStatus = 1
		AND PaymentDate between @FromDate and @ToDate
	if @TotalOutstandingPaymentWithinTimeLimit is null SET @TotalOutstandingPaymentWithinTimeLimit = 0

	SELECT @TotalOutstandingPaymentOutOfTimeLimit = sum(Amount) from CETPayments where PaymentStatus = 2
		AND PaymentDate between @FromDate and @ToDate
	if @TotalOutstandingPaymentOutOfTimeLimit is null SET @TotalOutstandingPaymentOutOfTimeLimit = 0

	SELECT @TotalPaymentDueFinalization = sum(Amount) from CETPayments where PaymentStatus = 2
		AND PaymentDate between @FromDate and @ToDate
	if @TotalPaymentDueFinalization is null SET @TotalPaymentDueFinalization = 0

	SELECT @TotalPaymentNotificationGenerated = count(*) from CETPayments
		WHERE PaymentDate between @FromDate and @ToDate
	if @TotalPaymentNotificationGenerated is null SET @TotalPaymentNotificationGenerated = 0

	SELECT @TotalPaymentTransactionsDone = count(*) from CETPayments where PaymentStatus = 3
		AND PaymentDate between @FromDate and @ToDate
	if @TotalPaymentTransactionsDone is null SET @TotalPaymentTransactionsDone = 0

	SELECT @TotalPaymentNotificationGenerated = count(*) from CETPayments where PaymentStatus in (1,2)
		AND PaymentDate between @FromDate and @ToDate
	if @TotalPaymentNotificationGenerated is null SET @TotalPaymentNotificationGenerated = 0


	select @TotalPaymentRequested 'TotalPaymentRequested'
			,@TotalPaymentReceived 'TotalPaymentReceived'
			,@TotalOutstandingPaymentWithinTimeLimit 'TotalOutstandingPaymentWithinTimeLimit'
			,@TotalOutstandingPaymentOutOfTimeLimit 'TotalOutstandingPaymentOutOfTimeLimit'
			,@TotalPaymentDueFinalization 'TotalPaymentDueFinalization'
			,@TotalPaymentNotificationGenerated 'TotalPaymentNotificationGenerated'
			,@TotalPaymentTransactionsDone 'TotalPaymentTransactionsDone'
			,@TotalPaymentNotificationGenerated 'TotalPaymentNotificationGenerated'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-03-2023
-- Description:	Stored Procedure to Get Payment Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentDetails] 
	-- Add the parameters for the stored procedure here
	@PaymentRecID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [PaymentRecID]
		  ,[PaymentOrderNo]
		  ,[Currency]
		  ,[Amount]
		  ,[TaxAmount]
		  ,[DueDate]
		  ,[PaymentType]
		  ,[TransactionType]
		  ,[CETClientID]
		  ,[PaymentStatus]
		  ,[NotificationID]
		  ,[NotificationType]
		  ,[PaymentDate]
		  ,[StripePaymentID]
		  ,[StripePaymentDate]
		  ,[StripePaymentStatus]
		  ,[StripePaymentMessage]
		  ,[StripePaymentReceiptURL]
		  ,[StripePaymentMethod]
		  ,InvoiceNo
		  ,Reserve1
		  ,Reserve2
	  FROM [CETPayments]
	  where PaymentRecID = @PaymentRecID
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-03-2023
-- Description:	Stored Procedure to Get Payment List
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentList] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@PaymentStatus int,
	@PaymentType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if @PaymentStatus != 8
	begin
		SELECT [PaymentRecID]
			  ,[PaymentOrderNo]
			  ,[Currency]
			  ,[Amount]
			  ,[TaxAmount]
			  ,[DueDate]
			  ,[PaymentType]
			  ,[TransactionType]
			  ,[CETClientID]
			  ,[PaymentStatus]
			  ,[NotificationID]
			  ,[NotificationType]
			  ,[PaymentDate]
			  ,[StripePaymentID]
			  ,[StripePaymentDate]
			  ,[StripePaymentStatus]
			  ,[StripePaymentMessage]
			  ,[StripePaymentReceiptURL]
			  ,[StripePaymentMethod]
			  ,InvoiceNo
			  ,Reserve1
			  ,Reserve2
		  FROM [CETPayments]
		  where CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
				AND PaymentStatus = IIF(@PaymentStatus = -1, PaymentStatus, @PaymentStatus)
				AND PaymentType = IIF(@PaymentType = -1, PaymentType, @PaymentType)
	end
	else
	begin
	SELECT [PaymentRecID]
		  ,[PaymentOrderNo]
		  ,[Currency]
		  ,[Amount]
		  ,[TaxAmount]
		  ,[DueDate]
		  ,[PaymentType]
		  ,[TransactionType]
		  ,[CETClientID]
		  ,[PaymentStatus]
		  ,[NotificationID]
		  ,[NotificationType]
		  ,[PaymentDate]
		  ,[StripePaymentID]
		  ,[StripePaymentDate]
		  ,[StripePaymentStatus]
		  ,[StripePaymentMessage]
		  ,[StripePaymentReceiptURL]
		  ,[StripePaymentMethod]
		  ,InvoiceNo
			  ,Reserve1
			  ,Reserve2
	  FROM [CETPayments]
	  where CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
			AND PaymentStatus in (1,2)
			AND PaymentType = IIF(@PaymentType = -1, PaymentType, @PaymentType)
	end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentListByPeriod]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-03-2023
-- Description:	Stored Procedure to Get Payment List by period
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentListByPeriod] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@PaymentStatus int,
	@PaymentType int,
	@FromDate Date,
	@ToDate Date
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [PaymentRecID]
		  ,[PaymentOrderNo]
		  ,[Currency]
		  ,[Amount]
		  ,[TaxAmount]
		  ,[DueDate]
		  ,[PaymentType]
		  ,[TransactionType]
		  ,[CETClientID]
		  ,[PaymentStatus]
		  ,[NotificationID]
		  ,[NotificationType]
		  ,[PaymentDate]
		  ,[StripePaymentID]
		  ,[StripePaymentDate]
		  ,[StripePaymentStatus]
		  ,[StripePaymentMessage]
		  ,[StripePaymentReceiptURL]
		  ,[StripePaymentMethod]
		  ,InvoiceNo
			,Reserve1
			,Reserve2
	  FROM [CETPayments]
	  where (PaymentDate BETWEEN @FromDate AND @ToDate)
			AND CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
			AND PaymentStatus = IIF(@PaymentStatus = -1, PaymentStatus, @PaymentStatus)
			AND PaymentType = IIF(@PaymentType = -1, PaymentType, @PaymentType)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentListForReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Krishna Bharwad
-- Create date: 16-03-2023
-- Description:	Stored Procedure to Get Payment List
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentListForReport] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@PaymentStatus int,
	@PaymentType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [PaymentRecID]
		  ,[PaymentOrderNo]
		  ,[Currency]
		  ,[Amount]
		  ,[TaxAmount]
		  ,[DueDate]
		  ,[PaymentType]
		  ,[TransactionType]
		  ,[CETClientID]
		  ,[NotificationID]
		  ,[NotificationType]
		  ,[PaymentDate]
		  ,case 
	  when [PaymentStatus]=-1 then 'All'
	  when [PaymentStatus]=1 then 'Payment Due Within Time'
	  when [PaymentStatus]=2 then 'Payment Due Out Of Time'
	  when [PaymentStatus]=3 then 'Payment Done'
	  when [PaymentStatus]=4 then 'Payment Cancelled'
	  when [PaymentStatus]=5 then 'Payment Discounted'
	  END 'PaymentStatus'
			  ,Reserve1
			  ,Reserve2
	  FROM [CETPayments]
	  where CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)
			AND PaymentStatus = IIF(@PaymentStatus = -1, [CETPayments].PaymentStatus, @PaymentStatus)
			AND PaymentType = IIF(@PaymentType = -1, PaymentType, @PaymentType)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentLog]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 11-05-2023
-- Description: Stored Procedure for GetPaymentLog
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentLog]
	-- Add the parameters for the stored procedure here
	 @CETClientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Declare @PaymentRecID int
	Select @PaymentRecID = PaymentRecID from CETPayments where 
	CETClientID = IIF(@CETClientID = -1, CETClientID, @CETClientID)

	Select   LogId, PaymentRecID
	    	,LogDateTime
			,PaymentStatus
			,PaymentLog = null
			from CETPaymentLog where PaymentRecID = @PaymentRecID
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentLogByRecID]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 11-05-2023
-- Description: Stored Procedure for GetPaymentLog
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentLogByRecID]
	-- Add the parameters for the stored procedure here
	 @PaymentRecID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select   LogId, PaymentRecID
	    	,LogDateTime
			,PaymentStatus
			,PaymentLog
			from CETPaymentLog where PaymentRecID = @PaymentRecID
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetPaymentReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 12-05-2023
-- Description:	Stored Procedure for GetPaymentReport
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetPaymentReport] 
	-- Add the parameters for the stored procedure here
	@CETClientID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select PaymentRecID
		  ,PaymentOrderNo
		  ,Amount
		  ,TaxAmount
		  ,Currency
		  ,PaymentStatus
		  from CETPayments where CETClientID = IIF(CETClientID = -1, CETClientId, @CETClientID)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetReferralDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 07-05-2023
-- Description:	Stored Procedure GetReferralDetails  by ReferralID
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetReferralDetails]
	-- Add the parameters for the stored procedure here
	@ReferralID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Select ReferralID
		  ,CETCandidateID
		  ,ReferralStatus
		  ,ReferralCode
		  from CETReferral where ReferralID = iif(ReferralID = -1, ReferralID, @ReferralID);
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetReferralDetailsByStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetReferralDetailsByStatus]
	-- Add the parameters for the stored procedure here
	@ReferralStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from CETReferral where ReferralStatus = IIF(@ReferralStatus = -1, ReferralStatus, @ReferralStatus)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetReferralPaidListForReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetReferralPaidListForReport]
	-- Add the parameters for the stored procedure here
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [ReferralID]
		  ,[ReferralCode] 
		  ,[UECandidateID]
		  ,case 
		  when [ReferralStatus] = -1  then 'All'
		  when [ReferralStatus] = 1 then 'New Candidate Signup'
		  when [ReferralStatus]	= 2 then 'Profile Completed'
		  when [ReferralStatus] = 3 then 'Registration Fees Paid'
		  when [ReferralStatus] = 4 then 'Referral Payment Successful'
	  End  as  'ReferralStatus'
	from [UEReferral] where ReferralStatus = 4
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetSignUpDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetSignUpDetails] 
	-- Add the parameters for the stored procedure here
	@ClientTypeID int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	--if @ClientTypeID=2							--Candidate 2
	--begin
	--SELECT  [ClientID],
	--		[AuthenticationName],
	--		[AuthenticationProfileURL],
	--		[SignedUpOn],
	--		[ClientTypeID],
	--		[AuthenticationID]

	--		FROM [CETClient]

	--		where 
	--		[ClientTypeID]=@ClientTypeID

	--		end

	--		else 
	--		begin

	--		SELECT  [ClientID],
	--		[AuthenticationName],
	--		[AuthenticationProfileURL],
	--		[SignedUpOn],
	--		[ClientTypeID],
	--		[AuthenticationID]

	--		FROM [CETClient]
	--		where 

	--		[ClientTypeID]=@ClientTypeID

	--		end

			

		SELECT  [ClientID],
			[AuthenticationName],
			[AuthenticationProfileURL],
			[SignedUpOn],
			[ClientTypeID],
			[AuthenticationID]

			FROM [CETClient]

			where 
			[ClientTypeID]=@ClientTypeID	


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetStaffDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 08-02-2023
-- Description:	Stored Procedure to get Staff Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetStaffDetails] 
	-- Add the parameters for the stored procedure here
	@userid nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
		SELECT  [userid]
			  ,[Name]
			  ,[Address]
			  ,[MobileNo]
			  ,[Email]
			  ,[Designation]
			  ,MISUserType.MISUserTypeName 'Team'
			  ,[TeamID] 
			  ,UserStatus
			  ,StaffPhoto
		FROM CETStaff
		inner join MISUserType on MISUserType.MISUserTypeId = CETStaff.TeamID 
		where userid = @userid
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetStateList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 28-02-2023
-- Description:	Stored Procedure to Get List of States
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetStateList] 
	-- Add the parameters for the stored procedure here
	@CountryCode nchar(3)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if @CountryCode!='all'
	select StateCode, 
		StateName, 
		Substring(StateCode,0,4) 'Countrycode', 
		CountryName,
		CurrencyName,
		CurrencySymbol
	from CETStates 
	where StateCode like @CountryCode+'%'
	else
	select StateCode, 
		StateName, 
		Substring(StateCode,0,4) 'Countrycode', 
		CountryName, 
		CurrencyName,
		CurrencySymbol
	from CETStates 


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetStateListByVacancyStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 28-02-2023
-- Description:	Stored Procedure to Get List of States by Vacancy Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetStateListByVacancyStatus] 
	-- Add the parameters for the stored procedure here
	@VacancyStatus int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT
		distinct PrimaryLocation,
		cet_uerms.fn_GetStateName(PrimaryLocation) 'StateName',
		cet_uerms.fn_GetCountryName(PrimaryLocation) 'CountryName'
	FROM CETVacancies
	WHERE VacancyStatusTypeId = IIF(@VacancyStatus = 0, VacancyStatusTypeId, @VacancyStatus) and PrimaryLocation is not null and PrimaryLocation != '' and PrimaryLocation != 'null'
	


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetTestimonialList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 25-04-2023
-- Description:	Stored Procedure to Get Shortlisted Testimonials
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetTestimonialList] 
	-- Add the parameters for the stored procedure here
	@IsShown int,
	@Rating int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [RecID]
      ,[CETClientID]
      ,[ResponseDate]
      ,[Rating]
      ,[ResponseMessage]
      ,[IsShown]
  FROM [UETestimonials]
  where IsShown = IIF(@IsShown = -1, IsShown, @IsShown)
		AND Rating = IIF(@Rating = -1, Rating, @Rating)
  order by ResponseDate desc
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVacancyDashboard]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 10-02-2023
-- Description:	Stored Procedure to get vacancy parameters for dashboard
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVacancyDashboard] 
	-- Add the parameters for the stored procedure here
	@VacancyLocation nvarchar(512)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TotalVacancies int
	DECLARE @OpenVacancies int
	DECLARE @VacanciesUnderScheduledInterview int
	DECLARE @CloseVacanciesAfterFinalSelection int

	SELECT @TotalVacancies = count(*) from CETVacancies where PrimaryLocation = IIF(@VacancyLocation = 'all', PrimaryLocation, @VacancyLocation)
	if @TotalVacancies is null SET @TotalVacancies = 0

	SELECT @OpenVacancies = count(*) from CETVacancies where VacancyStatusTypeId = 1 AND PrimaryLocation = IIF(@VacancyLocation = 'all', PrimaryLocation, @VacancyLocation)
	if @OpenVacancies is null SET @OpenVacancies = 0

	SELECT @VacanciesUnderScheduledInterview = count(*) from CETVacancies where VacancyStatusTypeId = 5 AND PrimaryLocation = IIF(@VacancyLocation = 'all', PrimaryLocation, @VacancyLocation)
	if @VacanciesUnderScheduledInterview is null SET @VacanciesUnderScheduledInterview = 0

	SELECT @CloseVacanciesAfterFinalSelection = count(*) from CETVacancies where VacancyStatusTypeId = 2 AND PrimaryLocation = IIF(@VacancyLocation = 'all', PrimaryLocation, @VacancyLocation)
	if @CloseVacanciesAfterFinalSelection is null SET @CloseVacanciesAfterFinalSelection = 0


	select
		@TotalVacancies 'TotalVacancies',
		@OpenVacancies 'OpenVacancies',
		@VacanciesUnderScheduledInterview 'VacanciesUnderScheduledInterview',
		@CloseVacanciesAfterFinalSelection 'CloseVacanciesAfterFinalSelection'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVacancyDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 21-02-2023
-- Description:	Stored Procedure to get details of vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVacancyDetails] 
	-- Add the parameters for the stored procedure here
	@VacancyID int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [VacancyID]
      ,[CETEmployerId]
      ,[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,[SalaryCycle]
  FROM [CETVacancies]
  Where VacancyID = @VacancyID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVacancyDetailsForReport]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Krishna Bharwad
-- Create date: 13-04-2023
-- Description:	Stored Procedure to get details of vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVacancyDetailsForReport] 
	-- Add the parameters for the stored procedure here
	@VacancyID int 
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [VacancyID]
      ,[CETEmployerId]
      ,[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered] 
	  ,[CETEmployer].BusinessLogo
	  ,CETEmployer.Name
  FROM [CETVacancies]
 inner join CETEmployer on CETEmployer.EmployerID=CETVacancies.CETEmployerId
 
  Where  VacancyID=  @VacancyID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVacancyListByEmployer]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 09-02-2023
-- Description:	Stored Procedure to get list of vacancies by Employer
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVacancyListByEmployer] 
	-- Add the parameters for the stored procedure here
	@EmployerID int,
	@VacancyStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
SELECT [VacancyID]
      ,[CETEmployerId]
      ,[CETVacancies].[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,[SalaryCycle]
	  ,CETVacancyStatusType.VacancyStatusTypeName
	--,CETStates.StateName
	-- ,CETStates.CurrencySymbol
	 						
  FROM [CETVacancies]

 
  
	--  inner join CETStates on CETStates.StateCode=CETVacancies.PrimaryLocation 
 --OR  CETStates.CurrencySymbol=CETVacancies.PrimaryLocation		--K
  inner join CETVacancyStatusType on CETVacancyStatusType.VacancyStatusTypeId = CETVacancies.VacancyStatusTypeId
  
	  where [CETVacancies].VacancyStatusTypeId = IIF(@VacancyStatus = 0, [CETVacancies].VacancyStatusTypeId, @VacancyStatus)
	  AND CETEmployerId = IIF(@EmployerID = 0, CETEmployerId, @EmployerID) 


	  
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVacancyListByLocation]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 09-02-2023
-- Description:	Stored Procedure to get list of vacancies by Employer
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVacancyListByLocation] 
	-- Add the parameters for the stored procedure here
	@JobLocation nvarchar(200),
	@VacancyStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if @JobLocation='all'							--Added By Krishna
	begin
SELECT [VacancyID]
      ,[CETEmployerId]
      ,[CETVacancies].[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,CETVacancyStatusType.VacancyStatusTypeName
	  ,case 
	  when @VacancyStatus=0 then 'Total Vacancies'
	  when @VacancyStatus=1 then 'Open Vacancies'
	  when @VacancyStatus=5 then 'Vacancies Under Scheduled Interview'
	  when @VacancyStatus=4 then 'Close Vacancies After Final Selection'
	  END 'VacancyStatus',
	  cet_uerms.fn_GetStateName(PrimaryLocation) 'StateName',
	  cet_uerms.fn_GetCountryName(PrimaryLocation) 'CountryName',
	  CETStates.CurrencySymbol
	--  CETStates.StateName,
	 -- CETStates.CountryName
  FROM [CETVacancies]
  --inner join CETStates on CETStates.StateCode = CETVacancies.PrimaryLocation
  inner join CETVacancyStatusType on CETVacancyStatusType.VacancyStatusTypeId = CETVacancies.VacancyStatusTypeId
  inner join CETStates on CETStates.StateCode = CETVacancies.PrimaryLocation
	  where [CETVacancies].VacancyStatusTypeId = IIF(@VacancyStatus = 0, [CETVacancies].VacancyStatusTypeId, @VacancyStatus)
	  --AND PrimaryLocation = IIF(@JobLocation = 'all', PrimaryLocation, @JobLocation)

	  ORDER BY [VacancyID] DESC				--Add by Krishna

	  end
	  else										--Added By Krishna
	  begin

	  SELECT [VacancyID]
      ,[CETEmployerId]
      ,[CETVacancies].[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,CETVacancyStatusType.VacancyStatusTypeName
	  ,case 
	  when @VacancyStatus=0 then 'Total Vacancies'
	  when @VacancyStatus=1 then 'Open Vacancies'
	  when @VacancyStatus=5 then 'Vacancies Under Scheduled Interview'
	  when @VacancyStatus=4 then 'Close Vacancies After Final Selection'
	  END 'VacancyStatus',
	 CETStates.StateName,
	 CETStates.CountryName,
	 CETStates.CurrencySymbol
	 FROM [CETVacancies]
	 inner join CETStates on CETStates.StateCode = CETVacancies.PrimaryLocation
	 inner join CETVacancyStatusType on CETVacancyStatusType.VacancyStatusTypeId = CETVacancies.VacancyStatusTypeId
	 where [CETVacancies].VacancyStatusTypeId = IIF(@VacancyStatus = 0, [CETVacancies].VacancyStatusTypeId, @VacancyStatus)
	 AND PrimaryLocation = IIF(@JobLocation = 'all', PrimaryLocation, @JobLocation)

	 ORDER BY [VacancyID] DESC			--Add by Krishna
	  end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVacancyListByLocationPageWise]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Krishna Bharwad
-- Create date: 28-03-2023
-- Description:	Stored procedure to get pagination in vacancy list
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVacancyListByLocationPageWise]  
	-- Add the parameters for the stored procedure here
	@JobLocation nvarchar(200),
	@VacancyStatus int = 0,
	@PageIndex INT = 1,
    @PageSize INT = 4,
    @RecordCount INT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT ROW_NUMBER() OVER
      (
			ORDER BY [VacancyID] ASC
			 )AS RowNumber


    -- Insert statements for procedure here
	, [VacancyID]
      ,[CETEmployerId]
      ,[CETVacancies].[VacancyStatusTypeId]
      ,[VacancyName]
      ,[VacancyCode]
      ,[PrimaryLocation]
      ,[JobType]
      ,[EmployementStatus]
      ,[CandidatesRequired]
      ,[RequiredMinExp]
      ,[RequiredMinQualification]
      ,[PostingDate]
      ,[VacancyDetails]
      ,[SalaryOffered]
	  ,CETVacancyStatusType.VacancyStatusTypeName
  FROM [CETRMS].[CETVacancies]

	


  inner join CETVacancyStatusType on CETVacancyStatusType.VacancyStatusTypeId = CETVacancies.VacancyStatusTypeId
	  where [CETVacancies].VacancyStatusTypeId = IIF(@VacancyStatus = 0, [CETVacancies].VacancyStatusTypeId, @VacancyStatus)
	  AND PrimaryLocation = IIF(@JobLocation = 'all', PrimaryLocation, @JobLocation)

	  SELECT @RecordCount = COUNT(*)
      FROM [CETRMS].[CETVacancies]

	  SELECT * FROM [CETRMS].[CETVacancies]
      WHERE [VacancyID] BETWEEN(@PageIndex -1) * @PageSize + 1 AND(((@PageIndex -1) * @PageSize + 1) + @PageSize) - 1



END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_GetVisaTypeList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 12-05-2023
-- Description:	Stored Procedure to get list of Visa Type
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_GetVisaTypeList] 
	-- Add the parameters for the stored procedure here
	@VisaCountry nvarchar(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	if @VisaCountry = 'all'
	begin
		SELECT [RecID]
			  ,[VisaTypeName]
			  ,[VisaCountry]
			  ,[VisaValidity]
			  ,[VisaTypeDetails]
			  ,[VisaStateCode]
		  FROM [UEVisaType]	
	end
	else
	begin
		SELECT [RecID]
			  ,[VisaTypeName]
			  ,[VisaCountry]
			  ,[VisaValidity]
			  ,[VisaTypeDetails]
			  ,[VisaStateCode]
		  FROM [UEVisaType]	
		  where VisaCountry LIKE '%'+@VisaCountry+'%'
	end


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_InsertDuePayment]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 15-03-2023
-- Description:	Stored Proceduret to insert Due Payment Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_InsertDuePayment] 
	-- Add the parameters for the stored procedure here
	@Currency nchar(4), 
	@Amount float,
	@TaxAmount float,
	@PaymentType int,
	@CETClientID int,
	@DueDate Date,
	@Reserve1 int,
	@Reserve2 int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @PaymentOrderNo nvarchar(50)
	DECLARE @CustSerialNo nvarchar(10);
	DECLARE @IsFirstTimeEntry int
	SET @IsFirstTimeEntry = 0

	if @PaymentType in (1,2)
	begin
		SELECT @IsFirstTimeEntry = count(*), @PaymentOrderNo = PaymentOrderNo from CETPayments where @CETClientID = CETClientID and @PaymentType in (1,2) group by PaymentOrderNo
		if @IsFirstTimeEntry is null SET @IsFirstTimeEntry = 0
	end

	if @IsFirstTimeEntry = 0
	begin
		SELECT @PaymentOrderNo = MAX(PaymentRecID) from CETPayments 

		if @PaymentOrderNo is Null
			SET @PaymentOrderNo = 1
		else
			SET @PaymentOrderNo = @PaymentOrderNo + 1

		SET @PaymentOrderNo = RIGHT('000000'+CAST(@PaymentOrderNo AS VARCHAR(6)),6)

		SET @PaymentOrderNo = @PaymentOrderNo
								+ RIGHT('00'+CAST(@PaymentType AS VARCHAR(2)),2) 
								+ RIGHT('00000'+CAST(@CETClientID AS VARCHAR(5)),4)
								+ LTRIM(RTRIM(@Currency))
								+ format(GETDATE(), 'ddMMyyyyhhmmss')

		DECLARE @NotificaionId int

		DECLARE @NotificationMessage nvarchar(200);

		if @PaymentType = 1 
			SET @NotificationMessage ='Kindly pay registration fee of $' + CONVERT(nvarchar(10), @Amount)
		else if @PaymentType = 2 
			SET @NotificationMessage = 'Kindly pay registration fee of $' + CONVERT(nvarchar(10), @Amount)
		else if @PaymentType  = 3
			SET @NotificationMessage = 'Kindly pay recruitment fee of $' + CONVERT(nvarchar(10), @Amount)
		else if @PaymentType  = 4
			SET @NotificationMessage = 'Kindly pay recruitment fee of $' + CONVERT(nvarchar(10), @Amount)

		INSERT INTO [CETNotification]
				   ([NotificationTime]
				   ,[NotificationType]
				   ,[NotificationMessage]
				   ,[CETClientID]
				   ,[NotificationStatus])
			 VALUES
				   (GETDATE()
				   ,2
				   ,@NotificationMessage
				   ,@CETClientID
				   ,1)
			SELECT @NotificaionId = SCOPE_IDENTITY();

		-- Insert statements for procedure here
			INSERT INTO [CETPayments]
					   ([PaymentOrderNo]
					   ,[Currency]
					   ,[Amount]
					   ,[TaxAmount]
					   ,[DueDate]
					   ,[PaymentType]
					   ,[TransactionType]
					   ,[CETClientID]
					   ,[PaymentStatus]
					   ,[NotificationID]
					   ,[NotificationType]
					   ,[Reserve1]
					   ,[Reserve2])
				 VALUES
					   (@PaymentOrderNo
					   ,@Currency
					   ,@Amount
					   ,@TaxAmount
					   ,@DueDate
					   ,@PaymentType
					   ,'CR'
					   ,@CETClientID
					   ,1
					   ,@NotificaionId
					   ,2
					   ,@Reserve1
					   ,@Reserve2)
		end
		
		SELECT @PaymentOrderNo 'PaymentOrderNo', @IsFirstTimeEntry 'IsFirstTimeEntry' -- 1: Old entry of similar transaction already exists and 0 : First time entry
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_InsertJobApplication]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 21-02-2023
-- Description:	Stored Procedure to Add Job Application
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_InsertJobApplication] 
	-- Add the parameters for the stored procedure here
	@VacancyID int,
	@CandidateID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ValidateApplication int
	DECLARE @ValidateCandidate int
	DECLARE @ValidateVacancy int
	DECLARE @EmployerID int
	SELECT @ValidateCandidate = count(*) from CETCandidate where CandidateId = @CandidateID
	if @ValidateCandidate is null SET @ValidateApplication = 0

	SELECT @ValidateVacancy = count(*) from CETVacancies where VacancyID = @VacancyID
	if @VacancyID is null SET @VacancyID = 0

	SELECT @EmployerID = CETEmployerId from CETVacancies where VacancyID = @VacancyID

    -- Insert statements for procedure here
	if @ValidateCandidate = 1 and @ValidateVacancy = 1
	begin
	INSERT INTO [JobApplications]
			   ([VacancyID]
			   ,[CandidateID]
			   ,[ApplicationStatus]
			   ,[CompanyRemarks]
			   ,[AppliedOn]
			   ,[RemarksOn])
		 VALUES
			   (@VacancyID
			   ,@CandidateID
			   ,1
			   ,null
			   ,GETDATE()
			   ,null)

	SET @ValidateApplication =  SCOPE_IDENTITY();

	UPDATE CETEmployer SET EmployerStatus = 5 where EmployerID = @EmployerID

	end
	else
	begin
		SET @ValidateApplication = 0;
	end

	SELECT @ValidateApplication 'ApplicationStatus' -- 0: Application not validate

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_InsertNewNotification]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 17-03-2023
-- Description:	Stored Procedure to add new notification
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_InsertNewNotification] 
	-- Add the parameters for the stored procedure here
	@NotificationType nchar(10),
	@NotificationMessage nvarchar(max),
	@CETClientID int,
	@Hyperlink nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO [CETNotification]
			   ([NotificationTime]
			   ,[NotificationType]
			   ,[NotificationMessage]
			   ,[CETClientID]
			   ,[NotificationStatus]
			   ,[HyperLink])
		 VALUES
			   (GETDATE()
			   ,@NotificationType
			   ,@NotificationMessage
			   ,@CETClientID
			   ,3
			   ,@Hyperlink)

	SELECT SCOPE_IDENTITY() 'NotificationID'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_InsertReferralDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 13-04-2023
-- Description:	Insert referral data in CETReferral Table
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_InsertReferralDetails] 
	-- Add the parameters for the stored procedure here
	@UECandidateID int, 
	@ReferralCode nvarchar(50)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Declare @ReferralID int
	Declare @IsCandidateIdPresent int
	Select @IsCandidateIdPresent = count(*) from CETCandidate where CandidateId = @UECandidateID
	--if @IsCandidateIdPresent is null SET @IsCandidateIdPresent = 0
    --Insert statements for procedure here
	--if @IsCandidateIdPresent = 0
	begin
		Insert into [UEReferral] ([UECandidateID],[ReferralStatus],[ReferralCode])
									 Values(@UECandidateID, 1, @ReferralCode)
		
		Select @ReferralID = SCOPE_IDENTITY()
     end					
	 Select @ReferralID 'ReferralID'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_JobApplicationList]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored Procedure to get List of Job Applications on chosen vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_JobApplicationList] 
	-- Add the parameters for the stored procedure here
	@VacancyID int,
	@ApplicationStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [JobApplicationID]
		  ,[VacancyID]
		  ,[CandidateID]
		  ,[ApplicationStatus]
		  ,[CompanyRemarks]
		  ,[AppliedOn]
		  ,[RemarksOn]
	  FROM [JobApplications]
	  where VacancyID = iif(@VacancyID = -1, VacancyID, @VacancyID)
			AND ApplicationStatus = IIF(@ApplicationStatus = -1, ApplicationStatus, @ApplicationStatus)

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_JobApplicationListByCandidate]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Krishna Bharwad
-- Create date: 22-03-2023
-- Description:	Stored Procedure to get List of Job Applications on chosen CandidateID
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_JobApplicationListByCandidate] 
	-- Add the parameters for the stored procedure here
	@CandidateID int,
	@ApplicationStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT [JobApplicationID]
		  ,[VacancyID]
		  ,[CandidateID]
		  ,[ApplicationStatus]
		  ,[CompanyRemarks]
		  ,[AppliedOn]
		  ,[RemarksOn]
	  FROM [JobApplications]
	  where CandidateID = @CandidateID
			AND ApplicationStatus = IIF(@ApplicationStatus = -1, ApplicationStatus, @ApplicationStatus)
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_JobApplicationListByEmployer]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored Procedure to get List of Job Applications on chosen vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_JobApplicationListByEmployer] 
	-- Add the parameters for the stored procedure here
	@EmployerID int,
	@ApplicationStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @VacancyListTable Table (RecID int identity(1,1), VacancyID int)

	insert into @VacancyListTable 
		select VacancyID from CETVacancies where CETVacancies.CETEmployerId = @EmployerID

	DECLARE @TempJobApplicationTable Table ([JobApplicationID] [int] NOT NULL,
	[VacancyID] [int] NULL,
	[CandidateID] [int] NULL,
	[ApplicationStatus] [int] NULL,
	[CompanyRemarks] [nvarchar](max) NULL,
	[AppliedOn] [datetime] NULL,
	[RemarksOn] [datetime] NULL,
	[OfferLetterGeneratedOn] [datetime] NULL,
	[OfferLetterData] [varbinary](max) NULL)

	DECLARE @RecCount int
	SELECT @RecCount = count(*) from @VacancyListTable

	DECLARE @Counter int
	SET @Counter = 1

	DECLARE @VacancyId int

	while @Counter <= @RecCount
	begin
		
		SELECT @VacancyId = VacancyID from @VacancyListTable where RecID = @Counter

		insert into @TempJobApplicationTable
			select * from JobApplications where @VacancyId = VacancyID and ApplicationStatus = @ApplicationStatus

		SET @Counter = @Counter + 1
	end

	select * from @TempJobApplicationTable

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_JobApplicationStatistics]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-04-2023
-- Description:	Stored Procedure to get Job Application Statistics for Vacancy
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_JobApplicationStatistics] 
	-- Add the parameters for the stored procedure here
	@VacancyID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @ApplicationReceived int
	DECLARE @InterviewScheduled int
	DECLARE @RejectedCandidates int

    -- Insert statements for procedure here
	SELECT @ApplicationReceived = count(*) from JobApplications where VacancyID = @VacancyID
	if @ApplicationReceived is null set @ApplicationReceived = 0
	SELECT @InterviewScheduled = count(*) from CETInterviews where JobApplicationID IN (SELECT JobApplicationID FROM JobApplications where VacancyID = @VacancyID)
	if @InterviewScheduled is null set @InterviewScheduled = 0
	SELECT @RejectedCandidates = count(*) from JobApplications where VacancyID = @VacancyID and ApplicationStatus = 6
	if @RejectedCandidates is null set @RejectedCandidates = 0


	SELECT @ApplicationReceived 'ApplicationReceived',
			@InterviewScheduled 'InterviewScheduled',
			@RejectedCandidates 'RejectedCandidates'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_ProposeNewInterview]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 23-02-2023
-- Description:	Stored Procedure to Propose new interview
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_ProposeNewInterview] 
	-- Add the parameters for the stored procedure here
	@JobApplicationID int, 
	@PreferredDateTime datetime,
	@EmployerRemarks nvarchar(max),
	@ChosenTimeZone nvarchar(50),
	@DurationInMinutes int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @Status int
	DECLARE @RecCount int
	DECLARE @IRecCount int
	DECLARE @JobVacancyID int			--Added by Durgesh
	DECLARE @EmployerID int
	DECLARE @InterviewID int

	SELECT @RecCount = count(*) from JobApplications where JobApplicationID = @JobApplicationID
	SELECT @IRecCount = count(*) from CETInterviews where JobApplicationID = @JobApplicationID
	if @IRecCount is null set @IRecCount = 0

	if @RecCount > 0 and @IRecCount = 0				--Edited by Durgesh
	begin
		INSERT INTO [CETInterviews]
				   ([JobApplicationID]
				   ,[PreferredDateTime]
				   ,[InterviewStatus]
				   ,[EmployerRemarks]
				   ,[ChosenTimeZone]
				   ,[DurationInMinutes])
			 VALUES
				   (@JobApplicationID,
				   @PreferredDateTime,
				   1,
				   @EmployerRemarks,
				   @ChosenTimeZone,
				   @DurationInMinutes)

		SELECT @InterviewID = SCOPE_IDENTITY()

		UPDATE JobApplications SET ApplicationStatus = 3 where JobApplicationID = @JobApplicationID
		SELECT @JobVacancyID = JobApplications.VacancyID from JobApplications where JobApplicationID = @JobApplicationID   --Added by Durgesh

		SELECT @EmployerID = CETEmployerId from CETVacancies where VacancyID = @JobVacancyID

		UPDATE CETVacancies SET VacancyStatusTypeId = 5 where VacancyID = @JobVacancyID		--Added by  Durgesh
		UPDATE CETEmployer SET EmployerStatus = 5 where EmployerID = @EmployerID -- If interview is scheduled then change employer status to InProcess


		SET @Status = 1
	end
	else
		SET @Status = 0

	SELECT @InterviewID 'InterviewID'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_RemoveUEClient]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 06-09-2023
-- Description:	Stored Procedure to delete client account
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_RemoveUEClient] 
	-- Add the parameters for the stored procedure here
	@CETClientID int,
	@UEClientType int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @RecCount int
	
	SELECT @RecCount = Count(*) from CETClient where ClientID = @CETClientID and ClientTypeID = @UEClientType
	if @RecCount is null SET @RecCount = 0

	if @RecCount = 1
	begin
		delete CETClient where ClientID = @CETClientID and ClientTypeID = @UEClientType
	end

	SELECT @RecCount 'Status'

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateBankAccountDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 11-04-2023
-- Description:	Store Procedure for Updating Bank Account Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateBankAccountDetails]
	-- Add the parameters for the stored procedure here
	@CandidateID int, 
	@BankAccountNumber nvarchar(50),
	@IFSCCode nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	Declare @Status int
	SET @Status = 0 
	Declare @RecCount int
	SET @RecCount = 0
	
	Select @RecCount = count(*) from CETCandidate Where CandidateId = @CandidateID

	if(@RecCount > 0)
	begin
	
	Update cet_uerms.[CETCandidate] SET [BankAccountNumber] = @BankAccountNumber where CandidateId = @CandidateID
	Update cet_uerms.[CETCandidate] SET [IFSCCode] = @IFSCCode where CandidateId = @CandidateID

	SET @Status = 1
	
	end
	Select @Status as 'Status'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCandidateContactDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored procedure to update Candidate Passport Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCandidateContactDetails] 
	-- Add the parameters for the stored procedure here
	@CandidateId int,
    @ContactNumberCountryCode nchar(10),
    @ContactNumber nchar(15),
    @CurrentStateCode nvarchar(50),
	@CurrentCityCode nvarchar(50),
    @PermanentAddress nvarchar(max),
    @PermanentStateCode nvarchar(200),
	@PermanentCityCode nvarchar(200),
    @CurrentAddress nvarchar(50)

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateId = @CandidateId group by Status
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateId

	UPDATE [CETCandidate]
	   SET [ContactNumberCountryCode] = @ContactNumberCountryCode
		  ,[ContactNumber] = @ContactNumber
		  ,[CurrentStateCode] = @CurrentStateCode
		  ,[CurrentCityCode] = @CurrentCityCode
		  ,[PermanentAddress] = @PermanentAddress
		  ,[PermanentCityCode] = @PermanentCityCode
		  ,[PermanentStateCode] = @PermanentStateCode
		  ,[CurrentAddress] = @CurrentAddress
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
		  

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateId
	if @CandidateStatus = 1
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateId

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCandidateOtherDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored procedure to update Candidate Other Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCandidateOtherDetails] 
	-- Add the parameters for the stored procedure here
	@CandidateId int,
    @ResumeFileType nvarchar(50),
    @ResumeFileName nvarchar(100),
    @ResumeData varbinary(max),
    @NoticePeriod int,
	@TotalExperienceMonths int

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateId = @CandidateId group by Status
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateId

	if @ResumeData is not null
	begin
	UPDATE [CETCandidate]
	   SET  ResumeFileType = @ResumeFileType,
			ResumeFileName = @ResumeFileName,
			ResumeData = @ResumeData,
			NoticePeriod = @NoticePeriod,
			TotalExperienceMonths = @TotalExperienceMonths
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
	  end
	  else
	  begin
	UPDATE [CETCandidate]
	   SET  NoticePeriod = @NoticePeriod,
			TotalExperienceMonths = @TotalExperienceMonths
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
	  end
		  

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateId
	if @CandidateStatus = 1
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateId

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCandidatePassportDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored procedure to update Candidate Passport Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCandidatePassportDetails] 
	-- Add the parameters for the stored procedure here
	@CandidateId int,
	@GivenName nvarchar(200),
	@LegalGuardianName nvarchar(200),
	@Surname nvarchar(200),
	@MotherName nvarchar(200),
	@SpouseName nvarchar(200),
	@PassportIssueDate date,
	@PassportExpiryDate date,
	@PassportNumber nvarchar(50),
	@PassportIssueLocation nvarchar(200),
	@PassportFileType nvarchar(50),
	@PassportFileName nvarchar(100),
	@PassportData varbinary(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateId = @CandidateId group by Status
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateId

	if @PassportData is not null
	begin
	UPDATE [CETCandidate]
	   SET [GivenName] = @GivenName
		  ,[LegalGuardianName] = @LegalGuardianName
		  ,[Surname] = @Surname
		  ,[MotherName] = @MotherName
		  ,[SpouseName] = @SpouseName
		  ,[PassportIssueDate] = @PassportIssueDate
		  ,[PassportExpiryDate] = @PassportExpiryDate
		  ,[PassportNumber] = @PassportNumber
		  ,[PassportIssueLocation] = @PassportIssueLocation
		  ,[PassportFileType] = @PassportFileType
		  ,[PassportFileName] = @PassportFileName
		  ,[PassportData] = @PassportData
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
	end
	else
	begin
	UPDATE [CETCandidate]
	   SET [GivenName] = @GivenName
		  ,[LegalGuardianName] = @LegalGuardianName
		  ,[Surname] = @Surname
		  ,[MotherName] = @MotherName
		  ,[SpouseName] = @SpouseName
		  ,[PassportIssueDate] = @PassportIssueDate
		  ,[PassportExpiryDate] = @PassportExpiryDate
		  ,[PassportNumber] = @PassportNumber
		  ,[PassportIssueLocation] = @PassportIssueLocation
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
	end
		  

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateId
	if @CandidateStatus = 1
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateId

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCandidatePersonalProfile]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored procedure to update Candidate Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCandidatePersonalProfile] 
	-- Add the parameters for the stored procedure here
	@CandidateId int,
	@Name nvarchar(200),
	@CandidateEmail nvarchar(200),
	@CandidateIntro nvarchar(max),
	@DateOfbirth date,
	@Gender int,
	@Photo varbinary(max),
	@MaritalStatus nvarchar(50),
	@Nationality nvarchar(200),
	@CandidateCast nvarchar(200),
	@VerifyEmail int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateId = @CandidateId group by Status
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateId


	if @Photo is not null
	begin
	UPDATE [CETCandidate]
	   SET [Name] = @Name
		  ,[CandidateEmail] = @CandidateEmail
		  ,[CandidateIntro] = @CandidateIntro
		  ,[DateOfbirth] = @DateOfbirth
		  ,[Gender] = @Gender
		  ,[Photo] = @Photo
		  ,[MaritalStatus] = @MaritalStatus
		  ,[Nationality] = @Nationality
		  ,[CandidateCast] = @CandidateCast
		  ,[LastUpdatedOn] = GETDATE()
		  ,[VerifyEmail] = @VerifyEmail
		Where CandidateId = @CandidateId
	end
	else
	begin
	UPDATE [CETCandidate]
	   SET [Name] = @Name
		  ,[CandidateEmail] = @CandidateEmail
		  ,[CandidateIntro] = @CandidateIntro
		  ,[DateOfbirth] = @DateOfbirth
		  ,[Gender] = @Gender
		  ,[MaritalStatus] = @MaritalStatus
		  ,[Nationality] = @Nationality
		  ,[CandidateCast] = @CandidateCast
		  ,[LastUpdatedOn] = GETDATE()
		  ,[VerifyEmail] = @VerifyEmail
		Where CandidateId = @CandidateId
	end

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateId
	if @CandidateStatus = 1
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateId

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCandidateQualificationDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored procedure to update Candidate Qualification Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCandidateQualificationDetails] 
	-- Add the parameters for the stored procedure here
	@CandidateId int,
    @HighestQualification nvarchar(200),
    @UniversityName nvarchar(512),
    @UniversityLocation nvarchar(200),
    @QualificationYear nchar(10)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateId = @CandidateId group by Status
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateId

	UPDATE [CETCandidate]
	   SET [HighestQualification] = @HighestQualification
		  ,[UniversityName] = @UniversityName
		  ,[UniversityLocation] = @UniversityLocation
		  ,[QualificationYear] = @QualificationYear
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
		  

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateId
	if @CandidateStatus = 1
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateId

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCandidateVisaDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		Prashant Sharma
-- Create date: 22-02-2023
-- Description:	Stored procedure to update Candidate Visa Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCandidateVisaDetails] 
	-- Add the parameters for the stored procedure here
	@CandidateId int,
	@VisaTypeID int,
	@VisaDetails nvarchar(max),
	@VisaValidUpto date,
	@VisaFileType nvarchar(50),
	@VisaFileName nvarchar(100),
	@VisaData varbinary(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @CandidateStatus int

	SELECT @RecCount = count(*), @CandidateStatus = Status from CETCandidate where CandidateId = @CandidateId group by Status
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @CandidateId

	if @VisaData is not null
	begin
	UPDATE [CETCandidate]
	   SET [VisaTypeID] = @VisaTypeID
		  ,[VisaDetails] = @VisaDetails
		  ,[VisaValidUpto] = @VisaValidUpto
		  ,[VisaFileType] = @VisaFileType
		  ,[VisaFileName] = @VisaFileName
		  ,[VisaData] = @VisaData
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
	 end
	 else
	 begin
	UPDATE [CETCandidate]
	   SET [VisaTypeID] = @VisaTypeID
		  ,[VisaDetails] = @VisaDetails
		  ,[VisaValidUpto] = @VisaValidUpto
		  ,[LastUpdatedOn] = GETDATE()
      Where CandidateId = @CandidateId
	 end
		  

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @CandidateId
	if @CandidateStatus = 1
		UPDATE CETCandidate SET Status = 2 where CandidateId = @CandidateId

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateCountryDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 11-05-2023
-- Description:	Stored Procedure to update country details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateCountryDetails] 
	-- Add the parameters for the stored procedure here
	@CountryCode nvarchar(5), 
	@CountryName nvarchar(512),
	@CurrencyName nvarchar(5),
	@CurrencySymbol nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE CETStates
	SET
		CountryName = @CountryName,
		CurrencySymbol = @CurrencySymbol,
		CurrencyName = @CurrencyName
	WHERE StateCode LIKE @CountryCode+'%'
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateEmployerDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-02-2023
-- Description:	Stored procedure to update Employer Details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateEmployerDetails] 
	-- Add the parameters for the stored procedure here
	@EmployerID int,
	@Name nvarchar(200),
	@BusinessName nvarchar(512),
	@Address nvarchar(max),
	@Email nvarchar(512),
	@WhatsAppNumber nvarchar(50),
	@LocationStateCode nvarchar(50),
	@LocationCityCode nvarchar(50),
	@BusinessLogo varbinary(max),
	@VerifyEmail int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	DECLARE @Status int
	SET @Status = 0;
	DECLARE @RecCount int
	SET @RecCount = 0
	DECLARE @ClientStatus int
	DECLARE @EmployerStatus int

	SELECT @RecCount = count(*), @EmployerStatus = EmployerStatus from CETEmployer where EmployerID = @EmployerID group by EmployerStatus
	
    -- Insert statements for procedure here
	if @RecCount > 0
	begin

	SELECT @ClientStatus = ClientStatusTypeID from CETClient where ClientID = @EmployerID

	if @BusinessLogo is not null
	begin
	UPDATE [CETEmployer]
	   SET [Name] = @Name
		  ,[BusinessName] = @BusinessName
		  ,[Address] = @Address
		  ,[Email] = @Email
		  ,[WhatsAppNumber] = @WhatsAppNumber
		  ,[LocationStateCode] = @LocationStateCode
		  ,[LocationCityCode] = @LocationCityCode
		  ,[BusinessLogo] = @BusinessLogo
		  ,[UpdateOn] = GETDATE()
		  ,[VerifyEmail] = @VerifyEmail
	 WHERE [EmployerID] = @EmployerID
	 end
	 else
	 begin
	UPDATE [CETEmployer]
	   SET [Name] = @Name
		  ,[BusinessName] = @BusinessName
		  ,[Address] = @Address
		  ,[Email] = @Email
		  ,[WhatsAppNumber] = @WhatsAppNumber
		  ,[LocationStateCode] = @LocationStateCode
		  ,[LocationCityCode] = @LocationCityCode
		  ,[UpdateOn] = GETDATE()
		  ,[VerifyEmail] = @VerifyEmail
	 WHERE [EmployerID] = @EmployerID
	 end

	 if @ClientStatus = 1
		UPDATE CETClient SET ClientStatusTypeID = 2 where ClientID = @EmployerID
	if @EmployerStatus = 1
	begin
		UPDATE CETEmployer SET EmployerStatus = 2 where EmployerID = @EmployerID

		-- Prashant : 16-03-2023 : Insert Due payment for first time profile update and send notification
		--EXEC sp_InsertDuePayment @Currency='USD', @Amount=50, @PaymentType=1, @CETClientID=@EmployerID
	end

	 SET @Status = 1
	 end

	 select @Status 'Status' -- 1: Record updated 0: Record not found for updation


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateJobApplicationStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-04-2023
-- Description:	Stored Procedure to update job application status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateJobApplicationStatus] 
	-- Add the parameters for the stored procedure here
	@JobApplicationID int = 0, 
	@JobApplicationStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [JobApplications]
   SET [ApplicationStatus] = @JobApplicationStatus
 WHERE JobApplicationID = @JobApplicationID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateNotificationStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 17-03-2023
-- Description:	Stored Procedure to update Notifiation Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateNotificationStatus] 
	-- Add the parameters for the stored procedure here
	@NotificationID int = 0, 
	@NotificationStatus int = 0
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE [CETNotification]
	   SET [NotificationStatus] = @NotificationStatus
	 WHERE NotificationID = @NotificationID

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdatePaymentStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 16-03-2023
-- Description:	Stored Procedure to update Payment Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdatePaymentStatus] 
	-- Add the parameters for the stored procedure here
	@PaymentRecID int, 
	@PaymentStatus int,
    @PaymentDate date,
    @StripePaymentID nvarchar(512),
    @StripePaymentDate datetime,
    @StripePaymentStatus nvarchar(128),
    @StripePaymentMessage nvarchar(max),
    @StripePaymentReceiptURL nvarchar(max),
    @StripePaymentMethod nvarchar(128)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @OldPaymentStatus int
	DECLARE @CETClientID int
	DECLARE @UEClientStatus int
	DECLARE @UEClientType int
	DECLARE @EmployerStatus int
	DECLARE @CandidateStatus int
	DECLARE @InvoiceNo int
	

	select @OldPaymentStatus = PaymentStatus, @CETClientID = CETClientID from CETPayments where PaymentRecID = @PaymentRecID
	select @UEClientStatus = ClientStatusTypeID, @UEClientType = ClientTypeID from CETClient where ClientID = @CETClientID

	if @UEClientType = 1
		SELECT @EmployerStatus = EmployerStatus, @CandidateStatus = -1 from CETEmployer where EmployerID = @CETClientID
	else
		SELECT @CandidateStatus = Status, @EmployerStatus = -1 from CETCandidate where CandidateId = @CETClientID

    -- Insert statements for procedure here
	UPDATE [CETPayments] 
	   SET [PaymentStatus] = @PaymentStatus
		  ,[PaymentDate] = @PaymentDate
		  ,[StripePaymentID] = @StripePaymentID
		  ,[StripePaymentDate] = @StripePaymentDate
		  ,[StripePaymentStatus] = @StripePaymentStatus
		  ,[StripePaymentMessage] = @StripePaymentMessage
		  ,[StripePaymentReceiptURL] = @StripePaymentReceiptURL
		  ,[StripePaymentMethod] = @StripePaymentMethod
	 WHERE PaymentRecID = @PaymentRecID

	 if @PaymentStatus = 4
	 begin
	 		SELECT @InvoiceNo = Max(InvoiceNo) from CETPayments
		if @InvoiceNo is null set @InvoiceNo = 1
		else SET @InvoiceNo = @InvoiceNo + 1

		UPDATE CETPayments SET InvoiceNo = @InvoiceNo WHERE PaymentRecID = @PaymentRecID 
		
		if @UEClientStatus = 2 UPDATE CETClient SET ClientStatusTypeID = 3 where ClientID = @CETClientID
		
		if @UEClientType = 1 
		begin
			if @EmployerStatus = 2 UPDATE CETEmployer SET EmployerStatus = 3 where EmployerID = @CETClientID
			else if @EmployerStatus = 5 UPDATE CETEmployer SET EmployerStatus = 6 where EmployerID = @CETClientID
			else if @EmployerStatus = 8 UPDATE CETEmployer SET EmployerStatus = 9 where EmployerID = @CETClientID
		end
		
		if @UEClientType = 2 
		begin
			if @CandidateStatus = 2 UPDATE CETCandidate SET Status = 3 where CandidateId = @CETClientID
			else if @CandidateStatus = 6 UPDATE CETCandidate SET Status = 7 where CandidateId = @CETClientID
			else if @CandidateStatus = 11 UPDATE CETCandidate SET Status = 12 where CandidateId = @CETClientID
		end
	 end
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdatePaymentTable]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 18-05-2023
-- Description:	Stored Procedure to update payment table with latest status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdatePaymentTable] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
		-- 1 - Declare Variables
		-- * UPDATE WITH YOUR SPECIFIC CODE HERE *
		DECLARE @PaymentRecID VARCHAR(50) -- database name 
		DECLARE @PaymentType VARCHAR(256) -- path for backup files 
		DECLARE @DueDays int -- filename for backup 
		DECLARE @DueDate datetime -- used for file name 
		DECLARE @PendingDate datetime -- used for file name 

		-- 2 - Declare Cursor
		DECLARE db_cursor CURSOR FOR 
		SELECT PaymentRecID 
		FROM CETPayments 
		WHERE PaymentStatus = 1 

		-- Open the Cursor
		OPEN db_cursor

		-- 3 - Fetch the next record from the cursor
		FETCH NEXT FROM db_cursor INTO @PaymentRecID  

		-- Set the status for the cursor
		WHILE @@FETCH_STATUS = 0  
 
		BEGIN  
			-- 4 - Begin the custom business logic
			SELECT @PaymentType = PaymentType, @DueDate = DueDate FROM CETPayments where PaymentRecID = @PaymentRecID

			--SELECT @DueDays = DueDays FROM CETPaymentType where PaymentTypeID = @PaymentType

			--SET @PendingDate = DATEADD(day, @DueDays, @DueDate)
			
			if @DueDate < GETDATE()
				UPDATE CETPayments SET PaymentStatus = 2 where PaymentRecID = @PaymentRecID

			-- 5 - Fetch the next record from the cursor
 			FETCH NEXT FROM db_cursor INTO @PaymentRecID 
		END 

		-- 6 - Close the cursor
		CLOSE db_cursor  

		-- 7 - Deallocate the cursor
		DEALLOCATE db_cursor 
END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateStaffMemberDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 08-02-2023
-- Description:	Stored Procedure to update staff member details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateStaffMemberDetails] 
	-- Add the parameters for the stored procedure here
	@userid nvarchar(50),
	@Name nvarchar(100),
	@Address nvarchar(max),
	@MobileNo nchar(10),
	@Email nvarchar(max),
	@Designation nvarchar(50),
	@TeamID int,
	@UserStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
UPDATE [UEStaff]
   SET [Name] = @Name 
      ,[Address] = @Address
      ,[MobileNo] = @MobileNo 
      ,[Email] = @Email
      ,[Designation] = @Designation
      ,[TeamID] = @TeamID
	  ,[UserStatus] = @UserStatus
 WHERE userid=@userid

 UPDATE MISUsers SET UserStatus = @UserStatus where UserID = @userid


END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateVacancyDetails]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Prashant Sharma
-- Create date: 21-02-2023
-- Description:	Stored procedure to update vacancy details
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateVacancyDetails] 
	-- Add the parameters for the stored procedure here
      @VacancyID int,
      @VacancyName nvarchar(200),
      @PrimaryLocation nvarchar(200),
      @JobType nchar(10),
      @EmployementStatus nvarchar(50),
      @CandidatesRequired int,
      @RequiredMinExp int,
      @RequiredMinQualification nvarchar(50),
      @VacancyDetails nvarchar(max),
      @SalaryOffered float,
	  @SalaryCycle int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
   DECLARE @CheckVacancyId int
	SELECT @CheckVacancyId = count(*) From CETVacancies where VacancyID = @VacancyID

	if @CheckVacancyId is null SET @CheckVacancyId = 0

    -- Insert statements for procedure here
	if @CheckVacancyId >0 
	begin
	UPDATE [CETVacancies]
	   SET [VacancyName] = @VacancyName
		  ,[PrimaryLocation] = @PrimaryLocation
		  ,[JobType] = @JobType
		  ,[EmployementStatus] = @EmployementStatus
		  ,[CandidatesRequired] = @CandidatesRequired
		  ,[RequiredMinExp] = @RequiredMinExp
		  ,[RequiredMinQualification] = @RequiredMinQualification
		  ,[PostingDate] = GETDATE()
		  ,[VacancyDetails] = @VacancyDetails
		  ,[SalaryOffered] = @SalaryOffered
		  ,[SalaryCycle] = @SalaryCycle
	 WHERE VacancyID = @VacancyID
	 end

	 SELECT @CheckVacancyId

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateVacancyStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 21-02-2023
-- Description:	Stored procedure to update vacancy status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateVacancyStatus] 
	-- Add the parameters for the stored procedure here
      @VacancyID int,
	  @VacancyStatus int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @CheckVacancyId int
	DECLARE @CandidateRequired int
	DECLARE @FilledSeats int
	SELECT @CheckVacancyId = count(*) From CETVacancies where VacancyID = @VacancyID

	if @CheckVacancyId is null SET @CheckVacancyId = 0

    -- Insert statements for procedure here
	if @CheckVacancyId >0 
	begin

	SELECT @FilledSeats = FilledSeats, @CandidateRequired = CandidatesRequired from CETVacancies where VacancyID = @VacancyID
	
	if @VacancyStatus = 2
	begin
		if @FilledSeats < @CandidateRequired
			SET @FilledSeats = @FilledSeats + 1

		if @FilledSeats = @CandidateRequired
		begin
			UPDATE [CETVacancies]
			   SET [VacancyStatusTypeId] = @VacancyStatus
			 WHERE VacancyID = @VacancyID
		end
	end
	else
	begin
		UPDATE [CETVacancies]
		   SET [VacancyStatusTypeId] = @VacancyStatus
		 WHERE VacancyID = @VacancyID
	 end

	 end
	 SELECT @CheckVacancyId

END
GO
/****** Object:  StoredProcedure [cet_uerms].[sp_UpdateZoomMeetingStatus]    Script Date: 11-12-2023 14:01:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Prashant Sharma
-- Create date: 27-02-2023
-- Description:	Stored Procedure to Update Zoom Meeting Status
-- =============================================
ALTER PROCEDURE [cet_uerms].[sp_UpdateZoomMeetingStatus] 
	-- Add the parameters for the stored procedure here
	@InterviewID int, 
	@ZVCMS_Status nvarchar(max),
	@ZVCMS_duration nvarchar(max),
	@ZVCMS_start_time nvarchar(max),
	@ZVCMS_end_time nvarchar(max),
	@ZVCMS_host_id nvarchar(max),
	@ZVCMS_dept nvarchar(max),
	@ZVCMS_participants_count nvarchar(max),
	@ZVCMS_source nvarchar(max),
	@ZVCMS_topic nvarchar(max),
	@ZVCMS_total_minutes nvarchar(max),
	@ZVCMS_type nvarchar(max),
	@ZVCMS_user_email nvarchar(max),
	@ZVCMS_user_name nvarchar(max)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

UPDATE [CETInterviews]
   SET [ZVCMS_Status] = @ZVCMS_Status
      ,[ZVCMS_duration] = @ZVCMS_duration
      ,[ZVCMS_start_time] = @ZVCMS_start_time
      ,[ZVCMS_end_time] = @ZVCMS_end_time
      ,[ZVCMS_host_id] = @ZVCMS_host_id
      ,[ZVCMS_dept] = @ZVCMS_dept
      ,[ZVCMS_participants_count] = @ZVCMS_participants_count
      ,[ZVCMS_source] = @ZVCMS_source
      ,[ZVCMS_topic] = @ZVCMS_topic
      ,[ZVCMS_total_minutes] = @ZVCMS_total_minutes
      ,[ZVCMS_type] = @ZVCMS_type
      ,[ZVCMS_user_email] = @ZVCMS_user_email
      ,[ZVCMS_user_name] = @ZVCMS_user_name
	 WHERE @InterviewID = InterviewID


END
GO
