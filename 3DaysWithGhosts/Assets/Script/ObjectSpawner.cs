using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    //��������v���n�u������
    [SerializeField] List<GameObject> objectSpawn;
    //��������I�u�W�F�N�g��
    [SerializeField] int numberObject;
    //�t�B�[���h�̑傫��
    [SerializeField] Vector3 fieldSize;
    //�����G���A�̒��S
    [SerializeField] Transform spawnSenter;
    //�������ꂽ�v���n�u�Ԃ̍ŏ�����
    [SerializeField] float minDistance;

    //�������ꂽ�v���n�u�̈ʒu��ێ����郊�X�g
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
                //�����_���Ȉʒu�𐶐�
                randomposition = new Vector3(Random.Range(-fieldSize.x / 2, fieldSize.x / 2), 0,
                Random.Range(-fieldSize.y / 2, fieldSize.y / 2));
                //�v���n�u�Ԃ̈ʒu���߂����Ȃ����`�F�b�N
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



            //���X�g���烉���_���ȃI�u�W�F�N�g��I��
            int randomIndex = Random.Range(0, objectSpawn.Count);
            GameObject objectToSpwan = objectSpawn[randomIndex];

            //�v���n�u�̍������擾���Đ����ʒu�𒲐��i���܂�Ȃ��悤�ɂ���)
            Collider objectCollider = objectToSpwan.GetComponent<Collider>();

            if (objectCollider != null)
            {

                //�v���n�u��Collider�̒�ӂ�Y���W�̒���
                randomposition.y = objectCollider.bounds.extents.y + spawnSenter.position.y;
            }

            //�I�u�W�F�N�g����
            GameObject spawnObject = Instantiate(objectToSpwan, randomposition, Quaternion.identity);

            if (objectCollider != null)
            {
                Vector3 spawnPos = spawnObject.transform.position;
                spawnPos.y = objectCollider.bounds.extents.y + spawnSenter.position.y;

                spawnObject.transform.position = spawnPos;
            }

            //�������ꂽ�ʒu�����X�g�ɒǉ�
            spawnPotisions.Add(randomposition);
        }
    }
}
