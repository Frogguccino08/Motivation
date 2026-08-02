using TMPro;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    public int mouseBeads;
    public int hyenaBeads;
    public int jellyfishBeads;

    public float tempoGanho = 0;
    public float tempoDecorrido = 0;
    public int ultimoMinuto = 0;

    [SerializeField] TMP_Text mouseText;
    [SerializeField] TMP_Text hyenaText;
    [SerializeField] TMP_Text jellyfishText;

    [SerializeField] TMP_Text tempoGanhoTxt;

    void Update()
    {
        mouseText.text = "x" + mouseBeads;
        hyenaText.text = "x" + hyenaBeads;
        jellyfishText.text = "x" + jellyfishBeads;
    }

    public void GanharTempo(int bead)
    {
        switch(bead)
        {
            case 1:
                if(mouseBeads >= 3)
                {
                    mouseBeads -= 3;
                    tempoGanho += 15 * 60;
                    Formatar();
                }
            break;
            case 2:
                if(hyenaBeads >= 3)
                {
                    hyenaBeads -= 3;
                    tempoGanho += 30 * 60;
                    Formatar();
                }
            break;
            case 3:
                if(jellyfishBeads >= 3)
                {
                    jellyfishBeads -= 3;
                    tempoGanho += 60 * 60;
                    Formatar();
                }
            break;
        }
    }

    public void Formatar()
    {
        int horas = Mathf.FloorToInt(tempoGanho / 3600);
        int minutos = Mathf.FloorToInt(tempoGanho % 3600 / 60);
        int segundos = Mathf.FloorToInt(tempoGanho % 60);

        tempoGanhoTxt.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
    }

    public void Salvar()
    {
        SaveSystem.SaveInfo(this);
    }
}
