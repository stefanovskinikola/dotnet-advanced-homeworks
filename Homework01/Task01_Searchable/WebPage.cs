namespace Task01_Searchable;

public class WebPage : ISearchable
{
    private readonly string _url;
    private readonly string _htmlContent;

    public WebPage(string url, string htmlContent)
    {
        _url = url;
        _htmlContent = htmlContent;
    }

    public void Search(string word)
    {
        bool found = _htmlContent.Contains(word, StringComparison.OrdinalIgnoreCase);
        Console.WriteLine(found
            ? $"WebPage: '{_url}' - Keyword '{word}' FOUND."
            : $"WebPage: '{_url}' - Keyword '{word}' NOT FOUND.");
    }
}