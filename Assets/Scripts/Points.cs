using UnityEngine;
using System.Collections;

public class Points : MonoBehaviour
{

    #region Main

    void Start()
    {
        liveTimer = Time.time;
    }
   
    void Update()
    {

        bool destroy=Time.time > liveTimer + 1f;

        if( destroy )
        {
            Destroy( gameObject );
        }
    }

    #endregion

    #region Pivate Variable

    private float liveTimer;

    #endregion
    
}
