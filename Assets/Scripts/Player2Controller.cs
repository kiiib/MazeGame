using UnityEngine;
using System.Collections;

public class Player2Controller : MonoBehaviour
{

    public float MoveSpeed;
    float currentSpeed = 0;

    public Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            var vector = this.transform.forward * MoveSpeed;
            this.gameObject.transform.position += vector;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            var vector = this.transform.forward * MoveSpeed;
            this.gameObject.transform.position -= vector;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.gameObject.transform.Rotate(new Vector3(0, -1, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.gameObject.transform.Rotate(new Vector3(0, 1, 0));
        }

    }


}
