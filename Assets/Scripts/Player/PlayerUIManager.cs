using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    [SerializeField] GameObject _player = null;
    [SerializeField] Text _treasureText = null;

    Player _playerScript = null;


    // caching
    private void Awake()
    {
        _playerScript = _player.GetComponent<Player>();
    }

    #region subscriptions
    // subscriptions
    private void OnEnable()
    {
        _playerScript.TreasureCollected += UpdateTreasure;
    }
    

    private void OnDisable()
    {
        _playerScript.TreasureCollected -= UpdateTreasure;
    }
    #endregion


    // on start, set UI values to default OR load from save
    private void Start()
    {
        _treasureText.text = "0";
    }


    // set UI to current treasure count
    private void UpdateTreasure(int treasure)
    {
        _treasureText.text = treasure.ToString();
    }
}
