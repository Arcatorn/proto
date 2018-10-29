using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2 : MonoBehaviour
{
    bool correction = false;
    Rigidbody rigid;
    Vector3 position_quand_rebond;
    public bool dashing = false;
    float distance = 0.0f;
    public float speed;
    GameObject head;
    public bool ended = true;
    Vector3 mouse_pos = Vector3.zero;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        head = transform.GetChild(0).gameObject;
    }
    void Update()
    {
        var player_rot = transform.eulerAngles;
        // print(player_rot.y);
        /* if (player_rot.y < 0)
        {
            transform.Rotate(0, (180 + (180 + transform.rotation.y)), 0);
            print(player_rot.y);
        }
        if (transform.rotation.y > 360) transform.Rotate(0, -360, 0);*/
        RaycastHit hit;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mouse_pos = hit.point;
            mouse_pos.y = 0;
        }

        if (Input.GetMouseButtonDown(0) && !dashing)
        {
            dash(mouse_pos);
        }
        if (Input.GetMouseButtonDown(1) && dashing && !correction)
        {
            dash(mouse_pos);
            correction = true;
        }
        if (dashing)
        {
            gestion_collision();
        }
        else if (!ended)
        {
            stopdash();
        }
        else transform.rotation = Quaternion.LookRotation(mouse_pos);
    }

    Vector3 mousepos_when_dash = Vector3.zero;
    void dash(Vector3 mousepos)
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        dashing = true;
        mousepos_when_dash = mousepos;

        RaycastHit hit;

        Ray direction = new Ray(transform.position, mousepos);
        if (Physics.Raycast(direction, out hit, Mathf.Infinity))
        {
            distance = (hit.point - transform.position).magnitude;
            Vector3 force_direction = new Vector3(mouse_pos.x - transform.position.x, 0, mouse_pos.z - transform.position.z).normalized;
            rigid.AddForce(force_direction * Time.deltaTime * speed, ForceMode.VelocityChange);
        }
    }

    void gestion_collision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, mousepos_when_dash, out hit, Mathf.Infinity))
        {
            float detection = (hit.point - transform.position).magnitude;
            if (Mathf.Abs(detection) < 1f)
            {
                //rotate(hit, hit.point);
                rebond(mouse_pos);
            }
        }
    }
    void rebond(Vector3 mousepos)
    {
        RaycastHit hit;
        if (Physics.Raycast(head.transform.position, head.transform.forward, out hit, Mathf.Infinity))
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            dashing = true;


            Ray direction = new Ray(transform.position, mousepos);
            if (Physics.Raycast(direction, out hit, Mathf.Infinity))
            {
                distance -= (hit.point - transform.position).magnitude;
                Vector3 force_direction = new Vector3(mouse_pos.x - transform.position.x, 0, mouse_pos.z - transform.position.z).normalized;
                rigid.AddForce(force_direction * Time.deltaTime * speed, ForceMode.VelocityChange);
            }
            ended = false;
        }
    }

    void stopdash()
    {
        float test = (position_quand_rebond - transform.position).magnitude;
        if ((Mathf.Abs(distance - test) < 0.2f))
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
            ended = true;
        }
    }
}

/*void rotate(RaycastHit hit, Vector3 point)
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        position_quand_rebond = transform.position;

        var posplayer = transform.position;
        // var pos1 = new Vector3(hit.transform.position.x - (hit.transform.localScale.x / 2), hit.transform.position.y, hit.transform.position.z - (hit.transform.localScale.z / 2));
        //   var pos2 = new Vector3(hit.transform.position.x + (hit.transform.localScale.x / 2), hit.transform.position.y, hit.transform.position.z + (hit.transform.localScale.z / 2));
        var pos1 = new Vector3(hit.transform.position.x - (hit.transform.localScale.x / 2), hit.transform.position.y, hit.transform.position.z - (hit.transform.localScale.z / 2));
        var pos2 = new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z);

        var hypo = new Vector3(pos2.x - pos1.x, pos2.y - pos1.y, pos2.z - pos1.z).magnitude;
        var adjacent = new Vector3(posplayer.x - pos1.x, posplayer.y - pos1.y, posplayer.z - pos1.z).magnitude;

        // var cote1 = pos1 - posplayer;
        //var cote2 = pos2 - posplayer;
        //var normal = Vector3.Cross(pos1, pos2).normalized;
        //print(Vector3.Angle(transform.position,hit.normal));
        var a = adjacent / hypo;
        print(a * Mathf.Rad2Deg);
        var player_rot = transform.eulerAngles;
        //  print(player_rot.y);
        player_rot.y += 45;
        transform.rotation = Quaternion.Euler(player_rot);
        //print(player_rot.y);

    }*/
