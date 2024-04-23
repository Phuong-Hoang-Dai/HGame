using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    EnemyController enemy;
    private void Awake()
    {
        enemy = transform.parent.GetComponent<EnemyController>();
    }
    public void TakeDmg(int damage)
    {
        if(enemy.health > 0)
        {
            enemy.health -= damage;
            Debug.Log($"Enemy's health is {enemy.health}.");
        }
    }
}
