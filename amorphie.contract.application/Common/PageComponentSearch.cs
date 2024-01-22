using amorphie.core.Base;

namespace amorphie.contract.application
{
    public class PageComponentSearch : DtoSearchBase
    {

    }
    public class ComponentSearch
    {
        public required string Keyword { get; set; }
    }
}