using Task01_Searchable;

ISearchable doc = new Document("Report", "This document contains a detailed annual report on climate change.");
ISearchable page = new WebPage("https://example.com", "<html><body>Welcome to the webpage!</body></html>");

doc.Search("annual");
doc.Search("budget");

page.Search("webpage");
page.Search("climate");