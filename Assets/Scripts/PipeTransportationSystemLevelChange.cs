using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PipeTransportationSystemLevelChange : MonoBehaviour
{


    public GameObject ExitLocation;
    public float requiredsize = 1.0f;
    public GameRotation NewGameRotation = GameRotation.NegativeX;
    public string LevelToLoad = "level to load";

    private float waitTimeElapsed = 0.0f;
    private float WaitTimeEndScreen = 3.0f;
    private float WaitTimeParticles = 3.0f;
    private bool EndLevel = false;
    private bool particlesPlayed = false;

    // Fade to white screen
    private GameObject victoryTransistionScreen;
    private Image fadeScreen;

    private Animator CAC; //Character Animation Controller

    // Use this for initialization
    void Start()
    {
        GameObject CharacterRenderer = GameObject.Find("iceCube_animation_control");
        CAC = CharacterRenderer.GetComponent<Animator>();

        victoryTransistionScreen = (GameObject)Instantiate(Resources.Load("VictoryTransitionScreen"));
        fadeScreen = victoryTransistionScreen.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider cube)
    {
        if (cube.gameObject.tag != "Player")
            return;

        Rigidbody player = cube.attachedRigidbody;

        player.velocity = new Vector3(0, 0, 0);
        cube.transform.position = transform.position;

        if (!particlesPlayed)
        {
            waitTimeElapsed += Time.deltaTime;

            if (waitTimeElapsed >= WaitTimeParticles)
            {
                particlesPlayed = true;
                waitTimeElapsed = 0.0f;
            }

            if (particlesPlayed)
            {
                cube.transform.position = ExitLocation.transform.position;
                GameManager.SetGameRotation(NewGameRotation);
                EndLevel = true;
            }
        }
    }
    void Update()
    {
        if (EndLevel)
        {
            waitTimeElapsed += Time.deltaTime;

            fadeScreen.color = new Color(1, 1, 1, Mathf.Pow(3, waitTimeElapsed) / Mathf.Pow(2, WaitTimeEndScreen));

            if (waitTimeElapsed >= WaitTimeEndScreen)
            {
                waitTimeElapsed = 0;
                EndLevel = false;
                Application.LoadLevel(LevelToLoad);
            }
        }
    }
}
