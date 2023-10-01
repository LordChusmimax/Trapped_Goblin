using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoskitoScript : MonoBehaviour
{

    GoblinScript player;
    SpriteRenderer sprite;
    [SerializeField] float speed = 0.5f;
    bool orbiting = false;

    [SerializeField] GameObject bullet, buff;

    AudioSource shoot;
    // Start is called before the first frame update
    void Start()
    {
        player = GoblinScript.current;
        sprite = GetComponentInChildren<SpriteRenderer>();
        AudioSource[] audios = GetComponents<AudioSource>();
        shoot = audios[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (orbiting) return;
        Move();
    }

    public void Shoot()
    {
        if (HudScript.dead) return;
        shoot.Play();
        var rotation = Vector2.SignedAngle(Vector2.right, (Vector2)player.transform.position - (Vector2)sprite.transform.position);
        var bullet = Instantiate(this.bullet, sprite.transform.position, Quaternion.Euler(0, 0, rotation));
        var bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = (Vector2)(Quaternion.Euler(0, 0, rotation) * Vector2.right) * 3;
    }

    private void Move()
    {
        var xDiference = sprite.transform.position.x - player.transform.position.x;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (xDiference < 0.3) Orbit();
    }

    private void Orbit()
    {
        orbiting = true;
        transform.SetParent(OrbitScript.current.transform);
        transform.localPosition = Vector3.zero;
        OrbitScript.current.Moskitos.Add(transform);
    }

    public void DesOrbit()
    {
        SpawnerScript.points += 2;
        OrbitScript.current.Moskitos.Remove(transform);
        if (Random.Range(0, 7) == 0)
        {
            Instantiate(buff, transform.position, Quaternion.Euler(0, 0, 0));
        }
        Destroy(gameObject);
    }
}
