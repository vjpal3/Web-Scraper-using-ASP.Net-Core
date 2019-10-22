SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[uspStocksData_AddStockData] 
					@ScrapeId int, 
					@SymbolId int, 
					@LastPrice decimal(19, 4), 
					@Change decimal(19, 4), 
					@PercentChange decimal(19, 4), 
					@PrevClose decimal(19, 4), 
					@OpenPrice decimal(19, 4), 
					@Shares int, 
					@CostBasics decimal(19, 4), 
					@TradeDate date, 
					@PercentAnnualGain decimal(19, 4), 
					@FiftyTwoWeekHigh decimal(19, 4), 
					@FiftyTwoWeekLow decimal(19, 4), 
					@Bid decimal(19, 4), 
					@BidSize int, 
					@Ask decimal(19, 4), 
					@AskSize int, 
					@MarketCap decimal(19, 4)
AS
SET NOCOUNT ON
Insert into dbo.StocksData
			(ScrapeId, SymbolId, LastPrice, Change, PercentChange, PrevClose, OpenPrice, Shares, 
			CostBasics, TradeDate, PercentAnnualGain, FiftyTwoWeekHigh, FiftyTwoWeekLow, Bid, 
			BidSize, Ask, AskSize, MarketCap)
		values
			( @ScrapeId, @SymbolId, @LastPrice, @Change, @PercentChange, @PrevClose, 
			@OpenPrice, @Shares, @CostBasics, @TradeDate, @PercentAnnualGain, @FiftyTwoWeekHigh, 
			@FiftyTwoWeekLow, @Bid, @BidSize, @Ask, @AskSize, @MarketCap)
GO

