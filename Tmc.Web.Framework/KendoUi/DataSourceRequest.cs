namespace Tmc.Web.Framework.KendoUi
{
    public class DataSourceRequest
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public DataSourceRequest()
        {
            this.Page = 0;
            this.PageSize = 10;
        }
    }
}
