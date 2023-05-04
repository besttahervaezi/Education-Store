namespace Application.wrappers;

public abstract class RequestParametersBasic:PaginationsParametersDto
{
    private string _search { get; set; }
    public TypeSort Typesort { get; set; } = TypeSort.Desc;
    public int Sort { get; set; } = 1;
    public string Search
    {
        get => _search;
        
        set=> _search=value?.ToLower();
    }
    public enum TypeSort
    {
        Desc = 1,
        Asc
    }
}