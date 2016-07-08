using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour
{

    #region Main

    void OnTriggerEnter2D( Collider2D col )
    {
        Destroy( col.gameObject );
    }

    #endregion
   
}

