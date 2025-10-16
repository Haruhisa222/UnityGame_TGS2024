using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class HitoSpown : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] gameObjects;
    public Transform[] targetPositions;
    private List<Transform> availableTargets;
    private List<Transform> usedTargets = new List<Transform>();
    public float moveSpeed = 2f;
    private List<GameObject> movingObjects = new List<GameObject>();
    private List<GameObject> selectedObjects = new List<GameObject>();
    public int SW;
    public int SW2;
    public SpriteRenderer targetspriteRenderer;
    public SpriteRenderer targetspriteRenderer2;
    public SpriteRenderer targetspriteRenderer3;
    //public SpriteRenderer targetspriteRenderer4;
    public Text targetText;
    public Text targetText2;
    public Text targetText3;
    //public Text targetText4;

    
    private bool isRespawning = false;

    void Start()
    {
        if (spawnPoints.Length < 2 || gameObjects.Length < 2 || targetPositions.Length < 2)
        {
            Debug.LogError("スポーン地点、ゲームオブジェクト、またはターゲット位置が不足しています");
            return;
        }

        // 初期のゲームオブジェクトをランダムに2つ選択
        selectedObjects = SelectRandomObjects(gameObjects, 2);

        // スポーン地点とターゲット位置をランダムに2つ選択
        List<Transform> selectedSpawnPoints = SelectRandomPoints(spawnPoints, 2);
        availableTargets = new List<Transform>(targetPositions);
        ShuffleList(availableTargets);
        
        // ゲームオブジェクトをスポーン地点からターゲット位置に移動させる
        StartCoroutine(SpawnAndMoveObjects(selectedObjects, selectedSpawnPoints));
    }

    void ShuffleList<T>(List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public List<Transform> SelectRandomPoints(Transform[] points, int count)
    {
        if (points == null || points.Length == 0 || count <= 0 || count > points.Length)
        {
            Debug.LogError("Invalid parameters");
            return new List<Transform>();
        }

        List<int> indices = new List<int>(points.Length);
        for (int i = 0; i < points.Length; i++)
        {
            indices.Add(i);
        }

        for (int i = 0; i < indices.Count; i++)
        {
            int temp = indices[i];
            int randomIndex = Random.Range(i, indices.Count);
            indices[i] = indices[randomIndex];
            indices[randomIndex] = temp;
        }

        List<Transform> selectedPoints = new List<Transform>();
        for (int i = 0; i < count; i++)
        {
            selectedPoints.Add(points[indices[i]]);
        }
        Debug.Log("Selected Points:");
        foreach (var point in selectedPoints)
        {
            Debug.Log(point.name);
        }

        return selectedPoints;
    }

    List<GameObject> SelectRandomObjects(GameObject[] objects, int count)
    {
        List<GameObject> selectedObjects = new List<GameObject>();
        List<int> indices = new List<int>();
        while (selectedObjects.Count < count)
        {
            int randomIndex = Random.Range(0, objects.Length);
            if (!indices.Contains(randomIndex))
            {
                selectedObjects.Add(objects[randomIndex]);
                indices.Add(randomIndex);
            }
        }
        return selectedObjects;
    }

    IEnumerator SpawnAndMoveObjects(List<GameObject> objects, List<Transform> spawnPoints)
    {
        int targetIndex = 0;

        foreach (var obj in objects)
        {
            if (targetIndex >= availableTargets.Count)
            {
                Debug.LogError("Not enough unique targets available.");
                yield break;
            }

            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
            Transform target = availableTargets[targetIndex];

            obj.transform.position = spawnPoint.position;
            obj.SetActive(true);
            movingObjects.Add(obj);

            StartCoroutine(MoveObject(obj, target));

            targetIndex++;
        }

        while (movingObjects.Count > 0)
        {
            yield return null;
        }
    }

    public void UpdateSW(int newSW)
    {
        SW = newSW;
    }

    public void UpdateSW2(int newSW2)
    {
        SW2 = newSW2;
    }

    IEnumerator MoveObject(GameObject obj, Transform target)
    {   
        targetspriteRenderer.enabled=true;
        targetspriteRenderer2.enabled=true;
        targetspriteRenderer3.enabled=true;
        targetText.gameObject.SetActive(true);
        targetText2.gameObject.SetActive(true);
        targetText3.gameObject.SetActive(true);
        SW = 0;
        SW2 = 0;
        Debug.Log(SW);
        Vector3 startPosition = obj.transform.position;
        Vector3 endPosition = target.position;
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, (elapsedTime / 1f));
            elapsedTime += Time.deltaTime * moveSpeed;
            yield return null;
        }
        obj.transform.position = endPosition;

        while (SW != 1 && SW2 != 1)
        {
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("SW2,ok");
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
        movingObjects.Remove(obj);

        if (!isRespawning)
        {
            Debug.Log("Coroutine,ok");
            StartCoroutine(HandleRespawn(obj));
        }
    }

    IEnumerator HandleRespawn(GameObject obj)
    {
        Debug.Log("Handling Respawn");

        isRespawning = true;

        yield return new WaitForSeconds(0.1f);

        // 追加: リスポーン時にターゲットリストをシャッフルし、使われたターゲットをリセット
        availableTargets = new List<Transform>(targetPositions);
        ShuffleList(availableTargets);
        usedTargets.Clear();

        List<GameObject> newObjects = SelectRandomObjects(gameObjects, 2);

        foreach (var newObj in newObjects)
        {
            newObj.transform.position = SelectRandomPoint(spawnPoints).position;
            newObj.SetActive(true);
            movingObjects.Add(newObj);

            Transform target = GetUniqueTarget();
            StartCoroutine(MoveObject(newObj, target));
        }

        isRespawning = false;
    }

    Transform GetUniqueTarget()
    {
        if (availableTargets.Count == 0)
        {
            Debug.LogError("No unique targets available.");
            return null;
        }

        // 一度使用したターゲットを除外
        Transform target;
        do
        {
            target = availableTargets[Random.Range(0, availableTargets.Count)];
        } while (usedTargets.Contains(target));

        usedTargets.Add(target);
        return target;
    }

    Transform SelectRandomPoint(Transform[] points)
    {
        if (points.Length == 0)
        {
            Debug.LogError("Points array is empty.");
            return null;
        }
        int randomIndex = Random.Range(0, points.Length);
        return points[randomIndex];
    }
}

