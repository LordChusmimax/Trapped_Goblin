using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GoblinScript : MonoBehaviour, Shootable
{
    PlayerInput inputAction;
    Rigidbody2D rb;
    [SerializeField] Sprite[] movementSprites;
    GunScript gun;
    SpriteRenderer sprite;
    short currentAnimFrame = 0;
    [SerializeField] float speed = 1f;
    Vector2 movement;
    State state;

    public int hp = 20, maxHp = 20;

    public FloortileScript lastFloorTile;

    static public GoblinScript current;

    float frameTimeCount;

    [SerializeField]BulletScript GoblinBullets;

    [SerializeField] TextMeshProUGUI hpText, maxHpText;

    AudioSource buffAudio,hitAudio;
    

    enum State : byte
    {
        Idle,
        Moving,
        Paused,
        Stunned
    }

    // Start is called before the first frame update
    void Start()
    {
        current = this;
        state = State.Idle;
        inputAction = new PlayerInput();
        inputAction.Player.Enable();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        gun = GetComponentInChildren<GunScript>();
        GoblinBullets.damage = 3;
        AudioSource[] audios = GetComponents<AudioSource>();
        buffAudio = audios[0];
        hitAudio = audios[1];
    }

    private void FixedUpdate()
    {
        if (state != State.Paused)
        {
            movement = inputAction.Player.Move.ReadValue<Vector2>().normalized;
            if (movement.Equals(Vector2.zero))
                state = State.Idle;
            else
                state = State.Moving;
            rb.position += movement * Time.fixedDeltaTime * speed;
        }
    }
    // Update is called once per frame
    void Update()
    {
        SetHpText();
        SetSprite();
    }
    private void SetHpText()
    {
        hpText.text = hp.ToString();
        maxHpText.text ="/" + maxHp.ToString();
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
        if (state == State.Idle)
            sprite.sprite = movementSprites[0];
        else if (state == State.Moving)
            sprite.sprite = movementSprites[currentAnimFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {
            lastFloorTile = collision.gameObject.GetComponent<FloortileScript>();
        }
    }

    public void Shot(int damage, bool enemy)
    {
        hitAudio.Play();
        hp-=damage;
        if (hp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        HudScript.current.Kill();
    }

    public void Buff (int type)
    {
        buffAudio.Play();
        switch (type)
        {
            case 0:
                GoblinBullets.damage+=2;
                break;
            case 1:
                gun.weaponCd*=0.85f;
                break;
            case 2:
                hp += maxHp/3;
                maxHp += 5;
                if (hp>maxHp) hp = maxHp;
                break;
        }
    }
}
