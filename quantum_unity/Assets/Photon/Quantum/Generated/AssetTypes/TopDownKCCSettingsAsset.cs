// <auto-generated>
// This code was auto-generated by a tool, every time
// the tool executes this code will be reset.
//
// If you need to extend the classes generated to add
// fields or methods to them, please create partial  
// declarations in another file.
// </auto-generated>

using Quantum;
using UnityEngine;

[CreateAssetMenu(menuName = "Quantum/TopDownKCCSettings", order = Quantum.EditorDefines.AssetMenuPriorityStart + 494)]
public partial class TopDownKCCSettingsAsset : AssetBase {
  public Quantum.TopDownKCCSettings Settings;

  public override Quantum.AssetObject AssetObject => Settings;
  public new Quantum.TopDownKCCSettings AssetObjectT => (Quantum.TopDownKCCSettings)AssetObject;
  
  public override void Reset() {
    if (Settings == null) {
      Settings = new Quantum.TopDownKCCSettings();
    }
    base.Reset();
  }
}

public static partial class TopDownKCCSettingsAssetExts {
  public static TopDownKCCSettingsAsset GetUnityAsset(this TopDownKCCSettings data) {
    return data == null ? null : UnityDB.FindAsset<TopDownKCCSettingsAsset>(data);
  }
}
