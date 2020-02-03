# Web-Scraper-using-ASP.Net-Core
A full stack web scraper to import stock data from personal Yahoo Finance account

Using C#, Selenium Webdriver and XPATH, the scraper logs into Yahoo Finance securely with login credentials and navigates to the portfolio and extracts the required data.

The scraped data is then stored in SQL Server database using Dapper and stored procedures. The database is normalized for optimal performance and allows the logged in user to view the historic scrapes as well as request a new scrape.

User authentication implemented using .NET Identity framework.

The scraper is configured in ASP.Net Core MVC and uses different routes to display data.

