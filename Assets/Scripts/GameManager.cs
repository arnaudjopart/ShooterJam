using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    #region Public Variable

    public LevelManager lvlManager;

    #endregion

    #region Static Variable

   public static GameState currentState;

    #endregion

    #region Main
 void Start()
    {
        currentState = GameState.PAUSE;
    }
    #endregion

    #region My Method

    public void EndGame()
    {
        currentState = GameState.GAMEOVER;
        print( "End of Game" );

    }

    public void StartGame()
    {
        currentState = GameState.GAMEON;
    }

    #endregion

    #region EnumCreator

    public enum GameState { GAMEON, PAUSE, GAMEOVER };

    #endregion
    

}
