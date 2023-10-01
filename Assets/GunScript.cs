using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    PlayerInput inputAction;
    bool spray = false;
    bool automatic = true;
    float fireCd = 0;
    public float weaponCd = 0.50f;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform hole;

    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        inputAction = new PlayerInput();
        inputAction.Shoot.Enable();
        inputAction.Shoot.Shoot.performed += ShootPressed;
        inputAction.Shoot.Shoot.canceled += ShootReleased;
        audio = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        inputAction.Shoot.Disable();
        inputAction.Shoot.Shoot.performed -= ShootPressed;
        inputAction.Shoot.Shoot.canceled -= ShootReleased;
    }

    // Update is called once per frame
    void Update()
    {
        if (fireCd <= weaponCd)
            fireCd += Time.deltaTime;
        else
            if (spray && automatic)
                Shoot();
                
    }
    private void Shoot()
    {
        fireCd = 0;
        var bullet = Instantiate(this.bullet, hole.transform.position, Quaternion.Euler(0, 0, ArmScript.GetRotation()));
        var bulletRB = bullet.GetComponent<Rigidbody2D>();
        bulletRB.velocity = (Vector2)(Quaternion.Euler(0, 0, ArmScript.GetRotation()) * Vector2.right)* 10;
        audio.Play();
    }

    void ShootPressed(InputAction.CallbackContext action)
    {
        spray = true;
        if (fireCd > weaponCd)
            Shoot();
    }

    void ShootReleased(InputAction.CallbackContext action)
    {
        spray = false;
    }
}
