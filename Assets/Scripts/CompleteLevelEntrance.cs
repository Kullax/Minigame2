using UnityEngine;
using UnityEngine.UI;

public class CompleteLevelEntrance : MonoBehaviour
{

    public GameObject pipeExitLocation;
    public int LevelToLoad;
    public float requiredSize = 1.0f;
    //public PlayerState.Direction pipeExitFaceingDirection = PlayerState.Direction.North;
    public float followEndPipetimer = 5.0f;
    private Renderer shading;
    //private PlayerState state;
    private Rigidbody rbody;
    private float timer = 0.0f;
    private bool fadePlateActivatet = false;
    private GameObject victoryTransistionScreen;
    private Image fadeScreen;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        //state = player.GetComponent<PlayerState>();
        rbody = player.GetComponent<Rigidbody>();
        //victoryTransistionScreen = (GameObject)Instantiate(Resources.Load("VictoryTransitionScreen"));
        //fadeScreen = victoryTransistionScreen.GetComponentInChildren<Image>();

    }
    void Update()
    {
        if (timer > followEndPipetimer)
        {
            Application.LoadLevel(LevelToLoad);
        }

        if (fadePlateActivatet)
        {
            timer += Time.deltaTime;
            Debug.Log(Mathf.Pow(2, timer));
            Debug.Log(Mathf.Pow(2, followEndPipetimer));
            Debug.Log(Mathf.Pow(2, Mathf.Pow(timer, 10)) / Mathf.Pow(100, followEndPipetimer));
            fadeScreen.color = new Color(1, 1, 1, Mathf.Pow(3, timer) / Mathf.Pow(2, followEndPipetimer));

        }
    }

    void OnTriggerStay(Collider cube)
    {
        if (cube.tag.Equals("Player") && (cube.transform.localScale.x < requiredSize))
        {

            fadePlateActivatet = true;

            cube.transform.position = pipeExitLocation.transform.position;
            //state.facing = pipeExitFaceingDirection;

            //rbody.transform.rotation = state.GetPlayerDesiredRotation();
            rbody.velocity = Vector3.zero;
        }
    }
}