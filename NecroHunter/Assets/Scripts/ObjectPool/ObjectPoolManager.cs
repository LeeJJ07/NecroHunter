using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Pool;
using UnityEditor.SceneManagement;

public class ObjectPoolManager : MonoBehaviour
{
    [SerializeField] private bool addToDontDestroyOnLoad = false;

    private GameObject emptyHolder;

    private static GameObject particleSystemsEmpty;
    private static GameObject gameObjectsEmpty;
    private static GameObject soundFXEmpty;

    private static Dictionary<GameObject, ObjectPool<GameObject>> objectPools;
    private static Dictionary<GameObject, GameObject> cloneToPrefabMap;

    public enum EPoolType
    {
        PARTICLE_SYSTEMS,
        GAMEOBJECTS,
        SOUNDFX
    }

    public static EPoolType PoolingType;

    private void Awake()
    {
        objectPools = new Dictionary<GameObject, ObjectPool<GameObject>>();
        cloneToPrefabMap = new Dictionary<GameObject, GameObject>();

        SetupEmpties();
    }

    private void SetupEmpties()
    {
        emptyHolder = new GameObject("Object Pools");

        particleSystemsEmpty = new GameObject("Particle Effects");
        particleSystemsEmpty.transform.SetParent(emptyHolder.transform);

        gameObjectsEmpty = new GameObject("GameObjects");
        gameObjectsEmpty.transform.SetParent(emptyHolder.transform);

        soundFXEmpty = new GameObject("Sound FX");
        soundFXEmpty.transform.SetParent(emptyHolder.transform);

        if (addToDontDestroyOnLoad)
            DontDestroyOnLoad(particleSystemsEmpty.transform.root);
    }

    private static void CreatePool(GameObject prefab, Vector3 pos, Quaternion rot, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: ()=> CreatObject(prefab, pos, rot, poolType),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
            );

        objectPools.Add(prefab, pool);
    }
    private static void CreatePool(GameObject prefab, Transform parent, Quaternion rot, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        ObjectPool<GameObject> pool = new ObjectPool<GameObject>(
            createFunc: () => CreatObject(prefab, parent, rot, poolType),
            actionOnGet: OnGetObject,
            actionOnRelease: OnReleaseObject,
            actionOnDestroy: OnDestroyObject
            );

        objectPools.Add(prefab, pool);
    }

    private static GameObject CreatObject(GameObject prefab, Vector3 pos, Quaternion rot, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab, pos, rot);

        prefab.SetActive(true);

        GameObject parentObject = SetParentObject(poolType);
        obj.transform.SetParent(parentObject.transform);

        return obj;
    }
    private static GameObject CreatObject(GameObject prefab, Transform parent, Quaternion rot, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        prefab.SetActive(false);

        GameObject obj = Instantiate(prefab, parent);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = rot;
        obj.transform.localScale = Vector3.one;

        prefab.SetActive(true);

        return obj;
    }

    private static void OnGetObject(GameObject obj)
    {

    }
    private static void OnReleaseObject(GameObject obj)
    {
        obj.SetActive(false);
    }
    private static void OnDestroyObject(GameObject obj)
    {
        if (cloneToPrefabMap.ContainsKey(obj))
            cloneToPrefabMap.Remove(obj);
    }

    private static GameObject SetParentObject(EPoolType poolType)
    {
        switch (poolType)
        {
            case EPoolType.PARTICLE_SYSTEMS:

                return particleSystemsEmpty;
            case EPoolType.GAMEOBJECTS:

                return gameObjectsEmpty;
            case EPoolType.SOUNDFX:

                return soundFXEmpty;
            default:
                return null;
        }
    }

    private static T SpawnObject<T>(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation, EPoolType poolType = EPoolType.GAMEOBJECTS) where T : Object
    {
        if (!objectPools.ContainsKey(objectToSpawn))
        {
            CreatePool(objectToSpawn, spawnPos, spawnRotation, poolType);
        }

        GameObject obj = objectPools[objectToSpawn].Get();

        if(obj != null)
        {
            if (!cloneToPrefabMap.ContainsKey(obj))
            {
                cloneToPrefabMap.Add(obj, objectToSpawn);
            }
            obj.transform.position = spawnPos;
            obj.transform.rotation = spawnRotation;
            obj.SetActive(true);

            if(typeof(T)== typeof(GameObject))
            {
                return obj as T;
            }

            T component = obj.GetComponent<T>();
            if(component == null)
            {
                Debug.LogError($"Object {objectToSpawn.name} doesn't have component of type {typeof(T)}");
                return null;
            }

            return component;
        }
        return null;
    }

    public static T SpawnObject<T>(T typePrefab,  Vector3 spawnPos, Quaternion spawnRotation, EPoolType poolType = EPoolType.GAMEOBJECTS) where T : Component
    {
        return SpawnObject<T>(typePrefab.gameObject, spawnPos, spawnRotation, poolType);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Vector3 spawnPos, Quaternion spawnRotation, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        return SpawnObject<GameObject>(objectToSpawn, spawnPos, spawnRotation, poolType);
    }
    private static T SpawnObject<T>(GameObject objectToSpawn, Transform parent, Quaternion spawnRotation, EPoolType poolType = EPoolType.GAMEOBJECTS) where T : Object
    {
        if (!objectPools.ContainsKey(objectToSpawn))
        {
            CreatePool(objectToSpawn, parent, spawnRotation, poolType);
        }

        GameObject obj = objectPools[objectToSpawn].Get();

        if (obj != null)
        {
            if (!cloneToPrefabMap.ContainsKey(obj))
            {
                cloneToPrefabMap.Add(obj, objectToSpawn);
            }
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localRotation = spawnRotation;
            obj.SetActive(true);

            if (typeof(T) == typeof(GameObject))
            {
                return obj as T;
            }

            T component = obj.GetComponent<T>();
            if (component == null)
            {
                Debug.LogError($"Object {objectToSpawn.name} doesn't have component of type {typeof(T)}");
                return null;
            }

            return component;
        }
        return null;
    }
    public static T SpawnObject<T>(T typePrefab, Transform parent, Quaternion spawnRotation, EPoolType poolType = EPoolType.GAMEOBJECTS) where T : Component
    {
        return SpawnObject<T>(typePrefab.gameObject, parent, spawnRotation, poolType);
    }

    public static GameObject SpawnObject(GameObject objectToSpawn, Transform parent, Quaternion spawnRotation, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        return SpawnObject<GameObject>(objectToSpawn, parent, spawnRotation, poolType);
    }
    public static void ReturnObjectToPool(GameObject obj, EPoolType poolType = EPoolType.GAMEOBJECTS)
    {
        if(cloneToPrefabMap.TryGetValue(obj, out GameObject prefab))
        {
            GameObject parentObject = SetParentObject(poolType);

            if(obj.transform.parent != parentObject.transform)
            {
                obj.transform.SetParent(parentObject.transform);
            }

            if(objectPools.TryGetValue(prefab, out ObjectPool<GameObject> pool))
            {
                pool.Release(obj);
            }
        }
        else
        {
            Debug.LogWarning("Trying to return an object that is not pooled: " + obj.name);
        }
    }
}
