using System.Collections.Generic;
using UnityEngine;

public class TunnelGenerator : MonoBehaviour
{
    public GameObject tunnelPrefab;
    public Transform player;
    public float generationDistanceX = 10f;
    public float tunnelPositionY = 0f;
    public float deleteDistance = 20f;

    private float playerLastX;
    private List<GameObject> forwardSegments = new List<GameObject>();
    private List<GameObject> backwardSegments = new List<GameObject>();

    void Start()
    {
        playerLastX = player.position.x;
        GenerateInitialSegment();
    }

    void Update()
    {
        GenerateSegmentsIfNeeded();
        DeleteOldSegments();
    }

    void GenerateInitialSegment()
    {
        var initialSegment = Instantiate(tunnelPrefab, new Vector3(player.position.x, tunnelPositionY, 0), Quaternion.identity);
        forwardSegments.Add(initialSegment);
        backwardSegments.Add(initialSegment);
    }

    void GenerateSegmentsIfNeeded()
    {
        float playerMoveDirection = player.position.x - playerLastX;
        bool forwardConditionMet = player.position.x > forwardSegments[forwardSegments.Count - 1].transform.position.x - generationDistanceX;
        bool backwardConditionMet = player.position.x < backwardSegments[0].transform.position.x + generationDistanceX;

        if (playerMoveDirection > 0 && forwardConditionMet)
        {
            GenerateSegment(true);
        }
        else if (playerMoveDirection < 0 && backwardConditionMet)
        {
            GenerateSegment(false);
        }
        else if (playerMoveDirection == 0)
        {
            if (forwardConditionMet)
            {
                GenerateSegment(true);
            }
            if (backwardConditionMet)
            {
                GenerateSegment(false);
            }
        }

        playerLastX = player.position.x;
    }

    void GenerateSegment(bool forward)
    {
        GameObject newSegment;
        if (forward)
        {
            newSegment = Instantiate(tunnelPrefab, new Vector3(forwardSegments[forwardSegments.Count - 1].transform.position.x + generationDistanceX, tunnelPositionY, 0), Quaternion.identity);
            forwardSegments.Add(newSegment);
        }
        else
        {
            newSegment = Instantiate(tunnelPrefab, new Vector3(backwardSegments[0].transform.position.x - generationDistanceX, tunnelPositionY, 0), Quaternion.identity);
            backwardSegments.Insert(0, newSegment); // Добавляем в начало списка
        }
    }

    void DeleteOldSegments()
    {
        // Удаление сегментов перед игроком
        while (forwardSegments.Count > 0 && forwardSegments[0] != null && player.position.x - forwardSegments[0].transform.position.x > deleteDistance)
        {
            Destroy(forwardSegments[0]);
            forwardSegments.RemoveAt(0);
        }

        // Удаление сегментов позади игрока
        while (backwardSegments.Count > 0 && backwardSegments[backwardSegments.Count - 1] != null && backwardSegments[backwardSegments.Count - 1].transform.position.x - player.position.x > deleteDistance)
        {
            Destroy(backwardSegments[backwardSegments.Count - 1]);
            backwardSegments.RemoveAt(backwardSegments.Count - 1);
        }
    }
}
