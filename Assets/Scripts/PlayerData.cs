public class PlayerData
{
    public uint highScore;

    public PlayerData(uint highScore = 0)
    {
        this.highScore = highScore;
    }

    public override string ToString()
    {
        return $"New high score saved: {highScore}";
    }
}
