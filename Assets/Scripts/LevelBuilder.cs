using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{
    [Header("Level settings")]
    [SerializeField] private Transform _currentPlatform;
    [SerializeField] private GameObject _platformTemplate;
    [Range(6, 25)]
    [SerializeField] private int _amountOfPlatforms = 6;
    [SerializeField] private int _DistanceBetweenLevels = 4;
    [Header("Platform settrings")]
    [SerializeField] private int _minXLenthPlatform = 1;
    [SerializeField] private int _maxXLenthPlatform = 6;
    [Header("")]
    [SerializeField] private int _minYOffsetPlatform = 1;
    [SerializeField] private int _maxYOffsetPlatform = 5;


    private void Start()
    {
        for (int i = 0; i < _amountOfPlatforms; i++)
        {
            GameObject platform = Instantiate(_platformTemplate, transform);

            float lengthPlatformX = Random.Range(_minXLenthPlatform, _maxXLenthPlatform);
            float offsetPlatformY = Random.Range(_minYOffsetPlatform, _maxYOffsetPlatform);

            platform.transform.localScale = new Vector3(lengthPlatformX, platform.transform.localScale.y, platform.transform.localScale.z);

            platform.transform.position = new Vector3(_currentPlatform.position.x + _currentPlatform.localScale.x / 2 + lengthPlatformX / 2, _currentPlatform.position.y + offsetPlatformY, platform.transform.position.z);

            _currentPlatform = platform.transform;
        }
    }
}
