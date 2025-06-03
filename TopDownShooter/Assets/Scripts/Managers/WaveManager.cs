using System;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas nextCanvas;
    private EnemyCountManager _enemyCountManager;
    public Difficulty difficulty;
    
    [Header("Enemies")]
    [SerializeField] private GameObject easyEnemies;
    [SerializeField] private GameObject normalEnemies;
    [SerializeField] private GameObject hardEnemies;

    private void Awake()
    {
        _enemyCountManager = gameObject.GetComponent<EnemyCountManager>();
    }

    public void WaveEnded()
    {
        Time.timeScale = 0;
        hudCanvas.enabled = false;
        nextCanvas.enabled = true;
    }

    public void ContinueGame()
    {
        Debug.Log(difficulty);
        Time.timeScale = 1;
        hudCanvas.enabled = true;
        nextCanvas.enabled = false;

        switch (difficulty)
        {
            case Difficulty.Easy:
                SetChildrenActive(easyEnemies);
                break;
            
            case Difficulty.Normal:
                SetChildrenActive(easyEnemies,normalEnemies);
                break;
            
            case Difficulty.Hard:
                SetChildrenActive(easyEnemies,normalEnemies, hardEnemies);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        _enemyCountManager.FindEnemies();
        
    }

    private void SetChildrenActive(params GameObject[] parents)
    {
        for (var indexParent = 0; indexParent < parents.Length; indexParent++)
        {
            var parentTransform = parents[indexParent].transform;
            var children = parentTransform.childCount;

            for (var indexChildren = 0; indexChildren < children; indexChildren++)
            {
                parentTransform.GetChild(indexChildren).gameObject.SetActive(true);
                parentTransform.GetChild(indexChildren).GetComponent<Enemy>().ResetPosition();
            }
        }
    }
    
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard
    }
}
