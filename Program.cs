using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.OpenApi; 


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//For allowing access from front end
builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("allowAll");
app.UseStaticFiles();
app.UseHttpsRedirection();

//Copied from chat GPT
// Global Exception Handler (catch all unhandled exceptions)
app.UseExceptionHandler(appBuilder =>
{
    appBuilder.Run(async context =>
    {
        context.Response.ContentType = "application/json";

        // Set default status code
        context.Response.StatusCode = 500;  // Internal Server Error by default

        var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (ex != null)
        {
            var response = new
            {
                Message = ex.Message,  // This will include the exception message
                Details = ex.StackTrace // Optionally include stack trace for debugging (may be omitted for security reasons)
            };

            // Handle specific exception types, like ArgumentException, and set appropriate status code
            if (ex is ArgumentException)
            {
                context.Response.StatusCode = 400; // Bad Request for ArgumentExceptions
            }

            // Write the response as JSON
            await context.Response.WriteAsJsonAsync(response);
        }
    });
});


//Creates an in memory class of Team and calls it newteam
Team? newteam = null;

app.MapGet("/", context => 
{
    context.Response.Redirect("http://localhost:5171/index.html");
    return Task.CompletedTask;
});

app.MapGet("/swagger", context => 
{
    context.Response.Redirect("http://localhost:5171/swagger/index.html");
    return Task.CompletedTask;
});


//Creates the team
app.MapPost("/createteam", ([FromForm] string teamname) => {
    //Checks that there isn't a team already made, will only allow one team to be made in this program
    ErrorService.OneTeamOnly(newteam);
    ErrorService.NotNullEmpty(teamname);
    newteam = new Team(teamname);
    return Results.Ok(new {Message = "You created a team"});
    
}).DisableAntiforgery();


//Adds the player into the list that is in the Team class
app.MapPost("/addplayer", ([FromBody] Player addplayer) => {
    //It checks to see if there is a team created
    ErrorService.CheckIfTeamExist(newteam);
    Player newplayer = new Player(addplayer.Name, addplayer.Age, addplayer.Position, addplayer.Ranking);
    newteam!.AddPlayer(newplayer);
    return Results.Ok(newteam); 
    
}).DisableAntiforgery();


//A get function that first uses a error function in ErrorService to se if there is a team made and it has players
//Then uses the method in Team class to get a the list of players in the team
app.MapGet("/getplayers", () => {
    ErrorService.CheckTeam(newteam);
    return Results.Ok(newteam!.GetPlayers());
});


//A delete function that first uses a error function in ErrorService to se if there is a team made and it has players
//Then uses a method in the Team class to delete a player
app.MapDelete("/deleteplayer", ([FromForm] int playerID) => {
    ErrorService.CheckTeam(newteam);
    newteam!.DeletePlayer(playerID);
    return Results.Ok(new {Message = "Person was deleted"});
}).DisableAntiforgery();


//A put function that first uses a error function in ErrorService to se if there is a team made and it has players
//Then it uses a method in Team class to update the player information
app.MapPut("/updateplayer/{playerid}", ([FromBody] Player player, int playerid) => {
    ErrorService.CheckTeam(newteam);
    newteam!.UpdatePlayer(player, playerid);
    return Results.Ok(new {Message = "Player was updated"});
}).DisableAntiforgery();

//A post function that first uses a error function in ErrorService to se if there is a team made and it has players
//Then uses a method in Team class to find all players with a spesific ranking
app.MapPost("/findranking", ([FromForm] int rankingnum) => {
    ErrorService.CheckTeam(newteam);
    return Results.Ok(newteam!.FindRanking(rankingnum));
}).DisableAntiforgery();


//A get function that first uses a error function in ErrorService to se if there is a team made and it has players
//Then it uses a method in Team class to find the oldest player
app.MapGet("/findoldest", () => {
    ErrorService.CheckTeam(newteam);
    return Results.Ok(newteam!.GetOldest());
}).DisableAntiforgery();

app.Run();
