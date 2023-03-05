//Simple math calculator through query strings

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Run(async (HttpContext content) =>
{
    var query = content.Request.Query;

    if (content.Request.Method == "GET")
    {
        int firstNumber;
        int secondNumber;
        bool invalid = false;
        string error = string.Empty;
        List<string> operation = new List<string> { "add", "subtract", "multiply", "divide", "modulo" };

        if (!query.ContainsKey("firstNumber"))
        {
            invalid = true;
            error.Insert(error.Length, "Invalid input for 'firstNumber'\n");
        }
        if (!query.ContainsKey("secondNumber"))
        {
            invalid = true;
            error.Insert(error.Length, "Invalid input for 'secondNumber'\n");
        }
        if (!query.ContainsKey("operation") || !operation.Contains(query["operation"][0]))
        {
            invalid = true;
            error.Insert(error.Length, "Invalid input for 'operation'");
        }
        if (invalid == true)
        {
            content.Response.StatusCode = 400;
            await content.Response.WriteAsync(error);
            return;
        }

        try
        {
            firstNumber = Convert.ToInt32(query["firstNumber"][0]);
            secondNumber = Convert.ToInt32(query["secondNumber"][0]);
        }
        catch(Exception e) 
        {
            content.Response.StatusCode = 400;
            await content.Response.WriteAsync(e.Message);
            return;
        }

        switch (query["operation"])
        {
            case "add":
                await content.Response.WriteAsync($"{firstNumber + secondNumber}");
                break;
            case "multiply":
                await content.Response.WriteAsync($"{firstNumber * secondNumber}");
                break;
            case "subtract":
                await content.Response.WriteAsync($"{firstNumber - secondNumber}");
                break;
            case "divide":
                await content.Response.WriteAsync($"{firstNumber / secondNumber}");
                break;
            case "modulo":
                await content.Response.WriteAsync($"{firstNumber % secondNumber}");
                break;
        }
    }
});

app.Run();
