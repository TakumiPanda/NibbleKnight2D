using UnityEngine;
using UnityEngine.UI;

public class BossFightManager : MonoBehaviour
{
    [SerializeField]private GameObject _escapeGuards;
    [SerializeField]private GameObject _bounds;
    [SerializeField]private Boss _boss;
    [SerializeField]private GameObject _healthBar;
    private Slider _slider;

    private void Start()
    {
        _slider = _healthBar.GetComponentInChildren<Slider>();    
    }

    private void StartBossFight()
    {
        Debug.Log("In Combat Mode");
        _boss.StopAllCoroutines();
        _escapeGuards.SetActive(true);
        _boss.CurrEnemyState = EnumEnemyState.Combat;
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
}
