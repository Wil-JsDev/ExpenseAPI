namespace ExpenseAPI.Extensions
{
    public static class Extension
    {

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "StoreDeLaCruz");
            });

        }
    }
}

