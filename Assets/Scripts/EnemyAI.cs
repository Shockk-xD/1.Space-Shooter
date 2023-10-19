using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform target;

    private void Start() {
        target = GameObject.Find("player").GetComponent<Transform>();
    }

    private void Update() {
        transform.Translate(0, ((target.transform.position.y > transform.position.y) ? 1 : -1) * Time.deltaTime, 0);
    }
}
