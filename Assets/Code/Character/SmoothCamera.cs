using UnityEngine;

public class SmoothCamera : MonoBehaviour {
    public Camera camera;

    public float dampTime = 0.15f;
    public Transform target;

    private Vector3 velocity = Vector3.zero;

    // Update is called once per frame
    private void Update() {
        if (target) {
            var point = camera.WorldToViewportPoint(target.position);
            var delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.2f, point.z));
            var destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
        }
    }
}