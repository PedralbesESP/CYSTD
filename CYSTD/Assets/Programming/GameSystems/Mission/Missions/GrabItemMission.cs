using UnityEngine;

public class GrabItemMission : BaseMission
{
    [SerializeField]
    Grabbable _missionItem;
    [SerializeField]
    bool DisableItemOnDisablingMission = true;

    void Update()
    {
        if (!UpdateValidations()) return;
        if (!IsCompleted() && Inventory.Instance.HasItem(_missionItem.ItemType))
        {
            _CompleteMission();
        }
    }

    public override void Enable()
    {
        base.Enable();
        _missionItem.gameObject.SetActive(true);
    }

    public override void Disable()
    {
        base.Disable();
        _missionItem.gameObject.SetActive(!DisableItemOnDisablingMission);
    }
}
