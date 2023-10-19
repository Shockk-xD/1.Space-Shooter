using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameController gm;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private Joystick joystick;
    [SerializeField] private GameObject textPrefab;

    private float _moveInput;
    private Rigidbody2D _rb;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        _moveInput = joystick.Vertical;
        _rb.velocity = new Vector2(0, _moveInput * speed);

        if (gm.players[0].transform.position.y > 6.71f) {
            gm.players[0].transform.position = new Vector3(-6.7f, -3.31f, 0);
            gm.players[1].transform.position = new Vector3(-6.7f, gm.players[0].transform.position.y + 10.0450005f, 0);
            gm.players[2].transform.position = new Vector3(-6.7f, gm.players[0].transform.position.y - 10.0450005f, 0);
        }
        if (gm.players[0].transform.position.y < -6.71f) {
            gm.players[0].transform.position = new Vector3(-6.7f, 3.335f, 0);
            gm.players[1].transform.position = new Vector3(-6.7f, gm.players[0].transform.position.y + 10.0450005f, 0);
            gm.players[2].transform.position = new Vector3(-6.7f, gm.players[0].transform.position.y - 10.0450005f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Friend")) {
            if (collision.gameObject.name == "score5(Clone)") gm.score += 10;
            else if (collision.gameObject.name == "score10(Clone)") gm.score += 20;
            else if (collision.gameObject.name == "score15(Clone)") gm.score += 30;
            gm.scoreText.text = "Счёт: " + gm.score;
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("Enemy")) {
            gm.lives--;
            gm.StartCoroutine(gm.Fading());
            gm.livesImage[2 - gm.lives].GetComponent<Image>().enabled = false;
            if (gm.lives == 0)
                gm.StopGame();
            Destroy(collision.gameObject);
        } else if (collision.gameObject.name == "Pistol Bullets(Clone)" && GameObject.Find("GunController").GetComponent<GunController>().pistolBullets < 91) {
            GameObject.Find("GunController").GetComponent<GunController>().AddBullets("pistol", Random.Range(5, 10));
            Destroy(collision.gameObject);
        } else if (collision.gameObject.name == "Staff Bullets(Clone)" && GameObject.Find("GunController").GetComponent<GunController>().staffBullets < 95) {
            GameObject.Find("GunController").GetComponent<GunController>().AddBullets("staff", 5);
            Destroy(collision.gameObject);
        } else if (collision.gameObject.name == "heart(Clone)" && gm.lives < 3) {
            gm.lives++;
            gm.livesImage[3 - gm.lives].GetComponent<Image>().enabled = true;
            Destroy(collision.gameObject);
        } else if (collision.gameObject.name == "lucky block(Clone)") {
            int lucky = Random.Range(0, 8);
            GameObject text = Instantiate(textPrefab, GameObject.Find("Canvas").transform);
            text.GetComponent<RectTransform>().localPosition = new Vector3(0, 380, 0);
            if (lucky == 1) {
                int randValue = Random.Range(50, 76);
                gm.score += randValue;
                text.GetComponent<Text>().text = $"+{randValue} очков";
            } else if (lucky == 2 && gm.lives < 3) {
                gm.lives++;
                text.GetComponent<Text>().text = "+1 дополнительная жизнь";
                gm.livesImage[3 - gm.lives].GetComponent<Image>().enabled = true;
            } else if (lucky == 3) {
                int pistolBullets = Random.Range(5, 11);
                int staffBullets = Random.Range(3, 7);
                GameObject.Find("GunController").GetComponent<GunController>().AddBullets("pistol", pistolBullets);
                GameObject.Find("GunController").GetComponent<GunController>().AddBullets("staff", staffBullets);
                text.GetComponent<Text>().text = $"+{pistolBullets} пулей для пистолета и +{staffBullets} для посоха";
            } else if (lucky == 4 && gm.GetComponent<ChangeScaleOverTime>().enabled) {
                StartCoroutine(StopChangeScale());
                text.GetComponent<Text>().text = "заморозка размера игрока на 10 секунд";
            } else if (lucky == 5 && gm.score > 50) {
                int randValue = Random.Range(50, 76);
                gm.score -= randValue;
                text.GetComponent<Text>().text = $"-{randValue} очков";
            } else if (lucky == 6 && gm.lives > 1) {
                gm.lives--;
                text.GetComponent<Text>().text = "-1 жизнь";
                gm.livesImage[2 - gm.lives].GetComponent<Image>().enabled = false;
            } else if (lucky == 7) {
                GameObject.Find("GunController").GetComponent<GunController>().reloadTimer = 20f;
                text.GetComponent<Text>().text = "блокировка стрельбы на 20 секунд";
            } else {
                text.GetComponent<Text>().text = "Unity в действии.\nМультиплатформенная разработка на C#";
                GameObject.Find("Unity In Action").GetComponent<Animator>().SetTrigger("Run");
            }
            Destroy(collision.gameObject);
        } else if (collision.gameObject.name == "Score Text Collider") {
            GameObject scoreText = GameObject.Find("Score Text");
            if (scoreText != null)
                scoreText.GetComponent<Animator>().SetBool("isFullAlpha", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.name == "Score Text Collider") {
            GameObject scoreText = GameObject.Find("Score Text");
            if (scoreText != null)
                scoreText.GetComponent<Animator>().SetBool("isFullAlpha", true);
        }
    }

    private IEnumerator StopChangeScale() {
        gm.GetComponent<ChangeScaleOverTime>().enabled = false;
        yield return new WaitForSeconds(10);
        gm.GetComponent<ChangeScaleOverTime>().enabled = true;
    }
}
