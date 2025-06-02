using System;
using System.Linq.Expressions;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private Canvas shopCanvas;
    private EnemyCountManager _enemyCountManager;
    public Difficulty difficulty = Difficulty.Normal;
    
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
        shopCanvas.enabled = true;
        ContinueGame();
    }

    public void ContinueGame()
    {
        Debug.Log(difficulty);
        Time.timeScale = 1;
        hudCanvas.enabled = true;
        shopCanvas.enabled = false;

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
