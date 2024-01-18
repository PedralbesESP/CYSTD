using UnityEngine;

public class BaseMission : MonoBehaviour
{
    [SerializeField]
    protected string _explaination;
    protected MissionState _state;

    public void SetMissionState(MissionState state) 
    {
        _state = state;
    }

    public MissionState GetMissionState()
    {
        return _state;
    }

    public string GetMissionExplaination()
    {
        return _explaination;
    }

    protected void _CompleteMission()
    {
        SetMissionState(MissionState.DONE);
    }
}