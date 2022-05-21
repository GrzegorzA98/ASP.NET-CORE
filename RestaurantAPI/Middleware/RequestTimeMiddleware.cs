using System.Diagnostics;

namespace RestaurantAPI.Middleware {

    public class RequestTimeMiddleware : IMiddleware {

        private readonly ILogger logger;
        private Stopwatch stopwatch;

        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger) {

            stopwatch = new Stopwatch();
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {

            stopwatch.Start();
            await next.Invoke(context);
            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds / 1000 > 4) {

                var message = $"Request [{context.Request.Method}] at {context.Request.Path} took {elapsedMilliseconds} ms";

                logger.LogInformation(message);

            }

        }

    }

}
