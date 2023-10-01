using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffScript : MonoBehaviour
{
    short type;
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        type = (short)Random.Range(0, 3);
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = sprites[type];
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            GoblinScript.current.Buff(type);
            Destroy(gameObject);
        }
    }
}
