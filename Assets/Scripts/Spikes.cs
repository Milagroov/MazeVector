using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spikes : MonoBehaviour
{

    public string currentLevel;

    public ParticleSystem ps;
    public GameObject spikes;
    //public CamShake shake;


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
            StartCoroutine(WaitLoadScene());
            Instantiate(ps, spikes.transform.position, Quaternion.identity);
            //shake.shouldShake = true;

        }
    }

    IEnumerator WaitLoadScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(currentLevel);
    }

}
