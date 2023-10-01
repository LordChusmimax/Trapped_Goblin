using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilEyeScript : MonoBehaviour, Shootable
{
    [SerializeField] Sprite[] sprites;
    [SerializeField] Sprite[] laserSprites;
    SpriteRenderer sprite;

    Collider2D collider;

    FloortileScript[] tiles;
    [SerializeField] bool vertical=true;
    [SerializeField] int position=0;
    bool laser;

    AudioSource prepare, fire, hit;

    int hp = 20;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider2D>();
        collider.enabled = false;
        int counter = 0;
        if (vertical)
        {
            tiles = new FloortileScript[7];
            foreach (LineScript line in FloorScript.current.lines)
            {
                tiles[counter] = line.floortiles[position];
                counter++;
            }
        }
        else
        {
            tiles = new FloortileScript[13];
            foreach (FloortileScript floortile in FloorScript.current.lines[position].floortiles)
            {
                tiles[counter] = floortile;
                counter++;
            }
        }
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;

        AudioSource[] audios = GetComponents<AudioSource>();

        prepare = audios[0];
        fire = audios[1];
        hit = audios[2];
    }

    // Update is called once per frame
    void Update()
    {
        if (laser && Time.frameCount % 15==0)
        {
            int counter = 0;
            foreach (FloortileScript tile in tiles)
            {
                if (counter == 0)
                {
                    tile.LaserSprite(laserSprites[Random.Range(0, 4)]);
                }
                else
                {
                    tile.LaserSprite(laserSprites[Random.Range(4,laserSprites.Length)]);
                }
                counter++;
            }
        }
    }

    public void Shot(int damage, bool enemy)
    {
        hit.Play();
        hp -= damage;
        if (hp <= 0) Destroy();
    }

    private void Destroy()
    {

        prepare.Stop();
        fire.Stop();
        sprite.enabled = false;
        collider.enabled = false;
        foreach (FloortileScript tile in tiles)
        {
            tile.SetAlarm(false);
            tile.SetLaser(false);
        }
    }

    public void Show()
    {
        hp = 20;
        sprite.enabled = true;
        sprite.sprite = sprites[0];
        Invoke("Activate", 2);
        foreach (FloortileScript tile in tiles)
        {
            tile.SetAlarm(true);
        }
    }

    public void Activate()
    {
        prepare.Play();
        sprite.sprite = sprites[1];
        Invoke("Fire", 2);
        collider.enabled = true;
    }

    private void Fire()
    {

        if (hp <= 0) return;
        prepare.Stop();
        fire.Play();
        sprite.sprite = sprites[2];
        foreach (FloortileScript tile in tiles)
        {
            tile.SetAlarm(false);
            tile.SetLaser(true);
        }
        laser = true;
        int counter = 0;
        foreach (FloortileScript tile in tiles)
        {
            if (counter == 0)
            {
                tile.LaserSprite(laserSprites[0]);
            }
            else
            {
                tile.LaserSprite(laserSprites[Random.Range(1, laserSprites.Length)]);
            }
            counter++;
        }
    }
}
