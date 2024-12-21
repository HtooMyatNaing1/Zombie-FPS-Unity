using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed;

    EnemyController hitEnemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            hitEnemy = other.GetComponent<EnemyController>();
            hitEnemy.DoDamageToZombie(50);
            GameObject.Destroy(gameObject);
        }
    }
}
