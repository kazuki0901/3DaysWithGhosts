using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunManeger : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject bullet;
    [SerializeField] AudioClip shootAudio;
    [SerializeField] AudioClip reloadAudio;

    [Header("GunProperty")]
    [SerializeField] float shootForce;              // 発射の威力
    [SerializeField] float timeBetweenShooting;     // 発射レート（連射武器用）
    [SerializeField] float timeBetweenShots;        // 発射レート（ショットガン用）
    [SerializeField] float spread;                  // 散弾具合（ショットガン用）
    [SerializeField] float reloadTime;              // リロード時間
    [SerializeField] int magazineSize;              // マガジンのサイズ
    [SerializeField] int bulletsPerTap;             // 一発当たりの弾の数
    [SerializeField] bool allowButtonHold;          //連射武器か単発武器かのフラグ



    private int magazineCount;//UI用

    [SerializeField] LayerMask ignoreLayer;         // 無視していいレイヤー

    GameObject playerCam;

    int bulletsShot;
    int bulletsLeft;

    bool shooting;
    bool readyToShoot;

    public bool reloading;
    public bool allowInvoke = true;

    public int MagazineCount
    {
        get { return this.magazineCount; }
        private set { this.magazineCount = value; }

    }


    void Start()
    {
        playerCam = GameObject.Find("Main Camera");

        bulletsLeft = magazineSize;
        readyToShoot = true;

        magazineCount = magazineSize;//UI用


    }

    void Update()
    {
        if(Time.timeScale != 0){
            InputHandler();
            //生成されてから一定時間で弾が消えるようにする


        }

    }

    // 入力制御
    private void InputHandler()
    {
        // 長押しできるかできないかで入力処理を切り替える
        if (allowButtonHold)
            shooting = Input.GetMouseButton(0);
        else
            shooting = Input.GetMouseButtonDown(0);

        // 打てる状態なのかをチェック
        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            bulletsShot = 0;

            Shoot();
            magazineCount--;
        }

        // リロードする
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();

        }

        // 弾がなくなったら自動的にリロード
        if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            Reload();
            Debug.Log("自動リロード");

        }
    }

    // 弾を発射
    private void Shoot()
    {
        readyToShoot = false;

        // 画面の中央にレイを飛ばす
        Ray ray = playerCam.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit, 1000f, ~ignoreLayer))// レイが何かに当たったかをがチェック
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(10);// 何にも当たらなかったらレイの長さを強制決定

        // 銃口から見たターゲットの方向を取得
        Vector3 directionWithoutSpread = targetPoint - shootPoint.position;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        // 散弾する銃の銃口から見たターゲットの方向を取得
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);



        // 弾を生成
        GameObject currentBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);

        //弾を生成したとき発砲音を鳴らす
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = shootAudio;
        audioSource.Play();

    

        // 弾を前方に向かせる
        currentBullet.transform.forward = directionWithSpread.normalized;

        // 弾に力を加える
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);

        bulletsLeft--;
        bulletsShot++;

        // 弾と弾に間隔を開ける
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShooting);
            allowInvoke = false;
        }
        // 一度に出す弾
        if (bulletsShot < bulletsPerTap && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }



    }

    // 撃てる状態にする
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    // リロード処理
    private void Reload()
    { //弾を生成したとき発砲音を鳴らす
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.clip = reloadAudio;
        audioSource.Play();


        reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
        Debug.Log("リロード時間" + reloadTime);
    }

    // リロード状態を終了
    private void ReloadFinished()
    {

        bulletsLeft = magazineSize;
        reloading = false;
        //マガジンの残弾数を最大に戻す
        magazineCount = magazineSize;
    }
}
