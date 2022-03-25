using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    // Start is called before the first frame update

    public int nivelDeJogo;

    public int [] tempoWave;
    public float tempoWaveAtual;
    public bool rolandoWave;


    public float inimigoVelocidade;


    public float[] temposDeSpawn;
    public float spawnTime;


    public Transform[] spawns;
    public GameObject[] dogPrefab;

    //aviso de nova wave
    public Text textoDeWave;


    void Start()
    {
        nivelDeJogo = 0;
        StartCoroutine("NovaWave");
    }

    // Update is called once per frame
    void Update()
    {

        
        // parar de spawnar e contar o tempo entre as waves
        if(tempoWaveAtual <= 0 )
        {
            rolandoWave = false;
            nivelDeJogo++;
            spawnTime = temposDeSpawn[nivelDeJogo];
            //chamando rotina de nova wave
            StartCoroutine("NovaWave");
        }
        else
        {

        // contadores de tempo diminuir
        if(rolandoWave)
        {
            spawnTime -= Time.deltaTime;
            tempoWaveAtual -= Time.deltaTime;
        }

        //spawnar cachorro quando tempo for menor ou igual a zero
        if(spawnTime <= 0)
        {
            //spawnar cachorro e resetar o tempo
            int randomIndex = Random.Range(0, 2);
            Instantiate(dogPrefab[randomIndex], spawns[randomIndex].position, Quaternion.identity);
            spawnTime = temposDeSpawn[nivelDeJogo];
             }
         }
    }

    public void IniciandoWave()
    {
        //definir quanto tempo vai durar a wave baseado no nívelDeJogo
        tempoWaveAtual = tempoWave[nivelDeJogo];
        //definir o tempo de spawn baseado no nível de jogo
        spawnTime = temposDeSpawn[nivelDeJogo];
        //definir variavel avisando que iniciou uma nova wave
        rolandoWave = true;
    }

    IEnumerator NovaWave()
    {
        //rotina que define o nome da nova wave, faz aparecer o texto por 3 segundos e depois inicia a próxima wave
        IniciandoWave();
        textoDeWave.text = "Wave " + (nivelDeJogo+1);
        textoDeWave.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        textoDeWave.gameObject.SetActive(false);
        rolandoWave = true;
    }


    public void Reiniciar(){
        StartCoroutine("NovoJogo");
    }

      IEnumerator NovoJogo()
    {

        yield return new WaitForSeconds(1);
        Application.LoadLevel("SampleScene");
    
    }
}
