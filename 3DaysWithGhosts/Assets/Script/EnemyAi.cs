using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject shootPoint;

    [SerializeField] Transform target;//プレイヤー（標的）のTransform

    [SerializeField] float shootTimer;//発射間隔
    [SerializeField] float speed;//弾の速度
    [SerializeField] float chaseRange;//追いかける範囲
    [SerializeField] float patrolRange;//ランダムに移動する範囲
    [SerializeField] float patrolWaitTime;//移動した後の待機時間

    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool isChasing;
    private bool isPatrolling;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        agent = GetComponent<NavMeshAgent>();
        originalPosition = transform.position;
        StartPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(target.position, transform.position);

        if (distanceToPlayer <= chaseRange)
        {
            //プレイヤーを検知した場合追跡、攻撃する
            ChasePlayer();
            Look();
            //Shoot();
        }
        else
        {
            //プレイヤーを見失った場合、ランダムに移動する
            if (!isPatrolling)
            {
                StartPatrol();
            }
        }


    }

    void Look()
    {
        transform.LookAt(target.transform);

    }

    void Shoot()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer >= 1)
        {
            shootTimer = 0;
            GameObject shootBullet = Instantiate(bullet, shootPoint.transform.position, Quaternion.identity);
            Rigidbody rb = shootBullet.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * speed);
        }
    }
    void ChasePlayer()
    {
        isChasing = true;
        isPatrolling = false;
        //プレイヤーの位置を目的地に設定
        agent.SetDestination(target.position);
    }

    void StartPatrol()
    {
        isChasing = false;
        isPatrolling = true;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (isPatrolling)
        {
            //ランダムな地点を設定
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
            randomDirection += originalPosition;
            NavMeshHit hit;

            NavMesh.SamplePosition(randomDirection, out hit, chaseRange, 1);
            Vector3 finalPosition = hit.position;

            //ランダム地点に移動を開始
            agent.SetDestination(finalPosition);

            //移動が完了するか、プレイヤーを発見するまで待機
            while (agent.remainingDistance > agent.stoppingDistance && !isChasing)
            {
                yield return null;
            }

            yield return new WaitForSeconds(patrolWaitTime);
        }
    }

    public void SetTarget(Transform transform)
    {
        target = transform;
      
    }
    private void OnCollisionEnter(Collision collision)
    {
       
        // プレイヤーに体当たりする
        if (gameObject.CompareTag("Enemy") && collision.gameObject.CompareTag("Player"))
        {
          
            PlayerHealth playerHealth = collision.gameObject.GetComponentInChildren<PlayerHealth>();
            
         
            if (playerHealth != null)
            {

                playerHealth.TakeDamage(10);
               

            }

        }
    }
 
}
