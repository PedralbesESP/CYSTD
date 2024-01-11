using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetworkController : NetworkControllerBase
{
    public override void RecieveData(ParameterSet parameterSet)
    {
        throw new System.NotImplementedException();
    }

    public override ParameterSet SendData()
    {
        ParameterSet parameterSet = new ParameterSet(Id);
        parameterSet.AddParameter(ParamKey.POSITION, transform.position.Vector3ToString());
        parameterSet.AddParameter(ParamKey.ROTATION, transform.rotation.eulerAngles.Vector3ToString());
        return parameterSet;
    }
}
