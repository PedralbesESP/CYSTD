using UnityEngine;

public class GrabItemMission : BaseMission
{
    [SerializeField]
    Grabbable _missionItem;

    void Update()
    {
        if (!_state.Equals(MissionState.DONE) && Inventory.Instance.HasItem(_missionItem.ItemType))
        {
            _CompleteMission();
        }
    }
}
