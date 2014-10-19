using System.Threading.Tasks;
using System.Web;

namespace Server
{
    public class SearchNewThread : HttpTaskAsyncHandler
    {
        public async override Task ProcessRequestAsync( HttpContext context )
        {
            var result = await Task.Run( async () => await FileReader.Search( "cip-cip" ) );
            context.Response.ContentType = "text/plain";
            context.Response.Write( result );
        }
    }
}