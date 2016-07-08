using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    #region MyMethod

    public void LoadNextLevel()
    {
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex + 1 );
    }

    public void LoadSceneByIndex( int i )
    {
        SceneManager.LoadScene( i );
    }

    #endregion 

}
