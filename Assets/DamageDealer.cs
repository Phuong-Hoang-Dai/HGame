using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class DamageDealer : MonoBehaviour
{
    float weaponLength = 0.6f;
    int attack = 1;

    public void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapBox(transform.position, Vector3.one * (weaponLength / 2), transform.rotation, 1 << 6);

        foreach (Collider enemy in hitEnemies)
        {
            TakeDamage hitEnemy = enemy.GetComponentInChildren<TakeDamage>();
            if(hitEnemy != null)
            {
                hitEnemy.TakeDmg(attack);
                Debug.Log("Enemy is hit.");
            }
            else
            {
                Debug.Log("Enemy not found.");
            }
        }
    }
  
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan; 
        Gizmos.DrawWireCube(transform.position, Vector3.one * weaponLength);
    }
}
