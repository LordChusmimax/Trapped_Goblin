using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour, Shootable
{
    int hp = 10;
    [SerializeField] float speed = 1f;
    GoblinScript player;

    float frameTimeCount;
    [SerializeField] Sprite[] movementSprites;
    SpriteRenderer sprite;
    short currentAnimFrame = 0;

    AudioSource shoot, hit;

    float shootCd = 0.75f, shootTime=0;

    [SerializeField] GameObject bullet,buff;

    // Start is called before the first frame update
    void Start()
    {
        player = GoblinScript.current;
        sprite = GetComponent<SpriteRenderer>();
        AudioSource[] audios = GetComponents<AudioSource>();
        shoot = audios[0];
        hit = audios[1];
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        SetSprite();
        Actions();
    }

    private void Move()
    {
        var xDiference = transform.position.x - player.transform.position.x;

        transform.localScale = xDiference < 0 ? new Vector2(2, 2) : new Vector2(-2, 2);

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    private void SetSprite()
    {
        frameTimeCount += Time.deltaTime;
        if (frameTimeCount >= 0.2f)
        {
            currentAnimFrame += 1;
            frameTimeCount = 0;
        }
        if (currentAnimFrame >= movementSprites.Length) currentAnimFrame = 0;
        sprite.sprite = movementSprites[currentAnimFrame];
    }

    private void Actions()
    {
        if (shootTime>shootCd)
        {
            shootTime = 0;
            if (Random.Range(0, 20) == 0)
                Shoot();
            return;
        }
        shootTime += Time.deltaTime;
    }

    private void Shoot()
    {
        if (HudScript.dead) return;
        shoot.Play();
        var rotation = Vector2.SignedAngle(Vector2.right, (Vector2)player.transform.position - (Vector2)transform.position);
        var bullet = Instantiate(this.bullet, transform.position, Quaternion.Euler(0, 0, rotation));
        var bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = (Vector2)(Quaternion.Euler(0, 0, rotation) * Vector2.right) * 4;
    }

    public void Shot(int damage, bool enemy)
    {
        hit.Play();
        hp -= damage;
        if (hp <= 0)
        {
            SpawnerScript.points++;
            if (Random.Range(0, 5) == 0)
            {
                Instantiate(buff, transform.position, Quaternion.Euler(0, 0, 0));
            }
            Destroy(gameObject);
        }
            
    }
}
