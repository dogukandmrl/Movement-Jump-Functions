using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponControl : MonoBehaviour
{ public LayerMask obstacleLayer;
    public Vector3 offset;
    RaycastHit hit;

    public GameObject bullet;
    public Transform firepoint;
    // Start is called before the first frame update
    private void Update()
    { 
        //Look
        if (Physics.Raycast(Camera.main.transform.position,Camera.main.transform.forward, out hit,Mathf.Infinity,obstacleLayer))
        {
            transform.LookAt(hit.point);
            transform.rotation *= Quaternion.Euler(offset);

        }
        //Fire
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, firepoint.position, transform.rotation*Quaternion.Euler(90,0,0));
        }
    }
}
