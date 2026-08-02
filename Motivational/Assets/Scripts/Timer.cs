using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    public float tempoDecorrido;
    public bool iniciado = false;

    [SerializeField] TMP_Text btText;

    Squash squash;
    Inventario inventario;
    int ultimoMinuto = 0;

    public bool up = true;
    [SerializeField] TMP_Text textBtTrocar;

    void Start()
    {
        squash = GameObject.Find("Machine").GetComponent<Squash>();
        inventario = GameObject.Find("Inventario").GetComponent<Inventario>();

        Load();
    }

    void Update()
    {
        if(iniciado && up)
        {
            tempoDecorrido += Time.deltaTime;
            inventario.tempoDecorrido = tempoDecorrido;

            int horas = Mathf.FloorToInt(tempoDecorrido / 3600);
            int minutos = Mathf.FloorToInt(tempoDecorrido % 3600 / 60);
            int segundos = Mathf.FloorToInt(tempoDecorrido % 60);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);

            if(minutos > ultimoMinuto && minutos % 15 == 0)
            {
                ultimoMinuto = minutos;

                GetPoint();
            }
        }

        if(iniciado && !up)
        {
            inventario.tempoGanho -= Time.deltaTime;

            int horas = Mathf.FloorToInt(inventario.tempoGanho / 3600);
            int minutos = Mathf.FloorToInt(inventario.tempoGanho % 3600 / 60);
            int segundos = Mathf.FloorToInt(inventario.tempoGanho % 60);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
            inventario.Formatar();

            if(inventario.tempoGanho <= 0)
            {
                inventario.tempoGanho = 0;
                iniciado = false;
                up = true;
            }
        }

        inventario.Salvar();
    }

    public void Iniciar()
    {
        if(!iniciado)
        {
            btText.text = "Parar";
            iniciado = true;
            Debug.Log("Inciado");
        }
        else
        {
            btText.text = "Inciar";
            iniciado = false;
            Debug.Log("Pausado");
        }
    }

    public void GetPoint()
    {
        int bead = Random.Range(1, 4);

        switch(bead)
        {
            case 1:
                inventario.mouseBeads += 1;
            break;
            case 2:
                inventario.hyenaBeads += 1;
            break;
            case 3:
                inventario.jellyfishBeads += 1;
            break;
        }

        squash.AplicarSquash();
    }

    public void ChangeTime()
    {
        if(up)
        {
            textBtTrocar.text = "Ganhar tempo";
            up = false;
            if(iniciado)
            {
                Iniciar();
            }

            int horas = Mathf.FloorToInt(inventario.tempoGanho / 3600);
            int minutos = Mathf.FloorToInt(inventario.tempoGanho % 3600 / 60);
            int segundos = Mathf.FloorToInt(inventario.tempoGanho % 60);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
        }
        else
        {
            textBtTrocar.text = "Usar tempo";
            up = true;
            if(iniciado)
            {
                Iniciar();
            }

            int horas = Mathf.FloorToInt(tempoDecorrido / 3600);
            int minutos = Mathf.FloorToInt(tempoDecorrido % 3600 / 60);
            int segundos = Mathf.FloorToInt(tempoDecorrido % 60);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
        }
    }

    public void Zerar()
    {
        textBtTrocar.text = "Usar tempo";
        up = true;
        if(iniciado)
        {
            Iniciar();
        }

        inventario.tempoDecorrido = 0;
        inventario.tempoGanho = 0;
        inventario.ultimoMinuto = 0;

        inventario.mouseBeads = 0;
        inventario.hyenaBeads = 0;
        inventario.jellyfishBeads = 0;

        inventario.Salvar();
        Load();
    }

    public void Load()
    {
        GameData data = SaveSystem.LoadInfo();
        inventario.tempoDecorrido = data.tempoDecorrido;
        inventario.tempoGanho = data.tempoGanho;
        inventario.mouseBeads = data.beads[0];
        inventario.hyenaBeads = data.beads[1];
        inventario.jellyfishBeads = data.beads[2];
        inventario.ultimoMinuto = data.ultimoMinuto;

        tempoDecorrido = inventario.tempoDecorrido;
        ultimoMinuto = inventario.ultimoMinuto;

        int horas = Mathf.FloorToInt(tempoDecorrido / 3600);
        int minutos = Mathf.FloorToInt(tempoDecorrido % 3600 / 60);
        int segundos = Mathf.FloorToInt(tempoDecorrido % 60);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);

        inventario.Formatar();
    }
}
