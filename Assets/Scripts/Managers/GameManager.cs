using UnityEngine;
using UnityEngine.SceneManagement;
using GameTracker;
using UnityEngine.Analytics;

public class GameManager : MonoBehaviour
{
    //Creas un object GameManager vacio (prefab para que sobreviva escenas) con este script.

    public static GameManager instance;
    public Vector2 checkpoint; 
    public int deadVal = -1;
    public int actualScene = 1;
    public bool fullScreenToggle = true,
                mando = true,
                gameIsPaused; 
                //toggleDeath = true;
    public float mainVolSlider = 0.2f,
                 SFXVolSlider = 0.2f,
                 musicVolSlider = 0.2f;


    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Inicializacion de Tracker
            ISerializer serializer = new JsonSerializer();
            IPersistence fp = new FilePersistence(ref serializer);
            TrackerSystem.Init("Neon_Rider", AnalyticsSessionInfo.sessionId.ToString(), AnalyticsSessionInfo.userId, ref fp);

            //Posible adiceion de ServerPersistence
            //IPersistence sp = new ServerPersistence(ref serializer);
            //TrackerSystem.GetInstance().AddPersistence(ref sp);


            //Iniciar Tracker
            TrackerSystem.GetInstance().Start();
        }
        else
        {
            Destroy(this.gameObject);
        }      
    }

    public void FullscreenToggleState(bool isFullscreen)
    {
        if (isFullscreen)
            fullScreenToggle = true;
        else
            fullScreenToggle = false;
    }
    public void ControlToggle(bool isMando)
    {
        mando = isMando;
    }
    /*public void DeathToggle(bool isDeath)
    {
        toggleDeath = !isDeath;
    }*/
    public void MainSliderState (float volume)
    {
        mainVolSlider = volume;
    }
    public void MusicSliderState(float volume)
    {
        musicVolSlider = volume;
    }
    public void SFXSliderState(float volume)
    {
        SFXVolSlider = volume;
    }

    public int getCurrentLevel()
    {
        return actualScene - 2;
    }

    public void ChangeScene()
    {
        checkpoint = new Vector2(0, 0);
        deadVal = -1;
        Time.timeScale = 1;
        //OnSceneChange(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level != 0 && level != SceneManager.sceneCountInBuildSettings - 1)
            actualScene = level;
        switch (level)
        {
            case (0):
                AudioManager.instance.StopAll();
                AudioManager.instance.Play(AudioManager.ESounds.Menu);
                break;
            case (1):
                AudioManager.instance.StopAll();
                AudioManager.instance.Play(AudioManager.ESounds.Level1Low);
                break;
            case (3):
                AudioManager.instance.Stop(AudioManager.ESounds.Level1Low);
                AudioManager.instance.Stop(AudioManager.ESounds.Menu);
                AudioManager.instance.Play(AudioManager.ESounds.Level1);
                break;
            case (5):
                AudioManager.instance.Stop(AudioManager.ESounds.Level1);
                AudioManager.instance.Stop(AudioManager.ESounds.Menu);
                AudioManager.instance.Play(AudioManager.ESounds.Level2);
                break;
            case (6):
            case (8):
                AudioManager.instance.Stop(AudioManager.ESounds.Level2);
                AudioManager.instance.Stop(AudioManager.ESounds.Menu);
                AudioManager.instance.Play(AudioManager.ESounds.Level2Low);
                break;
            case (7):
                AudioManager.instance.Stop(AudioManager.ESounds.Level2Low);
                AudioManager.instance.Stop(AudioManager.ESounds.Menu);
                AudioManager.instance.Play(AudioManager.ESounds.Level2);
                break;
            case (9):
                AudioManager.instance.Stop(AudioManager.ESounds.Level2Low);
                AudioManager.instance.Stop(AudioManager.ESounds.Menu);
                break;
            case (10):
                AudioManager.instance.Stop(AudioManager.ESounds.Boss);
                AudioManager.instance.Stop(AudioManager.ESounds.Menu);
                AudioManager.instance.Play(AudioManager.ESounds.Credits);
                break;
        }
    }
}
