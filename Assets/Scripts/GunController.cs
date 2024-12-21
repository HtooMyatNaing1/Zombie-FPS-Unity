using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public GameObject muzzleFlashPrefab;
    public int bulletCount;

    GameObject bulletClone;
    GameObject muzzleFlashClone;

    Animator gunAnimator;

    float randomFlashRotation;
    float randomFlashScale;
    bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        gunAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ShootBullets();
    }

    void ShootBullets()
    {
        if(Input.GetMouseButtonDown(0) && bulletCount > 0 && isShooting == false)
        {
            gunAnimator.SetInteger("State", 1);
            StartCoroutine(SetGunIdle());

            isShooting = true;
            bulletCount -= 1;

            bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            GameObject.Destroy(bulletClone, 2);

            muzzleFlashClone = Instantiate(muzzleFlashPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            GameObject.Destroy(muzzleFlashClone, 0.03f);

            randomFlashRotation = Random.Range(-90, 90);
            muzzleFlashClone.transform.Rotate(0, 0, randomFlashRotation);

            randomFlashScale = Random.Range(1, 2.5f);
            muzzleFlashClone.transform.localScale = new Vector3(randomFlashScale, randomFlashScale, randomFlashScale);
        }
    }

    IEnumerator SetGunIdle()
    {
        yield return new WaitForSeconds(0.10f);
        gunAnimator.SetInteger("State", 0);
        isShooting = false;
    }
}
