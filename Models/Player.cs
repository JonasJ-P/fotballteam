class Player
{
    public string Name{get; private set;} = "";
    public int Age{get; private set;} = 0;
    public string Position{get; private set;} = "";
    public int Ranking {get; private set;} = 0;
    public Player(string name, int age, string position, int ranking){
        SetName(name);
        SetAge(age);
        SetPosition(position);
        SetRanking(ranking);

    }

    //A set method to set the name of the player and check if input name is empty or not
    public void SetName(string name)
    {
        if(string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Name cannot be null or empty");
        }
        Name=name;
    }

    //A set method to set age, and check to se if age is more than 0, could change this to only allow over 10 or something else
    //Depending on what the team would allow of age
    public void SetAge(int age)
    {
        if(age < 0)
        {
            throw new ArgumentException("You have to enter a age of the player");
        }
        Age=age;
    }

    //A set method that only allows the input to be one of 4 things
    public void SetPosition(string position)
    {
        int check = 0;
        string[] positionArray = ["Goalkeeper", "Defender", "Midfielder", "Striker"];
        foreach(string positions in positionArray)
        {
            if(position == positions)
            {
                Position = position;
                check++;
            }
        }
        if (check==0)
        {
            throw new ArgumentException("You have not selected a position");
        }
    }

    //A set method that allows the setting of a ranking between 0 and 5
    public void SetRanking(int ranking)
    {
        if((ranking < 0) || (ranking > 5))
        {
            throw new ArgumentException("Ranking has to be between 0 and 5");
        }
        Ranking=ranking;
    }
}