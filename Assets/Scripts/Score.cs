using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

    public Transform player;
    public Text scoreText;
    public float initialPos;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPos = player.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        var dist = player.position.z - initialPos;
        scoreText.text = ((int) dist).ToString();
    }
}
