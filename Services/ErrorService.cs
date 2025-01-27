class ErrorService
{
    //A method that checks if there is a team created and that there is players in it
    public static void CheckTeam(Team? team)
    {
        if(team == null)
        {
            throw new ArgumentException("There is no team created");
        }
        else if(!team.GetPlayers().Any())
        {
            throw new ArgumentException("There is no players in the team");
        }
    }
    public static void OneTeamOnly(Team? team)
    {
        if(team != null)
        {
            throw new ArgumentException("There can only be one team");
        }
    }

    public static void CheckIfTeamExist(Team? team)
    {
        if(team == null)
        {
            throw new ArgumentException("You have to create a team first");
        }
    }
    public static void NotNullEmpty(string teamname)
    {
        if (String.IsNullOrWhiteSpace(teamname))
        {
            throw new ArgumentException("You have to enter a name for the team");
        }
    }
}