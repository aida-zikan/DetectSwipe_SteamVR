# Detect Swipe for Steam VR(Unity)

## 準備

1. Steam VRをインポート
<https://www.assetstore.unity3d.com/jp/#!/content/32647>
2. 本パッケージをインポート
2. Asstes/SteamVR/Prefabs/[CameraRig]をSceneにD&D
3. SwipeしたいコントローラーのGameObject(Scene/[CameraRig]/Controller (left)または[CameraRig]/Controller (right))にDetectSwipe_SteamVR/Scripts/ViveController.csをアタッチ

## 使い方

以下のusing ディレクティブを追加。

    using zabaglione.vive;

Swipe検出時のコールバックを登録

    void Start () {
        ViveController vc = GetComponent<ViveController>();
        vc.OnSwipe += OnSwipe;
    }

Swipe検出後の処理

    void OnSwipe(ViveController.SwipeDirection dir) {
    }

使用例はDetectSwipe_SteamVR/Samples/sampleのシーンファイルを開き、Scene/[CameraRig]/Controller (leftにアタッチしているRotateMenu.csを参照してください。
