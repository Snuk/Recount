﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Recount.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>().Build().Run();
        }
    }
}
