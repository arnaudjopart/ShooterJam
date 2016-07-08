using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    #region Public And Protected Variable

    [Header("Element GameObject")]
    [Tooltip("Mettre la prefab ennemie")]
    public GameObject _PrefabToSpawn;

    [Header("Element Numerique")]
    public float _FrequenceOfSpawn;

    [Header("Element Script")]
    public Player _Player;

    #endregion   

    #region Main

    void Update()
    {
        bool gamescene=GameManager.currentState == GameManager.GameState.GAMEON;
        bool spawntime=Time.time > _SpawnTimer + Mathf.Max( 1, 5 * Random.value + _FrequenceOfSpawn - Mathf.RoundToInt( _SpawnTimer / 5 ) );
        if( gamescene )
        {
            if( spawntime )
            {
                _SpawnTimer = Time.time;
                Vector3 spawnPosition = new Vector3(-6 + Random.value * 12, 10, 0);
                GameObject spawnedObject = Instantiate(_PrefabToSpawn, spawnPosition, Quaternion.identity) as GameObject;
                spawnedObject.transform.SetParent( this.transform, false );
            }
        }
    }

    #endregion

    #region Private Variable

    private float _SpawnTimer;

    #endregion

}
