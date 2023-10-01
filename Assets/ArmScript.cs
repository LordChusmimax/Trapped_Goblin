using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmScript : MonoBehaviour
{
    PlayerInput inputAction;
    private Camera cam;
    static float rotation;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        inputAction = new PlayerInput();
        inputAction.Aim.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        rotation = Vector2.SignedAngle(Vector2.right, (Vector2)cam.ScreenToWorldPoint(inputAction.Aim.Aim.ReadValue<Vector2>()) - (Vector2)transform.position);
        if (rotation < 90 && rotation > -90)
        {
            transform.parent.localScale = new Vector2(1, 1);
            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
        else
        {
            transform.parent.localScale = new Vector2(-1, 1);
            transform.rotation = Quaternion.Euler(0, 0, rotation + 180);
        }

    }

    public static float GetRotation()
    {
        return rotation;
    }
}
