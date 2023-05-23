using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CameraFollow : MonoBehaviour
{
    
    [SerializeField] Camera playerCamera;
    [SerializeField] Camera godViewCamera;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Canvas worldSpaceCanvas;
    Camera activeCamera;
    float timerGodView;
    const float maxGodViewTime = 5f;
    int godViewUsageTimes;
    int maxGodViewUsage;
    
    Vector3 temp;
    [SerializeField] float sizeZoom;
    [SerializeField] float sizeNotZoom;

    // Start is called before the first frame update

    void Start()
    {
        SetActiveCamera(playerCamera);
        playerCamera.orthographicSize = sizeZoom;
        //Switch for god view
        playerCamera.transform.position = transform.position + Vector3.back * 200;
        timerGodView = -1;
        godViewUsageTimes = 0;
        int difficulty = PlayerPrefs.GetInt(Difficulty.keyDifficulty);
        if (difficulty == 1)        // easy
            maxGodViewUsage = -1;
        if (difficulty == 2)        // normal
            maxGodViewUsage = 3;
        if (difficulty == 3)        // hard
            maxGodViewUsage = 0 ;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyBinds.godView) && timerGodView<0 && !Player.self.endLevel && (godViewUsageTimes < maxGodViewUsage || maxGodViewUsage<0))
        {
            GodView(true);
            timerGodView = 0;
            godViewUsageTimes++;
            if(Player.useEye != null)
                Player.useEye(godViewUsageTimes);
        }
        if(timerGodView >= 0)
        {
            timerGodView += Time.deltaTime;
            if(timerGodView > maxGodViewTime || Player.self.endLevel)
            {
                GodView(false);
                timerGodView = -1f;
            }
        }

        temp = transform.position;
        temp.z = -200;
        playerCamera.transform.position = temp;
    }

    void GodView(bool enable)
    {
        godViewCamera.depth = playerCamera.depth - 1;
        if (enable) godViewCamera.depth += 2;
        SetActiveCamera(enable ? godViewCamera : playerCamera);
    }

    void SetActiveCamera(Camera camera)
    {
        activeCamera = camera;
        videoPlayer.targetCamera = camera;
        //worldSpaceCanvas.worldCamera = camera;
    }
}
