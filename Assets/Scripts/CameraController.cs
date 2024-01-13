using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    [SerializeField] private Vector3 transOffset;
    [SerializeField] private float speed;
    [SerializeField] private float maxDistance;
    //private RaycastHit hit;
    //[SerializeField] private Vector3 rotatOffset;

    private void Update()
    {
        //CameraTarget();
        BeyondTarget();
    }

    private void CameraTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position + transOffset, speed);
    }

    private void BeyondTarget()
    {
        //Debug.DrawRay(transform.position, transform.forward * Vector3.Distance(transform.position, target.position), Color.red, 0.3f);
        bool h = false;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, Vector3.Distance(transform.position, target.position)))
        {
            //맞았을때 실행할 코드
            //Destroy(hit.collider.gameObject);
            if (hit.collider != null && hit.collider.gameObject.CompareTag("Wall"))
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = false;
                h = true;
                //Material m = hit.collider.gameObject.GetComponent<MeshRenderer>().material;
                //m.color = new Color(m.color.r, m.color.g, m.color.b, 0.5f);
            }
        }
        //else if (!h && hit.collider.gameObject.tag == "Wall")
        //{
        //    hit.collider.gameObject.GetComponent<MeshRenderer>().enabled = true;
        //    h = false;
        //    //hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1, 1);
        //}
    }
}
