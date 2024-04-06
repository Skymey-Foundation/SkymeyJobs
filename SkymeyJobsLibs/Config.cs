using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkymeyJobsLibs
{
    public class Config
    {
        public static string Path { get; set; }
        public static string MongoDbDatabase { get; set; }
        public static string MongoClientConnection { get; set; }
        public static string? TinkoffAPI {  get; set; }
        public Config()
        {

        }
    }
}
