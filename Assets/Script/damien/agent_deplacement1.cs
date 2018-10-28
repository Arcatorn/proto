using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class agent_deplacement1 : MonoBehaviour
{
    Vector3 lastMoveDirection;
    private Rigidbody rigid;
    public float speed = 100;
    Transform mainCamera;
    public Camera cam;


    public CharacterController2 comp;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        comp = GetComponent<CharacterController2>();
    }
    void Start()
    {
        mainCamera = cam.transform;
    }
    void Update()
    {
        Vector3 position = moves_detection();

        if (!comp.dashing && comp.ended) Move(position);

    }
    public static Vector3 moves_detection()
    {
        var vector = Vector3.zero;
        float deadzone = 0.25f;
        Vector2 stickInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (stickInput.magnitude < deadzone)
            stickInput = Vector2.zero;
        else
            stickInput = stickInput.normalized * ((stickInput.magnitude - deadzone) / (1 - deadzone));

        vector = new Vector3(stickInput.x,0,stickInput.y);
        return vector;
    }

    public void Move(Vector3 direction)
    {
        //Debug.Log ("Direction of the player based on the camera" + direction);
        //var mainCamera = Camera.main.transform;
        var cameraAngle = mainCamera.rotation;
        mainCamera.eulerAngles = new Vector3(0, mainCamera.eulerAngles.y, 0);
        direction = mainCamera.TransformDirection(direction);
        mainCamera.rotation = cameraAngle;
        rigid.velocity = direction * speed;
        this.lastMoveDirection = direction;
    }

}
