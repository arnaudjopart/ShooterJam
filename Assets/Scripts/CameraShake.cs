using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

    #region Public And  Protected Variable
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform _CamTransform;
    // How long the object should shake for.
    [Tooltip("Change the duration of shake")]
    [Range(0,1)]public float _ShakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
   
    [Range(0,1)]public float _ShakeAmount = 0.7f;
   
    [Range(0,1)]public float _SdecreaseFactor = 1.0f;

    #endregion

    #region Main
    void Awake()
    {
        if( _CamTransform == null )
        {
            _CamTransform = GetComponent( typeof( Transform ) ) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = _CamTransform.localPosition;
    }

    void Update()
    {
        if( _ShakeDuration > 0 )
        {
            _CamTransform.localPosition = originalPos + Random.insideUnitSphere * _ShakeAmount;

            _ShakeDuration -= Time.deltaTime * _SdecreaseFactor;
        }
        else
        {
            _ShakeDuration = 0f;
            _CamTransform.localPosition = originalPos;
        }
    }
    #endregion

    #region Private Variable

    private Vector3 originalPos;

    #endregion
    
}