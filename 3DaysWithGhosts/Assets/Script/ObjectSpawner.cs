using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //生成するプレハブを入れる
    [SerializeField] List<GameObject> objectSpawn;
    //生成するオブジェクト数
    [SerializeField] int numberObject;
    //フィールドの大きさ
    [SerializeField] Vector3 fieldSize;
    //生成エリアの中心
    [SerializeField] Transform spawnSenter;
    //生成されたプレハブ間の最小距離
    [SerializeField] float minDistance;

    //生成されたプレハブの位置を保持するリスト
    List<Vector3> spawnPotisions = new List<Vector3>();




    // Start is called before the first frame update
    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {

        for (int i = 0; i < numberObject; i++)
        {
            Vector3 randomposition;
            bool positionIsValid;
            do
            {
                //ランダムな位置を生成
                randomposition = new Vector3(Random.Range(-fieldSize.x / 2, fieldSize.x / 2), 0,
                Random.Range(-fieldSize.y / 2, fieldSize.y / 2));
                //プレハブ間の位置が近すぎないかチェック
                positionIsValid = true;

                foreach (Vector3 pos in spawnPotisions)
                {
                    if (Vector3.Distance(pos, randomposition) < minDistance)
                    {
                        positionIsValid = false;
                        break;
                    }
                }

            }
            while (!positionIsValid);



            //リストからランダムなオブジェクトを選択
            int randomIndex = Random.Range(0, objectSpawn.Count);
            GameObject objectToSpwan = objectSpawn[randomIndex];

            //プレハブの高さを取得して生成位置を調整（埋まらないようにする)
            Collider objectCollider = objectToSpwan.GetComponent<Collider>();

            if (objectCollider != null)
            {

                //プレハブのColliderの底辺のY座標の調整
                randomposition.y = objectCollider.bounds.extents.y + spawnSenter.position.y;
            }

            //オブジェクト生成
            GameObject spawnObject = Instantiate(objectToSpwan, randomposition, Quaternion.identity);

            if (objectCollider != null)
            {
                Vector3 spawnPos = spawnObject.transform.position;
                spawnPos.y = objectCollider.bounds.extents.y + spawnSenter.position.y;

                spawnObject.transform.position = spawnPos;
            }

            //生成された位置をリストに追加
            spawnPotisions.Add(randomposition);
        }
    }
}
