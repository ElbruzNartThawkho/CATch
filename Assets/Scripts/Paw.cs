using UnityEngine;
using UnityEngine.EventSystems;

public class Paw : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject paw;

    public void OnPointerDown(PointerEventData eventData) => Destroy(Instantiate(paw, eventData.position, Quaternion.identity, transform), 2);
}