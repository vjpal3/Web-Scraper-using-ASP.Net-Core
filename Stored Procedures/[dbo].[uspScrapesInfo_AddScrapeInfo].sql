SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspScrapesInfo_AddScrapeInfo]
					@ScrapeDate datetime2, @TimeZone nchar(6), @UserId nvarchar(450)
AS
SET NOCOUNT ON
Insert into dbo.ScrapesInfo
			(ScrapeDate, TimeZone, UserId)
		values
			(@ScrapeDate, @TimeZone, @UserId)
GO

