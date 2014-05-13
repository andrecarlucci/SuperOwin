using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using Microsoft.Owin.Hosting;

namespace SuperOwin {
    class Program {
        static void Main(string[] args) {
            using (WebApp.Start<Startup>("http://localhost:12345")) {
                Console.ReadLine();
            }
        }
    }

    public class Startup {
        public void Configuration(IAppBuilder app) {
            app.UseWelcomePage();
        }
    }
}
