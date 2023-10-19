using UnityEngine;

public class ChangeScaleOverTime : MonoBehaviour
{
    [SerializeField] private float timeToScale = 3;
    [SerializeField] private GameObject[] players;
    [SerializeField] private SpriteController spriteController;

    private float decreaseSpeed;
    private float timer = 0;
    private float scaleToChange;
    private float lastScaleValue;
    private bool isGrowing;

    private void Update() {
        if (timer > 0) {
            if (isGrowing && players[0].transform.localScale.x < scaleToChange)
                for (int i = 0; i < players.Length; i++)
                    players[i].transform.localScale = new Vector3(players[i].transform.localScale.x + Time.deltaTime / decreaseSpeed, players[i].transform.localScale.y + Time.deltaTime / decreaseSpeed, 0);
            else if (!isGrowing && players[0].transform.localScale.x > scaleToChange)
                for (int i = 0; i < players.Length; i++)
                    players[i].transform.localScale = new Vector3(players[i].transform.localScale.x - Time.deltaTime / decreaseSpeed, players[i].transform.localScale.y - Time.deltaTime / decreaseSpeed, 0);
            else timer = 0;
            timer -= Time.deltaTime;
        } else {
            if (spriteController.isUsingNewSprites) {
                while (scaleToChange == lastScaleValue && Mathf.Abs(scaleToChange - lastScaleValue) < 1f)
                    scaleToChange = Random.Range(0.6f, 0.8f);
                timer = timeToScale / 2;
                decreaseSpeed = 10;
            } else {
                while (scaleToChange == lastScaleValue && Mathf.Abs(scaleToChange - lastScaleValue) < 0.5f)
                    scaleToChange = Random.Range(0.75f, 1.5f);
                timer = timeToScale;
                decreaseSpeed = 5;
            }
            isGrowing = lastScaleValue > scaleToChange;
            lastScaleValue = scaleToChange;
        }
    }
}
