SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[uspScrapesInfo_GetByScrapeId] 
		@ScrapeId int
as
begin
	set nocount on;
	SELECT * FROM dbo.ScrapesInfo WHERE ScrapeId = @ScrapeId;
end
GO

