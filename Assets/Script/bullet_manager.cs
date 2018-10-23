using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_manager : MonoBehaviour
{

    void OnCollisionEnter()
    {
        Destroy(this.gameObject);
    }
}
