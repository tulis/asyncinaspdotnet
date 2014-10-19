using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main( string [] args )
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var numberOfRequest = Int32.Parse( args[0] );
            var uri = args[1];
            try
            {
                var result = Request( numberOfRequest, uri );
                Console.WriteLine( String.Format( "Result: {0}", result ) );
            }
            catch( Exception exception )
            {
                Console.WriteLine(exception.ToString());
            }
            
            stopwatch.Stop();
            var timeSpan = stopwatch.Elapsed;
            Console.WriteLine("{0} requests take about {1:00}:{2:00}:{3:00}:{4:000}", 
                numberOfRequest, 
                timeSpan.Hours,
                timeSpan.Minutes,
                timeSpan.Seconds,
                timeSpan.Milliseconds);
        }

        static string Request( int numberOfRequest, string uri )
        {
            var httpClient = new HttpClient();
            var tasks = Enumerable
                .Range( 0, numberOfRequest )
                .Select( index => httpClient.GetStringAsync(new Uri(uri) ))
                .ToArray();

            Task.WaitAll( tasks );
            return new string( tasks
                .SelectMany( response => String.Format( "--> {0}\n", response.Result ) )
                .ToArray());
        }
    }
}
