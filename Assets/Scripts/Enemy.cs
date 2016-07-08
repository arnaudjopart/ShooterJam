using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{

    #region Public And Protected Menbers

    [Header("Element numerique")]
    public int _Speed;
    public int _Lives=2;
    public float _FrequenceOfShootInseconds;

    [Header("Element Audio")]
    public AudioSource _SoundEnnemie;
    public AudioClip   _ExplosionE;

    [Header("Element GameObject")]
    public GameObject _PointPrefab; 
    public GameObject _BeamPrefab;
    public GameObject _ExplodePrefab;

    [Header("Element Sprite")]
    public SpriteRenderer m_SpriteEnemy;

    [Header("Element Script")]
    public Player _Player;
  
    #endregion

    #region Main

    void Start()
    {
        frequenceOfShootPerSecond = 1 / _FrequenceOfShootInseconds;
        frequenceOfShootPerframe = frequenceOfShootPerSecond * Time.deltaTime;

        _rb2D = GetComponent<Rigidbody2D>();
        _Speed = Random.Range( 1, 3 );
        _rb2D.velocity = Vector3.down * _Speed;

        _projectilesContainer = GameObject.Find( "ProjectilesContainer" );

        if( _projectilesContainer == null )
        {
            _projectilesContainer = new GameObject( "ProjectilesContainer" );
        }

    }

    void Update()
    {
        if( Random.value < frequenceOfShootPerframe )
        {
            Shoot();
        }

        if( GameManager.currentState == GameManager.GameState.GAMEOVER )
        {
            GameObject.Destroy( this.gameObject );
        }

    }

    void OnTriggerEnter2D( Collider2D col )
    {
        GetDamage( 1 );
    }

    #endregion

    #region MyMethods

    private void GetDamage( int damage )
    {
        _Lives -= damage;
        if( _Lives <= 1 )
        {
            m_SpriteEnemy.color = Color.red;
            Invoke( "ReturnNormal", .3f );
        }
        if( _Lives <= 0 )
        {
            Player.Score += 50;
            GameObject explosion = Instantiate(_ExplodePrefab, transform.position, Quaternion.identity) as GameObject;
            explosion.transform.SetParent( _projectilesContainer.transform, false );
            GameObject.Destroy( this.gameObject );
            AudioSource.PlayClipAtPoint( _ExplosionE, transform.position );
            GameObject points = Instantiate(_PointPrefab, transform.position, Quaternion.identity) as GameObject;
            points.transform.SetParent( _projectilesContainer.transform, false );
        }

    }

    private void Shoot()
    {
        GameObject laser = Instantiate(_BeamPrefab, transform.position, Quaternion.identity) as GameObject;
        laser.transform.SetParent( _projectilesContainer.transform );
    }

    void ReturnNormal()
    {
        m_SpriteEnemy.color = Color.white;
    }

    #endregion

    #region Private Members

    private float frequenceOfShootPerSecond;
    private float frequenceOfShootPerframe;
    private GameObject _projectilesContainer;
    private Rigidbody2D _rb2D;

    #endregion

}



