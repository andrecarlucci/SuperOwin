using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Hosting;
using System.Collections;

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
            app.Use<Mw1>();
            app.Use<Mw2>();
            app.UseErrorPage();

            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute("default", "api/{controller}");
            app.UseWebApi(config);

            app.UseHandlerAsync((req, res) => {
                if (req.Path.Contains("/fail")) {
                    throw new Exception("ho ho ho");
                }
                res.ContentType = "text/plain";
                return res.WriteAsync("Hello TDC");
            });
        }
    }

    public class HomeController : ApiController {
        public int[] GetValues() {
            return new[] {1, 2, 3};
        }
    }

    public class Mw1 {
        private Func<IDictionary<string, object>, Task> _next;

        public Mw1(Func<IDictionary<string, object>, Task> next) {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env) {
            Console.WriteLine("Mw1");
            await _next.Invoke(env);
        }
    }

    public class Mw2 {
        private Func<IDictionary<string, object>, Task> _next;

        public Mw2(Func<IDictionary<string, object>, Task> next) {
            _next = next;
        }

        public async Task Invoke(IDictionary<string, object> env) {
            Console.WriteLine("Mw2");
            //foreach (var key in env.Keys) {
            //    Console.WriteLine("{0} {1}", key, env[key]);
            //}
            await _next.Invoke(env);
        }
    }


}