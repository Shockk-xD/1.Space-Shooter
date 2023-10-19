using UnityEngine;
using UnityEngine.UI;

public class SecondChmonyaController : MonoBehaviour
{
    private void Update() {
        transform.Translate(-5 * Time.deltaTime, 0, 0);
        if (transform.position.x < -13)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.name == "player") {
            GameController gm = GameObject.Find("GameController").GetComponent<GameController>();
            gm.lives--;
            gm.StartCoroutine(gm.Fading());
            gm.livesImage[2 - gm.lives].GetComponent<Image>().enabled = false;
            if (gm.lives == 0)
                gm.StopGame();
            Destroy(gameObject);
        }
    }
}
