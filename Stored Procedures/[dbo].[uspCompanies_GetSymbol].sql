SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[uspCompanies_GetSymbol] 
		@SymbolName nvarchar(10)
as
begin
	set nocount on;
	SELECT Id FROM dbo.Companies WHERE SymbolName = @SymbolName 
end
GO

