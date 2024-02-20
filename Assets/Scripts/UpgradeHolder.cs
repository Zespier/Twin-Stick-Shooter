using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHolder : MonoBehaviour {

    public Upgrade upgradePrefab;
    public List<Upgrade> upgrades = new List<Upgrade>();

    public Upgrade TempUpgrade { get; set; }

    public void AddUpgrade(Upgrade upgrade) {

        bool found = false;

        for (int i = 0; i < upgrades.Count; i++) {

            if (upgrades[i].upgradeType == upgrade.upgradeType && upgrades[i].amount == upgrade.amount) {
                upgrades[i].enabled = true;
                found = true;
                break;
            }
        }

        if (!found) {

            TempUpgrade = Instantiate(upgradePrefab, transform);
            TempUpgrade.upgradeType = upgrade.upgradeType;
            TempUpgrade.amount = upgrade.amount;
            if (!TempUpgrade.enabled) { TempUpgrade.enabled = true; }

            upgrades.Add(TempUpgrade);
        }
    }

    public void RemoveUpgrade(Upgrade upgrade) {

        for (int i = 0; i < upgrades.Count; i++) {

            if (upgrades[i].upgradeType == upgrade.upgradeType && upgrades[i].amount == upgrade.amount) {
                upgrades[i].enabled = false;
                break;
            }
        }
    }

}
