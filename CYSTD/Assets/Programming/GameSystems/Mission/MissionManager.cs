using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager Instance;
    [SerializeField]
    List<BaseMission> _missions;
    [SerializeField]
    EndMission _sceneExitMission;
    [SerializeField]
    TMP_Text _missionText;

    bool _hasEndedMissions = false;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (_hasEndedMissions)
        {
            if (_CheckSceneExited())
            {
                GameManager.Instance.Win();
                return;
            } 
        }
        else
        {
            _SetMissionText(GetActiveMissions());
            if (_CheckMissionsCompleted())
            {
                _hasEndedMissions = true;
                _EnableEndMission();
            }
        }
    }

    bool _CheckMissionsCompleted()
    {
        if (_missions == null)
        {
            return false;
        }
        else if (_missions.Count < 1)
        {
            return true;
        }
        else
        {
            return _missions.TrueForAll(m => m.GetMissionState().Equals(MissionState.DONE));
        }
    }

    bool _CheckSceneExited()
    {
        if (_sceneExitMission == null)
        {
            return false;
        }
        return _sceneExitMission.GetMissionState().Equals(MissionState.DONE);
    }

    void _EnableEndMission()
    {
        if (_sceneExitMission != null)
        {
            _sceneExitMission.EnableMission();
        }
        _SetMissionText(_sceneExitMission);
    }

    void _SetMissionText(BaseMission mission) 
    {
        _SetMissionText(new List<BaseMission>() { mission });
    }

    void _SetMissionText(List<BaseMission> missions)
    {
        if (_missionText != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (BaseMission mission in missions)
            {
                if (mission != null)
                {
                    sb.AppendLine(mission.GetMissionExplaination()); 
                }
            }
            _missionText.SetText(sb.ToString());
        }
    }

    public List<BaseMission> GetMissions()
    {
        return _missions;
    }

    public List<BaseMission> GetActiveMissions()
    {
        return _missions.Where(m => 
                                    m.GetMissionState().Equals(MissionState.NOT_DONE) || 
                                    m.GetMissionState().Equals(MissionState.DOING))
                        .ToList();
    }
}
