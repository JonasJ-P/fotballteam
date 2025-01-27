class Team
{
    public string TeamName{get;set;} = "";
    List<Player> Players{get;set;}

    public Team(string teamname)
    {
        TeamName = teamname;
        Players = new List<Player>();
    }

    //A method to add a player to the list
    public void AddPlayer(Player player)
    {
        if(player == null)
        {
            throw new ArgumentException("All the fields have to filled in");
        }
        Players.Add(player);
    }

    //A method to get the player list
    public List<Player> GetPlayers()
    {
        return Players;
    }

    //A method to remove a player from the list
    public void DeletePlayer(int playerId)
    {
        if(Players.Count < playerId +1 || playerId < 0)
        {
            throw new ArgumentException("You have enter a wrong ID");
        }
        Players.RemoveAt(playerId);
    }

    //A method to update a player on the list
    public void UpdatePlayer(Player player, int playerId)
    {
        if(Players.Count < playerId + 1 || playerId < 0)
        {
            throw new ArgumentException("You have enter a wrong ID");
        }
        if(player == null)
        {
            throw new ArgumentException("All the fields have to filled in");
        }
        Players[playerId] = player;
    }
    //A method to find all players with the selected ranking
    public List<Player> FindRanking(int ranking)
    {
        if(5 < ranking || ranking < 0)
        {
            throw new ArgumentException("You have to enter a rating between 0 and 5");
        }
        List<Player> SelectedPlayers = new List<Player>();
        foreach(var player in Players)
        {
            if(player.Ranking == ranking)
            {
                SelectedPlayers.Add(player);
            }
        }
        return SelectedPlayers;
    }

    //A method to find the oldest age on the team(in the list)
    public List<Player> GetOldest()
    {
        List<Player> SelectedPlayers = new List<Player>();
        int oldest = Players.Max(player => player.Age);
        foreach(var player in Players)
        {
            if(player.Age == oldest)
            {
                SelectedPlayers.Add(player);
            }
        }
        return SelectedPlayers;
    }


}