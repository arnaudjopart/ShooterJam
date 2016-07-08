using UnityEngine;
using System.Collections;

public class ParticleManager : MonoBehaviour
{

    #region Main
   
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if( !ps.IsAlive() )
        {
            Destroy( transform.parent.gameObject );
        }
    }

    #endregion

    #region Private Variable

    private ParticleSystem ps;

    #endregion
    
}
