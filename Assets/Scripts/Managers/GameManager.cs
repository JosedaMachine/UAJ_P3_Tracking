using UnityEngine;
using UnityEngine.SceneManagement;
using GameTracker;
using UnityEngine.Analytics;
using System.IO;

public class GameManager : MonoBehaviour
{
    const string editorPath = "Assets/trackedData/";
    string buildPath;
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

    [Header("TRACKER SETTINGS")]
    [Header("Persistancy Frecuency (secs)")]
    [Tooltip("If 0, it will just persist when tracker stops.")]
    public uint timePersistance_ = 0;

    [Header("User ID")]
    [Tooltip("If void, it will use Unity Analitics user ID information.")]
    public string userID_ = "";

    // En el método Awake comprueba si hay otro GameManger
    // y si no lo hay se inicializa como GameManager. En el caso
    // que hubiera otro se autodestruye
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

            //Inicializar tracker
            InitiaizeTracker();
        }
        else
        {
            Destroy(this.gameObject);
        }

        buildPath = Application.dataPath + "/trackerData/";
    }

    private void InitiaizeTracker()
    {
        //Inicializacion de Tracker
        ISerializer serializer = new JsonSerializer();

        //Asignacion del path de los ficheros del tracker
        string trackerPath;
        if (Application.isEditor)
            trackerPath = editorPath;
        else
        {
            trackerPath = buildPath;
            //if (!Directory.Exists(trackerPath))
            //{
            //    Directory.CreateDirectory(trackerPath);
            //}
        }

        string session = AnalyticsSessionInfo.sessionId.ToString();

        //Asignamos el nombre del fichero
        ((JsonSerializer)serializer).setName("trackerData_" + session + ".json");

        IPersistence fp = new FilePersistence(ref serializer);

        //Asignamos la salida de los datos persistidos.
        (fp as FilePersistence).setOutPutPath(trackerPath);

        //En caso de que no se defina identificado de usuario.
        if (string.IsNullOrWhiteSpace(userID_))
            userID_ = AnalyticsSessionInfo.userId;

        //Inicializamos la instancia del Tracker
        TrackerSystem.Init("Neon_Rider", session, userID_, ref fp);

        ////Añadimos nuevos sistema de persistencia
        ISerializer serializerCSV = new CSVSerializer();
        ((CSVSerializer)serializerCSV).setName("trackerData_" + session + ".csv");
        IPersistence fpCSV = new FilePersistence(ref serializerCSV);
        (fpCSV as FilePersistence).setOutPutPath(trackerPath);
        TrackerSystem.GetInstance().AddPersistence(ref fpCSV);

        //Posible adicion de ServerPersistence
        //IPersistence sp = new ServerPersistence(ref serializer);
        //TrackerSystem.GetInstance().AddPersistence(ref sp);

        //Configurations
        TrackerSystem.GetInstance().setFrecuencyPersistanceTimeSeconds(timePersistance_);
        //Iniciar Tracker
        TrackerSystem.GetInstance().Start();
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
