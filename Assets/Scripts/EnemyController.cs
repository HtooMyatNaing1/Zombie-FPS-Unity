using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    NavMeshAgent myAgent;

    private PlayerController player; // Changed to private
    public Animator myAnimator;
    float distanceFromPlayer;
    Vector3 rotationVector;
    public int damage;
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();

        // This Condition Check is Testing...
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<PlayerController>();
            }
            else
            {
                Debug.LogError("PlayerController not found in the scene. Ensure your player is tagged as 'Player'.");
            }
        }
        // To this
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // The second condition check is testing
        if(health > 0 && player != null)
        {
            distanceFromPlayer = Vector3.Distance(transform.position, player.gameObject.transform.position);
            if (distanceFromPlayer > 3)
            {
                myAnimator.SetInteger("State", 0);

                myAgent.isStopped = false;
                myAgent.SetDestination(player.gameObject.transform.position);
            }
            else
            {
                myAnimator.SetInteger("State", 2);
                myAgent.isStopped = true;

                transform.LookAt(player.gameObject.transform.position);
                rotationVector = transform.eulerAngles;
                rotationVector.x = 0;
                transform.eulerAngles = rotationVector;
            }
        }
    }

    public void AttackPlayerEvent()
    {
        // This condition check is testing
        if (player != null)
        {
            player.DoDamageToPlayer(damage);
        }
        
    }

    public void DoDamageToZombie(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            myAnimator.SetInteger("State", 3);
            myAgent.isStopped = true;
        }
    }

    public void ClearEnemyEvent()
    {
        GameObject.Destroy(gameObject);
        // The condition check is testing
        if(player != null)
        {
            player.killCount += 1;
        }
    }
}
