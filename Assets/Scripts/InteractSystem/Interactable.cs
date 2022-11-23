using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class Interactable : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<CircleCollider2D>().isTrigger = true;
        GetComponent<CircleCollider2D>().radius = 0.8f;
    }
    public abstract void Interact();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Player>().OpenInteractableIcon();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<Player>().CloseInteractableIcon();
    }
}
