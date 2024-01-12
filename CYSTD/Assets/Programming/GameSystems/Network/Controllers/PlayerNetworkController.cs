using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkController : NetworkControllerBase
{
    public override void RecieveData(ParameterSet parameterSet)
    {
        Debug.Log(parameterSet.SenderId);
        foreach (var param in parameterSet.Parameters)
        {
            Debug.Log("Param: " + param.Key + "Value: " + param.Value);
        }
    }

    public override ParameterSet SendData()
    {
        ParameterSet parameterSet = new ParameterSet(Id);
        parameterSet.AddParameter(ParamKey.POSITION, transform.position.Vector3ToString());
        parameterSet.AddParameter(ParamKey.ROTATION, transform.rotation.eulerAngles.Vector3ToString());
        return parameterSet;
    }
}
