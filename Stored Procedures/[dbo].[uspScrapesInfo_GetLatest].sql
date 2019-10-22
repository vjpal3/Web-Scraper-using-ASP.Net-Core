SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspScrapesInfo_GetLatest]
				@UserId varchar(450)
as
begin
	set nocount on;
	SELECT TOP 1 ScrapeId FROM dbo.ScrapesInfo 
		where UserId = @UserId ORDER BY ScrapeId DESC
end
GO

