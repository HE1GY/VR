using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    [SerializeField] private bool _showController;
        
    [SerializeField] private GameObject _handModel;
    [SerializeField] private List<GameObject> _controllerPrefabs;
    [SerializeField] private InputDeviceCharacteristics _inputDeviceCharacteristics;

    private GameObject _spawnedHand;
    private GameObject _spawnedController;
    private Animator _animator;
    
    private InputDevice _targetDevice;
    void Start()
    {
        TryInitialized();
    }
    

    private void Update()
    {
        if (!_targetDevice.isValid)
        {
            TryInitialized();
        }
        else
        {
            if (_showController)
            {
                _spawnedHand.SetActive(false);
                _spawnedController.SetActive(true);
            }
            else
            {
                _spawnedHand.SetActive(true);
                _spawnedController.SetActive(false);
                HandleHandAnimation();
            }
        }
    }

    private void HandleHandAnimation()
    {
        if (_targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float trigger))
        {
            _animator.SetFloat("Trigger",trigger);
        }
        else
        {
            _animator.SetFloat("Trigger",0);
        }
        if (_targetDevice.TryGetFeatureValue(CommonUsages.grip, out float grip))
        {
            _animator.SetFloat("Grib",grip);
        }
        else
        {
            _animator.SetFloat("Grib",0);
        }
    }

    private void TryInitialized()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(_inputDeviceCharacteristics,devices);
        if (devices.Count > 0)
        {
            _targetDevice = devices[0];
            GameObject prefab = _controllerPrefabs.Find(gameObject => gameObject.name == _targetDevice.name);
            if (prefab)
            {
                _spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.LogError("Don't find corresponding controller model");
                _spawnedController = Instantiate(_controllerPrefabs[0], transform);
            }

            _spawnedHand = Instantiate(_handModel, transform);
            _animator = _spawnedHand.GetComponent<Animator>();
        }
        else
        {
            Debug.LogError("there is not Controller");
        }
    }
}
