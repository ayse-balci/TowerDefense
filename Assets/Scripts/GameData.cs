[System.Serializable]
public class GameData
{
    public int level;
    public int[] fullLocations;
    public int leftTankCount;
    public int killedMonsterCount;
    public GameData(GameState gameState)
    {
        level = gameState.level;
        fullLocations = gameState.fullLocations;
        leftTankCount = gameState.leftTankCount;
        killedMonsterCount = gameState.killedMonsterCount;
    }
}
