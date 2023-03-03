using System.Collections;
using UnityEngine;

public class FollowPlayerPosition : MonoBehaviour
{
    
    [SerializeField] private Transform playerTransform;
    
    private void Start()
    {
        StartCoroutine(FollowPlayer());
    }

    private IEnumerator FollowPlayer(float cooldown = 0.5f)
    {
        while (true)
        {
            transform.position = new Vector3(0, playerTransform.position.y, 0);
            yield return new WaitForSeconds(cooldown);
        }
        // ReSharper disable once IteratorNeverReturns
    }
}
