using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedBoxSpawner : MonoBehaviour
{
    [SerializeField] GameObject MedBoxPrefab;//�񕜃L�b�g�̃v���n�u
    [SerializeField] Transform spawnPoint;//�񕜃L�b�g�̃X�|�[������ʒu
    [SerializeField] float spawninterbal;//�X�|�[���̊Ԋu
    private GameObject spawnItem;




    // Start is called before the first frame update
    void Start()
    {
       
        //�Q�[���J�n���ɃA�C�e�����X�|�[��
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
            //�A�C�e�������݂��Ȃ��ꍇ�A�A�C�e�����X�|�[��
            spawnItem = Instantiate(MedBoxPrefab,spawnPoint.position,Quaternion.identity);
        }
    }

    public void ItemCollected()
    {
        if(spawnItem != null)
        {
            //�A�C�e�����擾���ꂽ�Ƃ��Ăяo��
            Destroy(spawnItem);
            spawnItem = null;

            StartCoroutine(RespawnItemDelay());

            
        }
    }

    IEnumerator
        RespawnItemDelay()
    {
        //��莞�Ԍ�ɃA�C�e�����X�|�[��
        yield return new WaitForSeconds(spawninterbal);
        SpawnItem();
    }
}
