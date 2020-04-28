-- basic exercises in groups of 3 (Chinook database)
-- 1. List all customers (full names, customer ID, and country) who are not in the US
SELECT *
FROM Customer
WHERE Country != 'USA';
-- 2. List all customers from brazil
SELECT *
FROM Customer
WHERE COUNTRY = 'Brazil';
-- 3. List all sales agents
SELECT *
FROM Employee
WHERE Title LIKE '%Sales%Agent%';
-- 4. Show a list of all countries in billing addresses on invoices.
SELECT BillingCountry
FROM Invoice
GROUP BY BillingCountry;
-- 5. How many invoices were there in 2009, and what was the sales total for that year?
SELECT YEAR(InvoiceDate) AS Year, COUNT(InvoiceId) AS 'Total Invoices', SUM(Total) AS 'Total Profit'
FROM Invoice
WHERE YEAR(InvoiceDate) = 2009
GROUP BY YEAR(InvoiceDate);
-- 6. How many line items were there for invoice #37?
SELECT Total
FROM Invoice
WHERE InvoiceId = 37;
-- 7. How many invoices per country?
SELECT BillingCountry, COUNT(InvoiceId) AS 'Total Invoices'
FROM Invoice
GROUP BY BillingCountry;
-- 8. Show total sales per country, ordered by highest sales first.
SELECT BillingCountry, SUM(Total) AS 'Total Invoices'
FROM Invoice
GROUP BY BillingCountry
ORDER BY SUM(TOTAL) DESC;