using UnityEngine;

public enum GameType { Normal, SpeedRun }
public enum ControlType { Normal, WorldTilt }
public enum WallType { Normal, Punishing}

public class GameController : MonoBehaviour
{
    public GameType gameType;
    public ControlType controlType;
    public WallType wallType;
    
    void Start()
    {
        //Comment the line below out to test our scenes. 
        //When we want to run from the title scene or in the build, uncomment it
        GetGameType();
        GetWallType();
    }

    //Gets the game type from our saved data
    public void GetGameType()
    {
        string savedGameType = PlayerPrefs.GetString("GameType");
        if (savedGameType != "")
            gameType = (GameType)System.Enum.Parse(typeof(GameType), savedGameType);
    }

    //Sets the game type from our selections
    public void SetGameType(GameType _gameType)
    {
        gameType = _gameType;
        PlayerPrefs.SetString("GameType", _gameType.ToString());
    }

    //To toggle between speedrun on or off
    public void ToggleSpeedRun(bool _speedRun)
    {
        if (_speedRun)
            SetGameType(GameType.SpeedRun);
        else
            SetGameType(GameType.Normal);
    }

    //Gets the wall type from our saved data
    public void GetWallType()
    {
        string savedWallType = PlayerPrefs.GetString("WallType");
        if (savedWallType != "")
            wallType = (WallType)System.Enum.Parse(typeof(WallType), savedWallType);
    }

    //Sets the wall type from our selections
    public void SetWallType(WallType _wallType)
    {
        wallType = _wallType;
        PlayerPrefs.SetString("WallType", _wallType.ToString());
    }

    //To toggle between punishing walls on or off
    public void ToggleWallType(bool _punishing)
    {
        if (_punishing)
            SetWallType(WallType.Punishing);
        else
            SetWallType(WallType.Normal);
    }
}
