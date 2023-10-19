using UnityEngine;

public class AudioManager1 : MonoBehaviour
{
    static private AudioManager1 instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        } else
            Destroy(this.gameObject);
    }
}
