using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkEnergyScript : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    SpriteRenderer sprite;
    FloortileScript floortile;
    float time = 3;
    float speed = 1.5f;
    bool active = true;
    private short row;
    private short col;
    private List<List<FloortileScript>> tiles;

    AudioSource thunderAudio;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        thunderAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!active) return;
        Move();
        if (time <= 0)
        {
            active = false;
            Explode();
        }
        if (Time.frameCount % 5 == 0)
        {
            sprite.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        time -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            floortile = collision.gameObject.GetComponent<FloortileScript>();
        }
    }

    private void Explode()
    {
        sprite.enabled = false;
        StartCoroutine(Explosions());
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, GoblinScript.current.transform.position, speed * Time.deltaTime);
    }

    private void NextWaveAlarm(int wave, bool alarm)
    {
        for (int i = row - wave; i <= row + wave; i++)
        {
            
            if (i < 0 || i > 6)
            {}
            else
            {
                if (col - wave >= 0)
                {
                    tiles[i][col - wave].SetAlarm(alarm);
                }
                if (col + wave <= 12)
                {
                    tiles[i][col + wave].SetAlarm(alarm);
                }
            }

        }
        for (int i = col - wave; i <= col + wave; i++)
        {
            if (i < 0 || i > 12)
            { }
            else
            {
                if (row - wave >= 0)
                {
                    tiles[row - wave][i].SetAlarm(alarm);
                }
                if (row + wave <= 6)
                {
                    tiles[row + wave][i].SetAlarm(alarm);
                }
            }
        }
    }

    private void NextWaveShock(int wave,bool alarm)
    {
        for (int i = row - wave; i <= row + wave; i++)
        {

            if (i < 0 || i > 6)
            { }
            else
            {
                if (col - wave >= 0)
                {
                    tiles[i][col - wave].SetShock(alarm);
                }
                if (col + wave <= 12)
                {
                    tiles[i][col + wave].SetShock(alarm);
                }
            }

        }
        for (int i = col - wave; i <= col + wave; i++)
        {
            if (i < 0 || i > 12)
            { }
            else
            {
                if (row - wave >= 0)
                {
                    tiles[row - wave][i].SetShock(alarm);
                }
                if (row + wave <= 6)
                {
                    tiles[row + wave][i].SetShock(alarm);
                }
            }
        }
    }

    IEnumerator Explosions()
    {
        if (floortile == null)
        {
            Destroy(gameObject);
            yield break;
        }
        row = floortile.row;
        col = floortile.column;
        tiles = FloorScript.current.tiles;

        tiles[row][col].SetAlarm(true);

        yield return new WaitForSeconds(1);
        tiles[row][col].SetAlarm(false);
        tiles[row][col].SetShock(true);
        thunderAudio.Play();
        yield return new WaitForSeconds(0.1f);
        tiles[row][col].SetShock(false);

        for (int i = 1; i < 5; i++)
        {
            NextWaveAlarm(i,true);
            yield return new WaitForSeconds(1);
            NextWaveAlarm(i,false);
            NextWaveShock(i,true);
            thunderAudio.Play();
            yield return new WaitForSeconds(0.1f);
            NextWaveShock(i, false);
        }

        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
