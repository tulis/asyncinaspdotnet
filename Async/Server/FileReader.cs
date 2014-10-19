using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public static class FileReader
    {
        public static async Task<string> Search( string item )
        {
            const string filePath = @"D:\data_2012";
            var numberOfLines = File.ReadAllLines( filePath ).Length;
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
    }
}