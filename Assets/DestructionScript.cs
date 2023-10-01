using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var shootable = collision.gameObject.GetComponent<Shootable>();
        if (shootable != null)
        {
            shootable.Shot(1000, true);
        }
    }
}
