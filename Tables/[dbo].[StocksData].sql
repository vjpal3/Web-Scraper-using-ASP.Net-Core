SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StocksData](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ScrapeId] [int] NOT NULL DEFAULT ((0)),
	[SymbolId] [int] NULL,
	[LastPrice] [decimal](19, 4) NULL,
	[Change] [decimal](19, 4) NULL,
	[PercentChange] [decimal](19, 4) NULL,
	[PrevClose] [decimal](19, 4) NULL,
	[OpenPrice] [decimal](19, 4) NULL,
	[Shares] [int] NULL,
	[CostBasics] [decimal](19, 4) NULL,
	[TradeDate] [date] NULL,
	[PercentAnnualGain] [decimal](19, 4) NULL,
	[FiftyTwoWeekHigh] [decimal](19, 4) NULL,
	[FiftyTwoWeekLow] [decimal](19, 4) NULL,
	[Bid] [decimal](19, 4) NULL,
	[BidSize] [int] NULL,
	[Ask] [decimal](19, 4) NULL,
	[AskSize] [int] NULL,
	[MarketCap] [decimal](19, 4) NULL
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[StocksData] ADD PRIMARY KEY CLUSTERED 
(
	[Id] ASC
) ON [PRIMARY]
GO
CREATE NONCLUSTERED INDEX [IX_StocksData_Symbol] ON [dbo].[StocksData]
(
	[SymbolId] ASC
) ON [PRIMARY]
ALTER TABLE [dbo].[StocksData]  WITH CHECK ADD  CONSTRAINT [FK_StocksData_Companies] FOREIGN KEY([SymbolId])
REFERENCES [dbo].[Companies] ([Id])
ALTER TABLE [dbo].[StocksData] CHECK CONSTRAINT [FK_StocksData_Companies]
ALTER TABLE [dbo].[StocksData]  WITH CHECK ADD  CONSTRAINT [FK_StocksData_ScrapesInfo] FOREIGN KEY([ScrapeId])
REFERENCES [dbo].[ScrapesInfo] ([ScrapeId])
ALTER TABLE [dbo].[StocksData] CHECK CONSTRAINT [FK_StocksData_ScrapesInfo]
GO

