public class GuardianNewsApiResponse
{
    public Response Response { get; set; }
}

public class Response
{
    public string Status { get; set; }
    public int Total { get; set; }
    public int StartIndex { get; set; }
    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int Pages { get; set; }
    public string OrderBy { get; set; }
    public List<NewsArticle> Results { get; set; }
}

public class NewsArticle
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string SectionId { get; set; }
    public string SectionName { get; set; }
    public string WebPublicationDate { get; set; }
    public string WebTitle { get; set; }
    public string WebUrl { get; set; }
    public string ApiUrl { get; set; }
    public bool IsHosted { get; set; }
    public string PillarId { get; set; }
    public string PillarName { get; set; }
}
