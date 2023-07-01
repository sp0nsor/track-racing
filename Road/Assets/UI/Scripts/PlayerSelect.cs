using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSelect : MonoBehaviour
{
    private GameObject[] cars;
    private int index;
    private CameraController cameraController;

    private void Start()
    {
        index = PlayerPrefs.GetInt("SelectPlayer");
        cars = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            cars[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in cars)
        {
            go.SetActive(false);
        }

        if (cars[index])
        {
            cars[index].SetActive(true);
        }

        cameraController = Camera.main.GetComponent<CameraController>();
        cameraController.SetTarget(cars[index].transform);
    }

    public void SelectLeft()
    {
        cars[index].SetActive(false);
        index--;

        if (index < 0)
        {
            index = cars.Length - 1;
        }

        cars[index].SetActive(true);
        cameraController.SetTarget(cars[index].transform);
    }

    public void SelectRight()
    {
        cars[index].SetActive(false);
        index++;

        if (index == cars.Length)
        {
            index = 0;
        }

        cars[index].SetActive(true);
        cameraController.SetTarget(cars[index].transform);
    }

    public void StartScene()
    {
        PlayerPrefs.SetInt("SelectPlayer", index);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
