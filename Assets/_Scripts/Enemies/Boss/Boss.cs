using UnityEngine;
using TMPro;

public class Boss : MonoBehaviour
{
    [SerializeField] public float recoverTime;
    [SerializeField] public int baseLife, life;
    [SerializeField] protected GameObject floatingPoints;
    [HideInInspector] public int power;

    [HideInInspector] public WeightedRandomList<string> Attacks;

    private bool canTakeDamage;

    private string currentAnimation;
    protected Animator animator;


    public void ChangeAnimation(string newAnimation)
    {
        if (currentAnimation == newAnimation) return;

        animator.Play(newAnimation);

        currentAnimation = newAnimation;

    }

    public void TakeDamage(int power)
    {
        if (canTakeDamage)
        {
            FloatingDamage(power);
            this.life -= power;
            if (life <= 0)
            {
                Destroy(gameObject,0.2f);
            }
        }
    }

    protected void FloatingDamage(int damage)
    {
        GameObject point;
        if (Mathf.Abs(this.gameObject.transform.rotation.eulerAngles.y) > 0.01f)
        {
            // Se a rotação do objeto pai não for zero, defina uma rotação padrão para a instância
            Quaternion defaultRotation = Quaternion.Euler(0f, 0f, 0f); // Substitua pelos ângulos desejados

            // Crie a instância com a rotação padrão
            point = Instantiate(floatingPoints, transform.position, defaultRotation);
        }
        else
        {
            point = Instantiate(floatingPoints, transform.position, transform.rotation);

        }
        point.transform.SetParent(this.transform);
        point.GetComponent<TextMeshPro>().text = damage.ToString();
        if (damage <= 1)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
        }
        else if (damage > 1 && damage <= 2)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 103, 0, 255);
        }
        else if (damage > 2 && damage <= 5)
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 0, 0, 255);
        }
        else
        {
            point.GetComponent<TextMeshPro>().color = new Color(255, 255, 255, 255);
        }
    }
}
