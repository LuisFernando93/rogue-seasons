using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuTranslation : MonoBehaviour
{
    /*
     No geral esse script seria colocado no canvas
     */


    //texto dos botões, teria de criar um para cada botão especifico, da p fzr usando List<string> tbm
    [SerializeField] private string buttonTextPT, buttonTextING;
    //texto dos creditos, usa o TextArea pq é melhor p escrever texto grande
    [TextArea] [SerializeField] private string creditosPT, creditosING;
    //Coloca os botões aq p acessar mais facil
    [SerializeField] private TextMeshProUGUI button, creditos;

    void Start()
    {
        //Jeito que a acessamos a lingua no nosso jogo
        Configuration.LanguageOption language = GameObject.FindGameObjectWithTag("Manager").GetComponent<Configuration>().GetLanguage();

        //acessa o compotente do text mesh pro e muda o texto dependendo da variavel
        if(language == Configuration.LanguageOption.PTBR)
        {
            button.text = buttonTextPT;
            creditos.text = creditosPT;
        }
        else if(language == Configuration.LanguageOption.ING)
        {
            button.text = buttonTextING;
            creditos.text = creditosING;
        }
    }


}
