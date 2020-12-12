using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereBehaviourScript : MonoBehaviour
{
    // initial speed
    public float speed = 1;

    private Rigidbody rb;

    private GameObject parentGameObject;

    // Start is called before the first frame update
    void Start()
    {
        // obtatin Rigid Body
        rb = this.GetComponent<Rigidbody>();
        rb.isKinematic = true;

        // obtain avatar
        parentGameObject = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
        {
            // start co-routine
            StartCoroutine("WaitCoRoutine");
        }
    }

    IEnumerator WaitCoRoutine() {

        // waiting throw motion
        float waitSecond = 2.0f;
        yield return new WaitForSeconds(waitSecond);

        // set parent to avatar
        transform.parent = parentGameObject.transform;

        parentGameObject = transform.parent.gameObject;
        Debug.Log("parent: " + parentGameObject.name);
        Debug.Log("parent.position: " + parentGameObject.transform.position);

        // set position
        float angleY = parentGameObject.transform.eulerAngles.y * Mathf.Deg2Rad;
        rb.transform.position = parentGameObject.transform.position
            + new Vector3(0.6f * Mathf.Cos(angleY), 1.2f, 0.6f * Mathf.Sin(angleY));
        Debug.Log("parent.position: " + parentGameObject.transform.position + ", " + "rb.position: " + rb.position);

        //        Debug.Log("parent.eulerAngles: " + parentGameObject.transform.eulerAngles);

        // calculate force
        Vector3 force = new Vector3(5.0f * Mathf.Sin(angleY), 10.0f, 5.0f * Mathf.Cos(angleY)) * speed;
        // add force
        rb.AddForce(force);

        // enable kinematic
        rb.isKinematic = false;

        // wait and release parent
        waitSecond = 5.0f;
        yield return new WaitForSeconds(waitSecond);

        transform.parent = null;
    }
}
