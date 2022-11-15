using UnityEngine;

public class FacaController : MonoBehaviour
{
    //Objetos
    [SerializeField] Player player;

    //Variaveis
    public float bulletForce = 20f;
    private Animator animator;
    Vector3 mousePos;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {

        WeaponRotation();
        /*if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("Attack01");
        }*/
    }
    // Rotaciona a arma de acordo com a posição do mouse
    void WeaponRotation()
    {
        //calcula a posição do mouse de acordo com a camera 
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Faz os ajustes para ela rodar em volta do proprio eixo e execuda a correção de inclinação de acordo com o sentido que o player esta virado
        if (player.facingRight)
            transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, 90);
        else
            transform.rotation = Quaternion.LookRotation(Vector3.forward, mousePos - transform.position) * Quaternion.Euler(0, 0, -90); 
    }


}
