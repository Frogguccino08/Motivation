using System.Collections;
using UnityEngine;

public class Squash : MonoBehaviour
{
    public float intensidade = 0.25f;
    public float duracao = 0.15f;

    private Vector3 escalaInicial;
    private Coroutine efeito;

    void Start()
    {
        escalaInicial = transform.localScale;
    }

    public void AplicarSquash()
    {
        if (efeito != null)
            StopCoroutine(efeito);

        efeito = StartCoroutine(SquashStretch());
    }

    IEnumerator SquashStretch()
    {
        Vector3 squash = new Vector3(
            escalaInicial.x * (1 + intensidade),
            escalaInicial.y * (1 - intensidade),
            escalaInicial.z * (1 + intensidade)
        );

        float tempo = 0;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            transform.localScale = Vector3.Lerp(escalaInicial, squash, tempo / duracao);
            yield return null;
        }

        tempo = 0;

        while (tempo < duracao)
        {
            tempo += Time.deltaTime;
            transform.localScale = Vector3.Lerp(squash, escalaInicial, tempo / duracao);
            yield return null;
        }

        transform.localScale = escalaInicial;
    }
}
