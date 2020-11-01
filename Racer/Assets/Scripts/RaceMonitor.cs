using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RaceMonitor : MonoBehaviour
{

    public GameObject[] countDownItems;
    CheckpointManager[] carsCPM;

    public GameObject[] carPrefabs;
    public Transform[] spawnPos;
    public static bool racing = false;
    public static int totalLaps = 1;
    public GameObject gameOverPanel;
    public GameObject HUD;

    int playerCar;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in countDownItems)
            g.SetActive(false);

        StartCoroutine(PlayCountdown());
        gameOverPanel.SetActive(false);

        playerCar = PlayerPrefs.GetInt("PlayerCar");
        GameObject pcar = Instantiate(carPrefabs[playerCar]);
        int randomStartPos = Random.Range(0, spawnPos.Length);
        pcar.transform.position = spawnPos[randomStartPos].position;
        pcar.transform.rotation = spawnPos[randomStartPos].rotation;
        SmoothFollow.playerCar = pcar.gameObject.GetComponent<Drive>().rb.transform;
        pcar.GetComponent<AIController>().enabled = false;
        pcar.GetComponent<PlayerController>().enabled = true;

        foreach(Transform t in spawnPos)
        {
            if (t == spawnPos[randomStartPos]) continue;
            GameObject car = Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)]);
            car.transform.position = t.position;
            car.transform.rotation = t.rotation;

        }

        GameObject[] cars = GameObject.FindGameObjectsWithTag("car");
        carsCPM = new CheckpointManager[cars.Length];
        for (int i = 0; i < cars.Length; i++)
            carsCPM[i] = cars[i].GetComponent<CheckpointManager>();
    }

    IEnumerator PlayCountdown()
    {
        yield return new WaitForSeconds(2);
        foreach(GameObject g in countDownItems)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(1);
            g.SetActive(false);
        }
        racing = true;
    }

    public void RestartLevel()
    {
        racing = false;
        SceneManager.LoadScene("RaceLevel");
    }

    private void LateUpdate()
    {
        int finishedCount = 0;
        foreach(CheckpointManager cpm in carsCPM)
        {
            if (cpm.lap == totalLaps + 1)
                finishedCount++;
        }
        if (finishedCount == carsCPM.Length)
        {
            HUD.SetActive(false);
            gameOverPanel.SetActive(true);
        }
    }
}
