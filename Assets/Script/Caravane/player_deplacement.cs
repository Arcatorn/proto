using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class player_deplacement : MonoBehaviour
{
    Vector3 posToTest = Vector3.zero;
    Vector3 mouse_position = Vector3.zero;
    Rigidbody rigid;
    bool moving = false;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        updateMousePosition();

        if (Input.GetMouseButtonDown(0))
        {
            posToTest = move(mouse_position);
        }
	/*print((posToTest - transform.position).magnitude);
        if (moving)
        {
            if ((posToTest - transform.position).magnitude < 1f)
            {
                moving = false;
                rigid.angularVelocity = Vector3.zero;
                rigid.velocity = Vector3.zero;
            }
        }*/
    }

    Vector3 move(Vector3 mouse_pos_at_this_moment)
    {
        rigid.angularVelocity = Vector3.zero;
        rigid.velocity = Vector3.zero;

        var destinationVector = (mouse_pos_at_this_moment - transform.position).normalized;
        rigid.AddForce(destinationVector * 1000 * Time.deltaTime, ForceMode.Impulse);
        moving = true;
        return mouse_pos_at_this_moment;
    }

    void updateMousePosition()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            mouse_position = hit.point;
        }
    }

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "walls")
		{
			print("WTF CA MARCHE AS");
			transform.parent = null;
			transform.position = new Vector3(transform.position.x, 2, transform.position.z);
		}
	}
}
