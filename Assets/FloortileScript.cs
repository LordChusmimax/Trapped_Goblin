using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloortileScript : MonoBehaviour
{
    public short row;
    public short column;

    private bool laserOn=false;

    private SpriteRenderer alarmSprite, laserSprite, brokenSprite, damageSprite, shockSprite;

    Collider2D brokenCollider,damageCollider,shockCollider;
    // Start is called before the first frame update
    void Start()
    {
        alarmSprite = GetComponentsInChildren<SpriteRenderer>()[1];
        laserSprite = GetComponentsInChildren<SpriteRenderer>()[2];
        brokenSprite = GetComponentsInChildren<SpriteRenderer>()[3];
        damageSprite = GetComponentsInChildren<SpriteRenderer>()[4];
        shockSprite = GetComponentsInChildren<SpriteRenderer>()[5];
        brokenCollider = brokenSprite.GetComponent<Collider2D>();
        damageCollider = damageSprite.GetComponent<Collider2D>();
        shockCollider = shockSprite.GetComponent<Collider2D>();

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!laserOn) return;
        var shootable = collision.gameObject.GetComponent<GoblinScript>();
        if (shootable != null)
        {
            shootable.Shot(1,true);
        }
    }

    public void SetAlarm(bool alarm)
    {
        alarmSprite.enabled = alarm;
    }

    public void SetLaser(bool alarm)
    {
        laserSprite.enabled = alarm;
        laserOn = alarm;
    }

    public void SetDamage(bool alarm)
    {
        damageCollider.enabled = alarm;
        damageSprite.enabled = alarm;
    }

    public void SetShock(bool alarm)
    {
        shockCollider.enabled = alarm;
        shockSprite.enabled = alarm;
    }

    public void SetBroken(bool alarm)
    {
        brokenSprite.enabled = alarm;
        brokenCollider.enabled = alarm;
    }

    public void LaserSprite(Sprite sprite)
    {
        laserSprite.sprite = sprite;
        laserSprite.transform.localScale = new Vector3(-1, 1, 1);
    }
}
