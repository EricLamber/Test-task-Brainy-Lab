using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCange : MonoBehaviour
{
    private byte EnemyScore;
    private byte PlayerScore;

    [SerializeField] private GameObject playerprefab;
    [SerializeField] private GameObject enemyprefab;
    [SerializeField] private GameObject playerStartPos;
    [SerializeField] private GameObject enemyStartPos;

    private void Awake()
    {
        HitEvent.OnEnemyHit.AddListener(EnemyHit);
        HitEvent.OnPlayerHit.AddListener(PlayerHit);
    }

    private void EnemyHit()
    {
        EnemyScore += 1;
        GetComponent<Text>().text = $"Enemy : {EnemyScore} | Player : {PlayerScore}";
        ResetPos();
    }

    private void PlayerHit()
    {
        PlayerScore += 1;
        GetComponent<Text>().text = $"Enemy : {EnemyScore} | Player : {PlayerScore}";
        ResetPos();
    }

    private void ResetPos()
    {
        Instantiate(playerprefab, playerStartPos.transform.position, playerStartPos.transform.rotation);
        Instantiate(enemyprefab, enemyStartPos.transform.position, enemyStartPos.transform.rotation);
    }
}
