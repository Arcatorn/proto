using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody rigidbody;
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward);
        if (Input.GetKeyDown(KeyCode.A))
        {
            dash();
        }
        if (dashing)
        {
            rebond();
        }
    }

    bool dashing = false;
    float distance = 0.0f;
    void dash()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            distance = (hit.transform.position - transform.position).magnitude;
            dashing = true;
            rigidbody.AddForce((this.transform.position - this.transform.forward) * 20, ForceMode.VelocityChange);
        }

    }

    void rebond()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, -transform.forward, out hit, Mathf.Infinity))
        {
            float detection = (hit.transform.position - transform.position).magnitude;
            if (Mathf.Abs(detection) < 0.3f)
            {
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero;

                transform.Rotate(0, 135, 0);
                rigidbody.AddForce((this.transform.position - this.transform.forward) * 200, ForceMode.VelocityChange);
                print("ok");
                dashing = false;
            }
        }


    }
}
