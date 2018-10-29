using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class comptence : MonoBehaviour
{
    Vector3 mouse_pos = Vector3.zero;
    bool dashing = false;
    bool can_redash = true;
    Rigidbody rigid;
    float distance_to_get = 0.0f;
    public float speed = 2000.0f;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

	Vector3 position_when_dashing = Vector3.zero;
    void Update()
    {
        updateMousePosition();
        updaterotation();

        if (Input.GetMouseButtonDown(0) && !dashing)
        {
			position_when_dashing = mouse_pos;
            dash();
        }
        else if (Input.GetMouseButtonDown(1) && dashing && can_redash)
        {
            can_redash = false;
        }
		
		if(dashing)
		{
			gestion_collision();
		}
    }

    void dash()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        dashing = true;

        RaycastHit hit;
        Ray direction = new Ray(transform.position, position_when_dashing);
        if (Physics.Raycast(direction, out hit, Mathf.Infinity))
        {
            distance_to_get = (hit.point - transform.position).magnitude;
            Vector3 force_direction = new Vector3(mouse_pos.x - transform.position.x, 0, mouse_pos.z - transform.position.z).normalized;
            rigid.AddForce(force_direction * Time.deltaTime * speed, ForceMode.VelocityChange);
        }
    }
	void gestion_collision()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, position_when_dashing, out hit, Mathf.Infinity))
        {
            float detection = (hit.point - transform.position).magnitude;
            if (Mathf.Abs(detection) < 1f)
            {
                print("touching");
            }
        }
    }
    void updateMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mouse_pos = hit.point;
            mouse_pos.y = 0;
        }
    }

    void updaterotation()
    {
		transform.rotation = Quaternion.LookRotation(mouse_pos);
    }
}
