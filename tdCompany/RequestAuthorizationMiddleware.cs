using tdCompany.Interfaces;

namespace MovieApp
{
    public class RequestAuthorizationMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestAuthorizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserRepository userRepository, IJwtService jwtService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                var userId = jwtService.ValidateToken(token);
                if (userId != Guid.Empty)
                    context.Items["User"] = await userRepository.GetById(Guid.Parse(userId.ToString()));
                await _next(context);
            }
            else
            {
                try
                {
                    await _next(context);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
