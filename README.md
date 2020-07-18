English is [here](#OverView)

# 概要
AudioMixerを用いたサウンド周りの開発のサンプルです
サンプルは以下の項目を含みます
- AudioClipのロード〜再生
- AudioMixer を用いた音量制御
- AudioMixer で設定したエフェクト操作


# 環境
|項目 | 設定 |
| --- | --- |
| Unity | Unity2019.4.1f1 (LTS) |
| OSX | MacOS (Addressables をビルドすればWindows でも可能)

## Plugins

以下を使っています
- Zenject([GitHub - modesttree/Zenject: Dependency Injection Framework for Unity3D](https://github.com/modesttree/Zenject))
- [UniTask]([GitHub - Cysharp/UniTask: Provides an efficient allocation free async/await integration to Unity.](https://github.com/Cysharp/UniTask))
- [UniRx]([GitHub - neuecc/UniRx: Reactive Extensions for Unity](https://github.com/neuecc/UniRx))
- 自作エディタ拡張([GitHub - Cova8bitdots/UnityEditorToolExtention: Library for Unity. Extention, EditorTool...](https://github.com/Cova8bitdots/UnityEditorToolExtention))
- 自作サウンドシステム([GitHub - Cova8bitdots/UnitySound: UnitySoundSystem](https://github.com/Cova8bitdots/UnitySound))
- Addressables Importer( [GitHub - favoyang/unity-addressable-importer: A rule based addressable asset importer](https://github.com/favoyang/unity-addressable-importer))
- Unity.Addressables([Unity Addressable Asset system | Addressables | 1.12.0](https://docs.unity3d.com/Packages/com.unity.addressables@1.12/manual/index.html))

# 使い方
1. Assets/003_Scenes/SampleScene.unityを開く
2. 再生

## 移動方法
|Key| 説明 |
| --- | --- |
| W | 全進 |
| A | 左にスライド |
| D | 右にスライド |
| S | 180度ターン |
| Q | 反時計回りに回転 |
| E | 右回りに回転 |


# OverView
Sample for using AudioMixer for sound design


# Settings
|項目 | 設定 |
| --- | --- |
| Unity | Unity2019.4.1f1 (LTS) |
| OSX | MacOS (Addressables をビルドすればWindows でも可能)


## Plugins

This sample uses there plugins.

- Zenject([GitHub - modesttree/Zenject: Dependency Injection Framework for Unity3D](https://github.com/modesttree/Zenject))
- [UniTask]([GitHub - Cysharp/UniTask: Provides an efficient allocation free async/await integration to Unity.](https://github.com/Cysharp/UniTask))
- [UniRx]([GitHub - neuecc/UniRx: Reactive Extensions for Unity](https://github.com/neuecc/UniRx))
- Original Unity Editor Library ([GitHub - Cova8bitdots/UnityEditorToolExtention: Library for Unity. Extention, EditorTool...](https://github.com/Cova8bitdots/UnityEditorToolExtention))
- Original Sound System([GitHub - Cova8bitdots/UnitySound: UnitySoundSystem](https://github.com/Cova8bitdots/UnitySound))
- Addressables Importer( [GitHub - favoyang/unity-addressable-importer: A rule based addressable asset importer](https://github.com/favoyang/unity-addressable-importer))
- Unity.Addressables([Unity Addressable Asset system | Addressables | 1.12.0](https://docs.unity3d.com/Packages/com.unity.addressables@1.12/manual/index.html))

# How to use
1. Open Assets/003_Scenes/SampleScene.unity
2. Play Unity

## KeyMapping
|Key| explanation |
| --- | --- |
| W | move forward |
| A | move left |
| D | moe right  |
| S | turn around |
| Q | counter‐clockwise rotation|
| E | clockwise rotation |
