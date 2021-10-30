using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse : MonoBehaviour, IPointerDownHandler
{
    private float right,up;
    public static bool clickable=true;

    private void Start()
    {
        right = 2 * GameManager.mouseCanvas.transform.position.x + 256;
        up = 2 * GameManager.mouseCanvas.transform.position.y + 256;
    }

    private void Update()
    {
        if (transform.position.x < -256 ||
            transform.position.x > right ||
            transform.position.y < -256 ||
            transform.position.y > up)
        {
            Destroy(gameObject);
            GameManager.Health();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickable)
        {
            Camera.main.GetComponent<AudioSource>().Play();
            Destroy(gameObject);
            GameManager.Score();
        }
    }
}