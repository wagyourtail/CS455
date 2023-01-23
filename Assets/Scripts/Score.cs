using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform player;
    public Text scoreText;
    public float initialPos;
    private int _points = 0;
    

    public static Score Get()
    {
        return FindObjectOfType<Score>();
    }
    
    public int getPoints()
    {
        return _points;
    }

    public void addPoint()
    {
        ++_points;
    }

    public void addPoints(int count)
    {
        _points += count;
    }

    public void freeze()
    {
        enabled = false;
    }

    public GameManager gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        initialPos = player.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = _points.ToString();
    }
}
