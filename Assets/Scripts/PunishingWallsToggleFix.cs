using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PunishingWallsToggleFix : MonoBehaviour
{
    GameController gameController;
    Toggle toggle;
    
    void Start()
    {
        gameController = FindObjectOfType<GameController>();
        toggle = GetComponent<Toggle>();
        StartCoroutine(FixWallToggle());
    }

    IEnumerator FixWallToggle()
    {
        yield return new WaitForEndOfFrame();
        if (gameController.wallType == WallType.Punishing)
            toggle.isOn = true;
        else
            toggle.isOn = false;
    }
}
