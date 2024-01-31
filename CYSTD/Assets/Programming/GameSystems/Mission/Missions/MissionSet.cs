using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MissionSet : BaseMission
{
    [SerializeField]
    List<BaseMission> _submissions;

    void Update()
    {
        if (!UpdateValidations()) return;
        if (!IsCompleted && _submissions.All(m => m.GetMissionState().Equals(MissionState.DONE)))
        {
            _CompleteMission();
        }
    }

    public override string GetMissionExplaination()
    {
        StringBuilder sb = new StringBuilder();
        if (_explaination != null) sb.AppendLine(_explaination);
        if (_submissions != null && _submissions.Count > 0)
        {
            _submissions.ForEach(m => sb.Append("\t").AppendLine(m.GetMissionExplaination()));
        }
        return sb.ToString();
    }
}
