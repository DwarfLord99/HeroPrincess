using UnityEngine;

// This class will hold all the data related to an ability, such as damage, cooldown, range, etc.
// It can be used by the CombatComponent to determine how to execute the ability and apply its effects.

[CreateAssetMenu(fileName = "New Ability", menuName = "Combat/Ability")]
public class AbilityData : ScriptableObject
{
    public float damage;
    public float rageCost;
    public float rageGenerated;
    public float cooldown;
}
