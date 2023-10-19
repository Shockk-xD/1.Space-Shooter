using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;

    private void Update() {
        transform.Translate(-speed * Time.deltaTime, 0, 0);
        if (transform.position.x < -26)
            transform.position = new Vector3(13, 3.27f, 0);
    }
}
