using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    private GameObject firstPlayerPrefab;
    private GameObject secondPlayerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Update()
    {
    }

    public void CreateFirstPlayer()
    {
        StartCoroutine(FirstPlayer());

    }

    public void CreateSecondPlayer()
    {
        StartCoroutine(SecondPlayer());

    }

    private IEnumerator FirstPlayer()
    {
        while(SceneManager.GetActiveScene().name != "GamePlay")
        {
            yield return null;
        }
        Vector3 kingPosition = new Vector3(-6.519f, -2.279f, 0f); 
        firstPlayerPrefab = Resources.Load("King") as GameObject;
        PhotonNetwork.Instantiate(firstPlayerPrefab.name, kingPosition, Quaternion.identity);
        yield break;
    }

    private IEnumerator SecondPlayer()
    {
        while(SceneManager.GetActiveScene().name != "GamePlay")
        {
            yield return null;
        }
        Vector3 pigPosition = new Vector3(23f, .9f, 0f);
        secondPlayerPrefab = Resources.Load("PicKing") as GameObject;
        PhotonNetwork.Instantiate(secondPlayerPrefab.name, pigPosition, Quaternion.Euler(0f, 0f, 0f));
        yield break;
    }
}
