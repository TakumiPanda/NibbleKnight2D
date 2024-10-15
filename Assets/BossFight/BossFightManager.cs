using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class BossFightManager : MonoBehaviour
{
    [SerializeField]private Boss _boss;
    [SerializeField]private GameObject _healthBar;
    private Slider _slider;

    // Environment Elements
    [SerializeField]private GameObject _escapeGuards;
    [SerializeField]private GameObject _bounds;

    [SerializeField]private GameObject _trashPrefab;
    [SerializeField]private Transform[] _trashDropPoints;
    [SerializeField]private float trashDropMinRate, trashDropMaxRate;

    // private Coroutine coroutineTracker;
    
    private void Start()
    {
        _slider = _healthBar.GetComponentInChildren<Slider>();
        // coroutineTracker = null;
    }

    private void StartBossFight()
    {
        Debug.Log("In Combat Mode");
        _boss.StopAllCoroutines();
        _escapeGuards.SetActive(true);
        _boss.IsInCombat = true;
        _slider.value = 1f;
        _healthBar.SetActive(true);
        _bounds.SetActive(false);
    }

    private void EndBossFight()
    {
        Debug.Log("Combat Ends");
        _escapeGuards.SetActive(false);
        _healthBar.SetActive(false);
    }

    private void UpdateHealthBar(float health)
    {
        _slider.value = health/_boss.EnemyData.MaxHealth;
    }

    private void DropTrash()
    {
        // if (coroutineTracker != null) return;
        // coroutineTracker = 
        StartCoroutine(StartDroppingTrash());
    }

    private IEnumerator StartDroppingTrash()
    {
        while(_boss.CombatState == EnumBossCombatState.HissyFit)
        {
            Instantiate(_trashPrefab, _trashDropPoints[Random.Range(0, _trashDropPoints.Length)].position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(trashDropMinRate, trashDropMaxRate));
        }
        // coroutineTracker = null;
    }
}
