using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private void Update()
    {
        if (Vector2.Distance(transform.position, GoblinScript.current.transform.position) > 30)
            Destroy(gameObject);
    }
    [SerializeField] public int damage = 3;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var shootable = collision.gameObject.GetComponent<Shootable>();
        if (shootable != null)
        {
            shootable.Shot(damage,false);
        }
        Destroy(gameObject);
    }
}
