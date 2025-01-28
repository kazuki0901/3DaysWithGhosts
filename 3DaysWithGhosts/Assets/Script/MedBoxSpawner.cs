using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBoxSpawner : MonoBehaviour
{
    [SerializeField] GameObject MedBoxPrefab;//回復キットのプレハブ
    [SerializeField] Transform spawnPoint;//回復キットのスポーンする位置
    [SerializeField] float spawninterbal;//スポーンの間隔
    private GameObject spawnItem;




    // Start is called before the first frame update
    void Start()
    {
       
        //ゲーム開始時にアイテムをスポーン
        SpawnItem();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnItem()
    {
        if(spawnItem == null)
        {
            //アイテムが存在しない場合、アイテムをスポーン
            spawnItem = Instantiate(MedBoxPrefab,spawnPoint.position,Quaternion.identity);
        }
    }

    public void ItemCollected()
    {
        if(spawnItem != null)
        {
            //アイテムが取得されたとき呼び出す
            Destroy(spawnItem);
            spawnItem = null;

            StartCoroutine(RespawnItemDelay());

            
        }
    }

    IEnumerator
        RespawnItemDelay()
    {
        //一定時間後にアイテムをスポーン
        yield return new WaitForSeconds(spawninterbal);
        SpawnItem();
    }
}
