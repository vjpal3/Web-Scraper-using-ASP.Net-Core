SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[uspCompanies_AddCompany] 
					@SymbolName nchar(10), @CompanyName nvarchar(50)
AS
SET NOCOUNT ON
Insert into dbo.Companies 
			(SymbolName, CompanyName)
		select @SymbolName, @CompanyName
		where not exists (select * from dbo.Companies where SymbolName = @SymbolName and CompanyName = @CompanyName)
GO

