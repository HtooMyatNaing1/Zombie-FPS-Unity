using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text healthText;
    public Text bulletCount;
    public Text scoreText;

    public PlayerController player;
    public GunController gun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.health + "/" + player.maxHealth;
        scoreText.text = "Kills : " + player.killCount;
        bulletCount.text = gun.bulletCount.ToString();
    }
}
