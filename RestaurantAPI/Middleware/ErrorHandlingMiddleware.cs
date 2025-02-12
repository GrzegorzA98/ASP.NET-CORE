﻿using RestaurantAPI.Exceptions;

namespace RestaurantAPI.Middleware {

    public class ErrorHandlingMiddleware : IMiddleware {

        private readonly ILogger<ErrorHandlingMiddleware> logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) {

            this.logger = logger;

        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next) {

            try {

                await next.Invoke(context);

            }

            catch (NotFoundException notFoundException) {

                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);

            }

            catch (BadRequestException badRequest) {

                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);

            }

            catch (ForbidException forbidException) {

                context.Response.StatusCode = 403;

            }

            catch (Exception ex) {

                logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");

            }

        }

    }

}
