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
    List<MissionObject> _missions;
    [SerializeField]
    EndMissionObject _sceneExitMission;
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

    void _SetMissionText(MissionObject mission) 
    {
        _SetMissionText(new List<MissionObject>() { mission });
    }

    void _SetMissionText(List<MissionObject> missions)
    {
        if (_missionText != null)
        {
            StringBuilder sb = new StringBuilder();
            foreach (MissionObject mission in missions)
            {
                if (mission != null)
                {
                    sb.AppendLine(mission.GetMissionExplaination()); 
                }
            }
            _missionText.SetText(sb.ToString());
        }
    }

    public List<MissionObject> GetMissions()
    {
        return _missions;
    }

    public List<MissionObject> GetActiveMissions()
    {
        return _missions.Where(m => 
                                    m.GetMissionState().Equals(MissionState.NOT_DONE) || 
                                    m.GetMissionState().Equals(MissionState.DOING))
                        .ToList();
    }
}
