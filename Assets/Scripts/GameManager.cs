using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject mouse;
    [SerializeField] private Sprite[] mouseSprites = new Sprite[3];
    private Vector3 left = new Vector3(0, 0, -90), right = new Vector3(0, 0, 90), up = new Vector3(0, 0, 180);
    private static int health,score;
    private static GameObject healthTMP,play,scoreTMP;
    public static GameObject mouseCanvas;
    
    private void Awake()
    {
        healthTMP = GameObject.Find("Health Text (TMP)");
        mouseCanvas = GameObject.Find("Mouse Canvas");
        play = GameObject.Find("Play Button");
        scoreTMP = GameObject.Find("Score Text (TMP)");

        play.SetActive(false);
    }

    private IEnumerator Wave()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Spawn();
        }
    }
    private void Spawn()
    {
        float moveSpeed = Random.Range(512, 1024);
        GameObject rat;
        int direction = Random.Range(0, 4), mouseSprite = Random.Range(0, 3);
        Vector2 position = Vector2.zero;

        switch (direction)
        {
            case 0: // Right
                position = new Vector2(128 + 2 * mouseCanvas.transform.position.x, Random.Range(0, 2 * mouseCanvas.transform.position.y));
                rat = Instantiate(mouse, position, Quaternion.Euler(left), mouseCanvas.transform);
                rat.GetComponent<Rigidbody2D>().AddForce(moveSpeed * Vector2.left, ForceMode2D.Impulse);
                break;
            case 1: // Left
                position = new Vector2(-256, Random.Range(0, 2 * mouseCanvas.transform.position.y));
                rat = Instantiate(mouse, position, Quaternion.Euler(right), mouseCanvas.transform);
                rat.GetComponent<Rigidbody2D>().AddForce(moveSpeed * Vector2.right, ForceMode2D.Impulse);
                break;
            case 2: // Down
                position = new Vector2(Random.Range(0, 2 * mouseCanvas.transform.position.x), -128);
                rat = Instantiate(mouse, position, Quaternion.Euler(up), mouseCanvas.transform);
                rat.GetComponent<Rigidbody2D>().AddForce(moveSpeed * Vector2.up, ForceMode2D.Impulse);
                break;
            default: // Up
                position = new Vector2(Random.Range(0, 2 * mouseCanvas.transform.position.x), 128 + 2 * mouseCanvas.transform.position.y);
                rat = Instantiate(mouse, position, Quaternion.Euler(Vector3.zero), mouseCanvas.transform);
                rat.GetComponent<Rigidbody2D>().AddForce(moveSpeed * Vector2.down, ForceMode2D.Impulse);
                break;
        }

        switch (mouseSprite)
        {
            case 0:
                rat.GetComponent<Image>().sprite = mouseSprites[0];
                break;
            case 1:
                rat.GetComponent<Image>().sprite = mouseSprites[1];
                break;
            default:
                rat.GetComponent<Image>().sprite = mouseSprites[2];
                break;
        }
    }

    private void Start()
    {
        health = 9;
        score = 0;

        Mouse.clickable = true;
        StartCoroutine(Wave());
    }

    public void Play() => SceneManager.LoadScene("Menu");

    public static void Health()
    {
        --health;
        if (0 < health)
            healthTMP.GetComponent<TextMeshProUGUI>().text = health.ToString();
        else if (health is 0)
        {
            Handheld.Vibrate();
            healthTMP.GetComponent<TextMeshProUGUI>().text = health.ToString();
            Mouse.clickable = false;
            play.SetActive(true);
        }
    }

    public static void Score()
    {
        score += 100;
        scoreTMP.GetComponent<TextMeshProUGUI>().text = score.ToString();
    }
}