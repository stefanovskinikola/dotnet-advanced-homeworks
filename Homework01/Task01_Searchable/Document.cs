namespace Task01_Searchable;

public class Document : ISearchable
{
    private readonly string _title;
    private readonly string _content;

    public Document(string title, string content)
    {
        _title = title;
        _content = content;
    }

    public void Search(string word)
    {
        bool found = _content.Contains(word, StringComparison.OrdinalIgnoreCase);
        Console.WriteLine(found
            ? $"Document: '{_title}' - Keyword '{word}' FOUND."
            : $"Document: '{_title}' - Keyword '{word}' NOT FOUND.");
    }
}