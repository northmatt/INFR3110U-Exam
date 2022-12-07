using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public static CameraController instance;
    public bool invertControllers = false;
    public float moveSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null) {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        int input = (Input.GetKey(KeyCode.S) ? 1 : 0) - (Input.GetKey(KeyCode.W) ? 1 : 0);

        if (invertControllers) {
            if (input == 1)
                input = -1;
            if (input == -1)
                input = 1;
        }

        transform.Rotate(moveSpeed * input * Time.deltaTime, 0f, 0f);
    }
}
