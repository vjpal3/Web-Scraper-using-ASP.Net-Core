SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspScrapesInfo_GetAllByUserId]
				@UserId varchar(450)
as
begin
	set nocount on;
	SELECT * FROM dbo.ScrapesInfo 
		where UserId = @UserId ORDER BY ScrapeDate DESC
end
GO

