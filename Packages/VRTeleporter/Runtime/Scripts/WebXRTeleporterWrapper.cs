using UnityEngine;
using WebXR;

public class WebXRTeleporterWrapper : MonoBehaviour
{
  public enum UpdateTypes
  {
    Flat,
    Controller,
    ControllerAxis
  }

  [SerializeField]
  private VRTeleporter teleporter = null;
  [SerializeField]
  private UpdateTypes updateType = UpdateTypes.Flat;
  [SerializeField]
  private WebXRController controller = null;
  [SerializeField]
  private float displayTeleportDelay = 0.2f;
  [SerializeField]
  private float minAxisY = 0.5f;

  private float timeShowTeleport = -1f;
  private bool axisNotZero = false;
  private bool showTeleport = false;

  public void SetUpdateType(UpdateTypes updateType)
  {
    this.updateType = updateType;
  }

  public void SetWebXRConrtoller(WebXRController controller)
  {
    this.controller = controller;
  }

  void Start()
  {
    if (teleporter == null)
    {
      teleporter = GetComponent<VRTeleporter>();
    }
  }

  void Update()
  {
    switch (updateType)
    {
      case UpdateTypes.Flat:
        UpdateFlat();
        break;
      case UpdateTypes.Controller:
        UpdateController();
        break;
      case UpdateTypes.ControllerAxis:
        UpdateControllerAxis();
        break;
    }
  }

  void UpdateFlat()
  {
    if (Input.GetMouseButtonDown(0))
    {
      teleporter.ToggleDisplay(true);
    }
    if (Input.GetMouseButtonUp(0))
    {
      teleporter.Teleport();
      teleporter.ToggleDisplay(false);
    }
  }

  void UpdateController()
  {
    if (!controller.isControllerActive)
    {
      teleporter.ToggleDisplay(false);
      return;
    }

    Vector2 thumbstick = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
    Vector2 touchpad = controller.GetAxis2D(WebXRController.Axis2DTypes.Touchpad);

    teleporter.ToggleDisplay(thumbstick.y > 0 || touchpad.y > 0);

    if (controller.GetButtonUp(WebXRController.ButtonTypes.Thumbstick) && thumbstick.y > 0)
    {
      teleporter.Teleport();
    }

    if (controller.GetButtonUp(WebXRController.ButtonTypes.Touchpad) && touchpad.y > 0)
    {
      teleporter.Teleport();
    }
  }

  void UpdateControllerAxis()
  {
    if (!controller.isControllerActive)
    {
      teleporter.ToggleDisplay(false);
      timeShowTeleport = -1f;
      return;
    }

    Vector2 thumbstick = controller.GetAxis2D(WebXRController.Axis2DTypes.Thumbstick);
    Vector2 touchpad = controller.GetAxis2D(WebXRController.Axis2DTypes.Touchpad);

    axisNotZero = thumbstick.y > minAxisY || touchpad.y > minAxisY;
    if (timeShowTeleport < 0 && axisNotZero)
    {
      timeShowTeleport = Time.time + displayTeleportDelay;
    }
    showTeleport = timeShowTeleport > 0 && Time.time >= timeShowTeleport;
    teleporter.ToggleDisplay(showTeleport);

    if (!axisNotZero)
    {
      if (showTeleport)
      {
        teleporter.Teleport();
      }
      timeShowTeleport = -1f;
    }
  }
}
