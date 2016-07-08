using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour
{

    #region Public And Protected Variable

    [Tooltip("Give a multiplier velocity of the laser")]
    [Range(-15,15)] public float _Speed;
    [Range(0,5)]public int damage = 1;
 
    #endregion
       
    #region Main

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.velocity = Vector3.up * _Speed;
    }

    void OnTriggerEnter2D( Collider2D col )
    {
        Destroy( this.gameObject );
    }

    #endregion

    #region MyMethod

    public int Damage { get { return damage; } }

    #endregion

    #region Private Variable

    private Rigidbody2D _rb2D;

    #endregion

}
