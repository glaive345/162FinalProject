using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    private ShieldBarManager shieldBarManager;
    private HullBarManager hullBarManager;

    [SerializeField] private float shieldDamageReduction;
    [SerializeField] private float shieldDamageMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        shieldBarManager = this.gameObject.GetComponent<ShieldBarManager>();
        hullBarManager = this.gameObject.GetComponent<HullBarManager>();
    }

    public void takeDamage(float percentTaken)
    {
        if (shieldBarManager.shieldActive)
        {
            shieldBarManager.changeBar(-percentTaken * shieldDamageMultiplier);
            hullBarManager.changeBar(-percentTaken * shieldDamageReduction);
        }
        else
        {
            hullBarManager.changeBar(-percentTaken);
        }
    }
}
