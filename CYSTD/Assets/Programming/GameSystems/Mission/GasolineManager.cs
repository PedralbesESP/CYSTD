using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GasolineManager : MonoBehaviour
{
    [SerializeField] List<UseItemMission> _generatorMissions;
    [SerializeField] float _secondsToAdd;

    void Update()
    {
        if (_generatorMissions.Any(g => g.GetMissionState().Equals(MissionState.DONE)))
        {
            _generatorMissions.Where(g => g.GetMissionState().Equals(MissionState.DONE)).ToList().ForEach(g =>
            {
                g.AddRequirements(ItemType.GASOLINE);
                g.SetMissionState(MissionState.NOT_DONE);
                Timer.Instance.AddTime(_secondsToAdd);
            });
        }
    }
}
