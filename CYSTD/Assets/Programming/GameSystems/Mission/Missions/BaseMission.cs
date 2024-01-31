using UnityEngine;

public class BaseMission : MonoBehaviour
{
    [SerializeField]
    bool enabledOnStart = true;
    [SerializeField]
    protected string _explaination;
    protected MissionState _state;
    protected bool _enabled;

    public bool IsCompleted
    {
        get => _state.Equals(MissionState.DONE);
    }

    void Awake()
    {
        if (enabledOnStart) Enable();
        else Disable();
    }

    protected virtual bool UpdateValidations()
    {
        if (!_enabled) return false;
        if (IsCompleted) return false;
        return true;
    }

    public void SetMissionState(MissionState state) 
    {
        if (_enabled) _state = state;
    }

    public MissionState GetMissionState()
    {
        return _state;
    }

    public virtual string GetMissionExplaination()
    {
        return _explaination;
    }

    public virtual bool IsEnabled()
    {
        return _enabled;
    }

    public virtual void Enable()
    {
        _enabled = true;
    }

    public virtual void Disable()
    {
        _enabled = false;
    }

    protected void _CompleteMission()
    {
        SetMissionState(MissionState.DONE);
        Disable();
    }
}