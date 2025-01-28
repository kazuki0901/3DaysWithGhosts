using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //スポーンさせる敵のプレハブ 
    [SerializeField] List<GameObject> enemyPrefab;

    [SerializeField] Transform[] spawnPoints;//敵をスポーンさせる位置
    [SerializeField] float timeBetweenWaves;//ウェーブ間の間隔
    [SerializeField] int enemiesPerWave;//１度のウェーブで敵がスポーンする数

    [SerializeField] int poolSize;//プールの最大数
    //エネミーのプールを管理する
    private List<GameObject> enemyPool = new List<GameObject>();
    
    //エネミーを生成したときに渡す情報用
    [SerializeField] GameDirector gameDirector;
    [SerializeField] Transform target;
    [SerializeField] EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {

        InitializedEnemyPool();

        StartCoroutine(SpawnEnemies());
    }

    void InitializedEnemyPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            //リストからランダムなオブジェクトを選択
            int randomIndex = Random.Range(0, enemyPrefab.Count);
            //エネミーのインスタンスを生成
            GameObject enemyToSpawn = Instantiate(enemyPrefab[randomIndex]);

            //必要な情報を渡す
            EnemyHealth enemyHealth = enemyToSpawn.GetComponent<EnemyHealth>();
            enemyHealth.SetGameDirecter(gameDirector);
            EnemyAi enemyAi = enemyToSpawn.GetComponent<EnemyAi>();
            enemyAi.SetTarget(target);

            enemyHealth.SetEnemySpawner(enemySpawner);


            enemyToSpawn.SetActive(false);//非アクティブ状態

            enemyPool.Add(enemyToSpawn);//プールに入れる
          
        }

    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new
                //スポーンのタイミングをばらけさせる
                WaitForSeconds(timeBetweenWaves + Random.Range(-1f, 1f));

            for (int i = 0; i < enemiesPerWave; i++)
            {

                SpawnEnemy();

            };
        }
    }

    void SpawnEnemy()
    {
        GameObject enemyToSpawn = GetPooledEnemy();

        if (enemyToSpawn != null)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            enemyToSpawn.transform.position = spawnPoint.position;

            enemyToSpawn.SetActive(true);
        }



    }

    GameObject GetPooledEnemy()
    {
        foreach (GameObject enemy in enemyPool)
        {
            if (!enemy.activeInHierarchy)//SetActiveがどうなっているかの確認
            {
                return enemy;
            }
        }
        return null;
    }

    public void EnemyKilled(GameObject enemy)
    {
    
        enemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
