using System.Threading.Tasks;
using System.Web;

namespace Server
{
    public class SearchNoThread : HttpTaskAsyncHandler
    {
        public async override Task ProcessRequestAsync( HttpContext context )
        {
            var result = await FileReader.Search( "cip-cip" );
            context.Response.ContentType = "text/plain";
            context.Response.Write( result );
        }
    }
}