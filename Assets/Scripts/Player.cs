using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    #region Public And Protected Variable

    [Header("Element numerique")]

    public int _Speed;
	public int _Lives = 3;
    [Range(0,1)]public float _FireRate;
    public int _MaxLives = 3;

    [Header("Element Audio")]

    public AudioSource _Sound;
	public AudioClip a_Explosion, a_FireLaser;

    [Header("Element GameObject")]

    public GameObject g_LaserPreFab;
    public GameObject g_Explosion;

    [Header("Transform")]
 
    public Transform _Camera;

    [HideInInspector]
    public Vector3 _MoveInPosition;
    [HideInInspector]
    public Vector3 _StartPosition;
    [HideInInspector]
    public Vector3 _Vecteur;

    [Header("Element Particule")]

    public ParticleSystem _ReacteurPlayer;

    [Header("Element Script")]

    public SimpleUIManager _UI;
    public GameManager _Mana;

    [Header("Les enum")]

    public States currentState;
    #endregion

    #region Static Variable

    public static int Score=0;

    #endregion

    #region Main

    void Start ()
	{
        _Lives = _MaxLives;
		//ReacteurPlayer.Stop();
        _transform = GetComponent<Transform>();
        
        _projectilesContainer = GameObject.Find("ProjectilesContainer");
        if (_projectilesContainer == null)
        {
            _projectilesContainer = new GameObject("ProjectilesContainer");
        }
	}

    void Update () 
	{
       if(GameManager.currentState == GameManager.GameState.GAMEON)
        {
            switch (currentState)   
            {
                case States.PLAYING:

                    _Vecteur.x = Input.GetAxis("Horizontal");
                    _Vecteur.y = Input.GetAxis("Vertical");

                    bool fire=Input.GetKey(KeyCode.Space);
                    if (fire)
                    {
                        if (Time.time > shootTimer + _FireRate)
                        {
                            shootTimer = Time.time;
                            print("Shoot");
                            Shoot();
                        }
                    }
                    Move();
                    _Lives = Mathf.Max(0, _Lives );

                    break;
                case States.MOVEIN:
                    speedPerFrame = _Speed * Time.deltaTime;
                    float distance = Vector3.Distance(_MoveInPosition, _transform.position);
                    _transform.position += speedPerFrame*distance *.2f* Vector3.Normalize(_MoveInPosition - _transform.position);
                    bool dist=distance < 0.1;
                    if (dist)
                    {
                        currentState = States.PLAYING;
                        _transform.position = _MoveInPosition;
                    }
                    break;
                default:
                    break; 
            }
            
        }
        
    }

    void OnTriggerEnter2D( Collider2D col )
    {
        _Lives--;
        _Camera.GetComponent<CameraShake>()._ShakeDuration = .5f;
        _UI.UpdateLives();

        bool live1 = _Lives <= 1;

        bool live0 = _Lives <= 0;

        if( live1 )
        {
            SimpleUIManager.One = true;
        }
        if( live0 )
        {
            //GameObject newExplosion = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
            _Sound.PlayOneShot( a_Explosion, 0.5f );
            Invoke( "EndOfGame", 1f );
            AudioSource.PlayClipAtPoint( a_Explosion, transform.position );
            SimpleUIManager.One = false;
            gameObject.SetActive( false );
        }
    }

    IEnumerator TimeShake()
    {
        for( int i = 0; i < 6; i++ )
        {
            _Camera.Translate( Random.Range( -0.5f, 0.5f ), 0f, 0f );
            yield return new WaitForSeconds( 0.005f );
        }

        //yield return new WaitForSeconds(0.1f);
        _Camera.position = new Vector3( 0f, 0f, -10f );

    }

    #endregion

    #region MyMethods

    public void InitializePlayer()
    {
        _transform.position = _StartPosition;
        gameObject.SetActive(true);
        _Lives = 3;
        currentState = States.MOVEIN;
    }
	
    private void Move()
    {
        Vector3 position = _transform.position+ _Vecteur * _Speed * Time.deltaTime;

        position = new Vector3(Mathf.Clamp(position.x, -6, 6), Mathf.Clamp(position.y, -10, 10), 0);
        _transform.position = position;
        
		if (Input.GetKeyDown ("up")) 
		{
			_ReacteurPlayer.Play ();
		} 
		if(Input.GetKeyUp ("up"))
		{
			_ReacteurPlayer.Stop ();
		}
    }

    private void Shoot()
    {
		_Sound.PlayOneShot (a_FireLaser,0.5f);
        Vector3 spawnPosition = new Vector3(_transform.position.x + (-.3f + Random.value * .6f), _transform.position.y, 0); 
        GameObject laser = Instantiate(g_LaserPreFab, spawnPosition, Quaternion.identity) as GameObject;
        laser.transform.SetParent(_projectilesContainer.transform, false);
    }

    private void EndOfGame()
    {
        _Mana.EndGame();
    }

    public enum States {PLAYING, MOVEIN};

    #endregion

    #region Private Variable
    private float shootTimer;
    private GameObject _projectilesContainer;
    private float speedPerFrame;
    private Transform _transform;
    #endregion

}
