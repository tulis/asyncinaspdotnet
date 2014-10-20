using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class FileReader
    {
        const string filePath = @"C:\Users\tanris\Documents\asyncinaspdotnet\Async\Server\data_2012";

        public static async Task<string> SearchAsync( string item )
        {
            using( var fileStream = new FileStream(
                filePath,
                FileMode.Open,
                FileAccess.Read,
                FileShare.Read,
                bufferSize: 4096,
                useAsync: true ) )
            {
                
                var buffer = new byte [ 0x1000 ];
                var numberOfBytesRead = await fileStream.ReadAsync( buffer, 0, buffer.Length );
                while( numberOfBytesRead != 0 )
                {
                    var record = Encoding.Unicode.GetString( buffer, 0, numberOfBytesRead );
                    if( record.Contains( item ) ) return record;
                    numberOfBytesRead = await fileStream.ReadAsync( buffer, 0, buffer.Length);
                }

                return "Record not found";
            }
        }

        public static async Task<string> SearchInNoThreadAsync(string item)
        {
            var numberOfLines = File.ReadAllLines(filePath).Length;
            return await SearchAsync(item);
        }

        public static async Task<string> SearchInNewThreadAsync(string item)
        {
            return await Task.Run(async () =>
            {
                var numberOfLines = File.ReadAllLines(filePath).Length;
                return await SearchAsync(item);
            });
        }
    }
}