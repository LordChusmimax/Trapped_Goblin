using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicianScript : MonoBehaviour, Shootable
{
    int hp = 500;
    float rotation = 0;
    SpriteRenderer sprite;
    Transform spriteTransform;
    GoblinScript player;
    int speed = 4;

    public static MagicianScript current;

    [SerializeField] GameObject energyBall;
    [SerializeField] Transform hole;

    float ballTime=0, ballCd=3;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        spriteTransform = GetComponentsInChildren<Transform>()[1];
        player = GoblinScript.current;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Rotate();
        if (ballTime > ballCd)
        {
            SummonBall();
            ballTime = 0;
            return;
        }
        ballTime += Time.deltaTime;
    }

    private void Move()
    {
        var destination = new Vector3(player.transform.position.x + 3, player.transform.position.y, player.transform.position.z);

        if (Vector3.Distance(destination, transform.position) > 2) speed = 4;
        else speed = 2;

        transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
    }

    private void Rotate()
    {
        rotation += Time.deltaTime * 60;

        transform.rotation = Quaternion.Euler(0, 0, rotation);
        spriteTransform.localRotation = Quaternion.Euler(0, 0, -rotation);
    }

    private void SummonBall()
    {
        Instantiate(energyBall, hole.position, Quaternion.identity);
    }

    public void Shot(int damage, bool enemy)
    {
        if (enemy) return;
        if (damage > 10)
            hp -= 10;
        else
            hp -= damage;
        if (hp <= 0)
        {
            HudScript.current.Win();
        }
    }
}
