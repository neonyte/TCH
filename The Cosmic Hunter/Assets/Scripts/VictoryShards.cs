using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryShards : MonoBehaviour {
    int shardAmt;
    public GameObject firstShard;
    public GameObject secondShard;
    public GameObject thirdShard;
    public float scaleSpeed = 2.0f;

	void Start () {
        shardAmt = GameObject.FindGameObjectWithTag("Player").GetComponent<MuniaControllerScript>().shardsCollected;
        firstShard.transform.localScale = new Vector3 (0, 0, 0);
        secondShard.transform.localScale = new Vector3(0, 0, 0);
        thirdShard.transform.localScale = new Vector3(0, 0, 0);
        shardEnlarge();
    }

    void shardEnlarge()
    {
        if (shardAmt > 0) StartCoroutine(shardEnlargeCor(firstShard, 0f));
        if (shardAmt > 1) StartCoroutine(shardEnlargeCor(secondShard, 0.5f));
        if (shardAmt > 2) StartCoroutine(shardEnlargeCor(thirdShard, 1f));
    }
    IEnumerator shardEnlargeCor(GameObject shard, float delay)
    {
        Vector3 startScale = shard.transform.localScale;
        Vector3 targetScale = new Vector3(1, 1, 1);
        float duration = 1.0f;
        float timer = 0.0f;
        yield return new WaitForSeconds(delay);
        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            t = t * t * t * (t * (6f * t - 15f) + 10f);
            shard.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }

        yield return null;
    }
    
}
