using UnityEngine;

[System.Serializable]
public class GameData
{
    public float tempoDecorrido;
    public float tempoGanho;
    public int ultimoMinuto;
    public int[] beads;

    public GameData (Inventario inventario)
    {
        tempoDecorrido = inventario.tempoDecorrido;
        tempoGanho = inventario.tempoGanho;
        ultimoMinuto = inventario.ultimoMinuto;

        beads = new int[3];

        beads[0] = inventario.mouseBeads;
        beads[1] = inventario.hyenaBeads;
        beads[2] = inventario.jellyfishBeads;
    }
}
