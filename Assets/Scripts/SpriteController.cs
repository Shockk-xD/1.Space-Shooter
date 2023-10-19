using UnityEngine;
using UnityEngine.UI;

public class SpriteController : MonoBehaviour
{
    [SerializeField] private GameObject[] players = new GameObject[0];
    [SerializeField] private GameObject[] additionalSprites = new GameObject[0];
    [SerializeField] private Sprite newSpriteForPlayer;
    [SerializeField] private Sprite oldSpriteForPlayer;
    [SerializeField] private Image ChangeButton;
    [SerializeField] private Text text;

    public bool isUsingNewSprites;
    
    private void Start() {
        if (!PlayerPrefs.HasKey("UsingNewSpriteForPlayer"))
            PlayerPrefs.SetString("UsingNewSpriteForPlayer", "False");
        isUsingNewSprites = PlayerPrefs.GetString("UsingNewSpriteForPlayer") == "True";
        PlayerPrefs.Save();
        ChangeSprites();
    }

    public void ChangeSpritesButton() {
        isUsingNewSprites = !isUsingNewSprites;
        PlayerPrefs.SetString("UsingNewSpriteForPlayer", isUsingNewSprites.ToString());
        PlayerPrefs.Save();
        ChangeSprites();
    }

    private void ChangeSprites() {
        if (isUsingNewSprites) {
            ChangePlayersSprite("New");
            ChangeButton.color = new Color(0, 150f / 255f, 0);
            text.text = "Use Old Sprites";
        } else {
            ChangePlayersSprite("Old");
            ChangeButton.color = new Color(1f, 0, 0);
            text.text = "Use New Sprites";
        }
    }

    private void ChangePlayersSprite(string value) {
        if (value == "New") {
            foreach (GameObject player in players) {
                player.GetComponent<SpriteRenderer>().sprite = newSpriteForPlayer;
                player.GetComponent<SpriteRenderer>().color = new Color(205f / 255f, 0, 1f);
                player.GetComponent<Animator>().enabled = true;
                PolygonCollider2D polygonCollider2D = player.GetComponent<PolygonCollider2D>();
                Destroy(polygonCollider2D);
                polygonCollider2D = player.AddComponent<PolygonCollider2D>();
                polygonCollider2D.isTrigger = true;
                player.transform.localScale = Vector3.one * 0.65f;
            }
            foreach (GameObject addSprite in additionalSprites)
                addSprite.SetActive(true);
        } else if (value == "Old") {
            foreach (GameObject addSprite in additionalSprites)
                addSprite.SetActive(false);
            foreach (GameObject player in players) {
                player.GetComponent<SpriteRenderer>().sprite = oldSpriteForPlayer;
                player.GetComponent<SpriteRenderer>().color = Color.white;
                player.GetComponent<Animator>().enabled = false;
                PolygonCollider2D polygonCollider2D = player.GetComponent<PolygonCollider2D>();
                Destroy(polygonCollider2D);
                polygonCollider2D = player.AddComponent<PolygonCollider2D>();
                polygonCollider2D.isTrigger = true;
                player.transform.localScale = Vector3.one * 0.9f;
            }
        }
    }
}
