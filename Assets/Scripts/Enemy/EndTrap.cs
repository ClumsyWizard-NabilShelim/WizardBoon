using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrap : MonoBehaviour
{
    public void KillPlayer()
    {
        FindObjectOfType<PlayerStats>().Damage(100);
    }
}
