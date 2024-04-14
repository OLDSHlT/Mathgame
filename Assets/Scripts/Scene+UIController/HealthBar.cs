using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    //public TMP_Text healthText;
    Slider healthbar;
    Damageable playerdamageable;
    public GameObject player;
    // Start is called before the first frame update
    private void Awake()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        //if (player == null)
        //    Debug.Log("HealthBar脚本没有找到Tag为'Player'的物体");
        playerdamageable=player.GetComponent<Damageable>();
        healthbar=GetComponent<Slider>();
    }
    void Start()
    {
        healthbar.value = CurrenSliderPerent(playerdamageable.Health , playerdamageable.maxHealth);
        //healthText.text = playerdamageable.Health + "/" + playerdamageable.maxHealth;
    }

    private float CurrenSliderPerent(float Health,float maxHealth)
    {
        return Health / maxHealth;
    }

    // Update is called once per frame
    private void OnEnable()
    {
        playerdamageable.healthchanged.AddListener(OnPlayerHealthChange);
    }
    private void OnDisable()
    {
        playerdamageable.healthchanged.RemoveListener(OnPlayerHealthChange);
    }
    private void OnPlayerHealthChange(int newHealth,int maxHealth)
    {
        healthbar.value = CurrenSliderPerent(newHealth, maxHealth);
        //healthText.text = newHealth + "/" + maxHealth;
    }
    private void OnPlayerMaxHealthChange()
    {
        healthbar.value = CurrenSliderPerent(playerdamageable.Health, playerdamageable.maxHealth);
        //healthText.text = playerdamageable.Health + "/" + playerdamageable.maxHealth;
    }
}
