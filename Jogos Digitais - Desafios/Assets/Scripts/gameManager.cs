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

    //Elementos de UI
    public Text textoDeWave, textoScore, textoHighscore;
    public GameObject painelPause, painelGameOver;

    //Variáveis de Score

    public float score;
    


    void Start()
    {
        score = 0;
        textoHighscore.text = "Highscore: " +Mathf.RoundToInt(PlayerPrefs.GetFloat("highscore", 0));
        nivelDeJogo = 0;
        StartCoroutine("NovaWave");
        rolandoWave = true;
    }

    // Update is called once per frame
    void Update()
    {

        UpdateScore();
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
            score += Time.deltaTime * 10;
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
        //rolandoWave = true;
    }

    IEnumerator NovaWave()
    {
        //rotina que define o nome da nova wave, faz aparecer o texto por 6 segundos e depois inicia a próxima wave
        IniciandoWave();
        textoDeWave.text = "Wave " + (nivelDeJogo+1);
        textoDeWave.gameObject.SetActive(true);
        yield return new WaitForSeconds(6);
        textoDeWave.gameObject.SetActive(false);
        rolandoWave = true;
    }


    public void GameOver(){
        Time.timeScale = 0;
        painelGameOver.SetActive(true);
    }


    public void Pausar(){
        Time.timeScale = 0;
        painelPause.SetActive(true);
    }

    public void Despausar(){
        Time.timeScale = 1;
        painelPause.SetActive(false);
    }

    public void Recomeçar(){
        Time.timeScale = 1;
        Application.LoadLevel("Game");
    }

    public void UpdateScore(){
        textoScore.text = "Score: " +Mathf.RoundToInt(score);
        if(score > PlayerPrefs.GetFloat("highscore", 0)){
            PlayerPrefs.SetFloat("highscore", score);
        }
        textoHighscore.text = "Highscore: " +Mathf.RoundToInt(PlayerPrefs.GetFloat("highscore", 0));
    }
}
