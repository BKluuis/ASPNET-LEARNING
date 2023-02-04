using Microsoft.AspNetCore.Http;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.UseEndpoints(endpoints =>
    {
        Dictionary<int, string> countries = new Dictionary<int, string>()
        {
            { 1, "United States" },
            { 2, "Canada" },
            { 3, "United Kingdom" },
            { 4, "India" },
            { 5, "Japan" }
        };

        endpoints.MapGet("countries", async (HttpContext context) => 
        { 
            context.Response.StatusCode = 200;
            foreach(KeyValuePair<int, string> country in countries)
            {
                await context.Response.WriteAsync($"{country.Key}, {country.Value}\n");
            }
        });

        endpoints.MapGet("countries/{countryID:int:range(1,100)}", async (HttpContext context) =>
        {
            int id = Convert.ToInt32(context.GetRouteValue("countryID"));

            if (countries.ContainsKey(id))
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync($"{id}, {countries[id]}");
            }
            else
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync($"The country id '{id}' isn't present in our list of countries");
            }
        });
    }
);

app.Run();