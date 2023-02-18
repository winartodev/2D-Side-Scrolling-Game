using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [Tooltip("Object that the camera want's to follow\nexample: player")]
    public Transform target;

    float _followSpeed;

    Transform _cameraPosition;

    public void Start() {
        _cameraPosition = GetComponent<Transform>();
        _followSpeed = 5f;
    }

    private void FixedUpdate() {
        Vector3 desirablePosition = new Vector3(target.position.x, target.position.y, _cameraPosition.position.z);
        Vector3 position = Vector3.Lerp(transform.position, desirablePosition, _followSpeed * Time.fixedDeltaTime);
        transform.position = position;
    }
}
