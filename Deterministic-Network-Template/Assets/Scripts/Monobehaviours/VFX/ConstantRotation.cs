using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour {

    public Vector3 rotationVector;

    float initialYPosition;

    void Start() {
        initialYPosition = transform.localPosition.y;
    }

    void Update() {
        transform.Rotate(rotationVector * Time.deltaTime);

        //transform.position = new Vector3(transform.position.x, transform.parent.position.y + initialYPosition + Mathf.Sin(Time.time * 10f) * 0.2f, transform.position.z);
    }

}