using System.Collections;
using UnityEngine;

public class FollowPlayerPosition : MonoBehaviour
{
    [SerializeField] private int zPosition = 0;
    [SerializeField] private Transform playerTransform;
    
    private void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer(float cooldown = 0.5f)
    {
        while (true)
        {
            transform.position = new Vector3(0, playerTransform.position.y, zPosition);
            yield return new WaitForSeconds(cooldown);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
