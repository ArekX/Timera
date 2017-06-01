using RestSharp;

namespace Provider.Base.REST.Enums
{
    public enum RequestMethod
    {
        GET = Method.GET,
        POST = Method.POST,
        PUT = Method.PUT,
        DELETE = Method.DELETE,
        HEAD = Method.HEAD,
        PATCH = Method.PATCH,
        MERGE = Method.MERGE,
        OPTIONS = Method.OPTIONS
    }
}
