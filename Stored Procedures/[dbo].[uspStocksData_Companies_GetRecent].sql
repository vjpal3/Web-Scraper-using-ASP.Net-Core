SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspStocksData_Companies_GetRecent] 
		@UserId varchar(450)
as
begin
	set nocount on;
	SELECT 
		SymbolName, CompanyName,
		LastPrice, Change, PercentChange, Shares, TradeDate
	FROM Companies, StocksData
	WHERE Companies.Id = StocksData.SymbolId
	AND StocksData.ScrapeId = (SELECT TOP 1 ScrapeId FROM dbo.ScrapesInfo 
		where UserId = @UserId ORDER BY ScrapeId DESC);
	
end


GO

