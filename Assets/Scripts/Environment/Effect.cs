using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public GameObject DestroyEffect;

    public void Destroy()
    {
        if(DestroyEffect != null)
            Instantiate(DestroyEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
