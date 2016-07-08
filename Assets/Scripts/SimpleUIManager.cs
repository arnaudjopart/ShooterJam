using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;

public class SimpleUIManager : MonoBehaviour
{
    #region Public And Protected Variable

    [Header("GUIElement UI")]
  
    public Text _TextScore;
    [Space(5f)]
    public Text _LivesText;
    [Space(5f)]
    public Button _StartButton;
    [Space(5f)]
    public RectTransform _LivesImage;

    [Header("Element Script")]
    [Space(15f)]
    public Player _Player;

    #endregion

    #region Static Variable

    public static bool One = false;

    #endregion

    #region List Variables

    [Header("Bordure")]
    [Space(10f)]
    public List<ParticleSystem> Border;

    #endregion

    #region Main

    void Start()
    {
        Canvas = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        Vector2 size = _LivesImage.sizeDelta;
        livesInitialHeight = size.y;
        liveLostOnDamage = livesInitialHeight / _Player._MaxLives;

    }

    void Update()
    {
    
        AffichageScore();
        switch( GameManager.currentState )
        {
            case GameManager.GameState.GAMEON:
                _LivesText.text = "Lives: " + _Player._Lives;
                AffichageScore();
                if( One == true )
                {
                    for( int i = 0; i < Border.Count; i++ )
                    {
                        Border[ i ].Play();
                    }
                }
                if( One == false )
                {
                    for( int j = 0; j < Border.Count; j++ )
                    {
                        Border[ j ].Stop();
                    }
                }
                break;
            case GameManager.GameState.PAUSE:
                break;
            case GameManager.GameState.GAMEOVER:
                Canvas.SetBool( "OpenDoor", false );
                AffichageScore();
                _StartButton.enabled = true;
                _StartButton.GetComponentInChildren<Text>().text = "Retry";
                break;
            default:
                break;
        }


    }

    #endregion
   
    #region MyMethods

    public void ButtonOpenDoor()
    {
        audioSource.Play();
        _StartButton.enabled = false;
        Canvas.SetBool( "OpenDoor", true );
        GameManager.currentState = GameManager.GameState.GAMEON;
        Player.Score = 0;
        _Player.InitializePlayer();
        LiveReset();
    }

    void AffichageScore()
    {
        score = Player.Score.ToString();
        _TextScore.text = score;
    }

    public void UpdateLives()
    {
        Vector2 newSize = _LivesImage.sizeDelta;
        float newHeightOfLives = newSize.y - liveLostOnDamage;
        newSize.y = newHeightOfLives;
        _LivesImage.sizeDelta = newSize;

    }

    public void LiveReset()
    {
        Vector2 newSize = _LivesImage.sizeDelta;
        float newHeightOfLives = livesInitialHeight;
        newSize.y = newHeightOfLives;
        _LivesImage.sizeDelta = newSize;
    }
    #endregion

    #region Private Variable

    private Animator Canvas;
    private string score = "0";
    private float livesInitialHeight;
    private float liveLostOnDamage;
    private AudioSource audioSource;

    #endregion
   
  
   
  
}
