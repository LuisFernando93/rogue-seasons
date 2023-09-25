using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{

    [SerializeField] private float typewriterSpeed = 25f; //multiplicador de velocidade

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation> //armazena os caracteres que vão ter um tempo de espera um pouco maior
    {

        new Punctuation(new HashSet<char>(){'.','!','?'}, 0.6f),
        new Punctuation(new HashSet<char>(){',',';',':'}, 0.3f)
 
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType, TMP_Text textLabel) //chama a corotina
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel) //corotina
    {
        IsRunning = true;
        textLabel.text = string.Empty; //limpa o texto de sample

        float t = 0;
        int charIndex = 0;

        while(charIndex < textToType.Length)//quantidade de caracteres do texto
        {
            int lastCharIndex = charIndex;


            t += Time.deltaTime * typewriterSpeed; //velocidade que o texto é digitado
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length); //controla a variavel

            for(int i = lastCharIndex; i< charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;

                textLabel.text = textToType.Substring(0, i+1); //vai escrevendo

                if(IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i+1], out _)) //implementa o tempo de espera maior em caso de carecter especial
                {
                    yield return new WaitForSeconds(waitTime);
                }
            }

            yield return null; //yield para o script e continua no proximo frame
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime) //checa se o caracter é um caracter especial
    {
        foreach(Punctuation punctuationCategory in punctuations)
        {
            if (punctuationCategory.Puncuations.Contains(character))
            {
                waitTime = punctuationCategory.WaitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Puncuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> puncuations, float waitTime)
        {
            Puncuations = puncuations;
            WaitTime = waitTime;
        }
    }

}
