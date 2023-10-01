using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatapultScript : MonoBehaviour
{
    public static CatapultScript current;
    SpriteRenderer sprite;
    [SerializeField] Sprite[] sprites;
    private AudioSource launch;
    private AudioSource impact;

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        sprite = GetComponentInChildren<SpriteRenderer>();
        AudioSource[] audios = GetComponents<AudioSource>();
        launch = audios[0];
        impact = audios[1];
        //Fire();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Fire()
    {
        StartCoroutine("FireProccess");
    }

    private List<FloortileScript> CrossFire()
    {
        var middleFloorTile = GoblinScript.current.lastFloorTile;
        var column = middleFloorTile.column;
        var row = middleFloorTile.row;
        GoblinScript.current.lastFloorTile.SetAlarm(true);
        var tiles = FloorScript.current.tiles;
        List <FloortileScript> list = new List<FloortileScript>();
        list.Add(middleFloorTile);
        if (row - 1 >= 0) list.Add(tiles[row - 1][column]);
        if (row + 1 < tiles.Count) list.Add(tiles[row + 1][column]);
        if (column - 1 >= 0) list.Add(tiles[row][column-1]);
        if (column + 1 < tiles[0].Count) list.Add(tiles[row][column+1]);

        foreach(FloortileScript tile in list)
        {
            tile.SetAlarm(true);
        }

        return list;
    }

    private void Explosion(List<FloortileScript> tiles)
    {
        impact.Play();
        foreach (FloortileScript tile in tiles)
        {
            tile.SetDamage(true);
        }
    }

    private void Rest(List<FloortileScript> tiles)
    {
        foreach (FloortileScript tile in tiles)
        {
            tile.SetAlarm(false);
            tile.SetDamage(false);
            tile.SetBroken(true);
        }
    }

    IEnumerator FireProccess()
    {
        sprite.sprite = sprites[0];
        sprite.enabled = true;
        yield return new WaitForSeconds(2f);
        sprite.sprite = sprites[1];
        sprite.enabled = true;
        launch.Play();
        yield return new WaitForSeconds(2f);
        sprite.enabled = false;
        var tiles = CrossFire();
        yield return new WaitForSeconds(2f);
        Explosion(tiles);
        yield return new WaitForSeconds(0.1f);
        Rest(tiles);
    }
}
