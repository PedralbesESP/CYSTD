// GENERATED AUTOMATICALLY FROM 'Assets/Programming/GameSystems/Scripts/testNet.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @TestNet : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @TestNet()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""testNet"",
    ""maps"": [
        {
            ""name"": ""test"",
            ""id"": ""e0edd5d2-196d-4337-96b9-3a24d25eba6a"",
            ""actions"": [
                {
                    ""name"": ""connection"",
                    ""type"": ""Value"",
                    ""id"": ""406a9d8c-870e-4ccf-a4b1-023b044b816b"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a45b522d-3941-4b94-b4c1-32069398cd5f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""connection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // test
        m_test = asset.FindActionMap("test", throwIfNotFound: true);
        m_test_connection = m_test.FindAction("connection", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // test
    private readonly InputActionMap m_test;
    private ITestActions m_TestActionsCallbackInterface;
    private readonly InputAction m_test_connection;
    public struct TestActions
    {
        private @TestNet m_Wrapper;
        public TestActions(@TestNet wrapper) { m_Wrapper = wrapper; }
        public InputAction @connection => m_Wrapper.m_test_connection;
        public InputActionMap Get() { return m_Wrapper.m_test; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(TestActions set) { return set.Get(); }
        public void SetCallbacks(ITestActions instance)
        {
            if (m_Wrapper.m_TestActionsCallbackInterface != null)
            {
                @connection.started -= m_Wrapper.m_TestActionsCallbackInterface.OnConnection;
                @connection.performed -= m_Wrapper.m_TestActionsCallbackInterface.OnConnection;
                @connection.canceled -= m_Wrapper.m_TestActionsCallbackInterface.OnConnection;
            }
            m_Wrapper.m_TestActionsCallbackInterface = instance;
            if (instance != null)
            {
                @connection.started += instance.OnConnection;
                @connection.performed += instance.OnConnection;
                @connection.canceled += instance.OnConnection;
            }
        }
    }
    public TestActions @test => new TestActions(this);
    public interface ITestActions
    {
        void OnConnection(InputAction.CallbackContext context);
    }
}
