using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimnControlltrer : MonoBehaviour
{
    public PlayerController player;
    private void Awake()
    {
        player = transform.parent.GetComponent<PlayerController>();
    }
    public void Attack()
    {
        player.Attack();
    }

}
