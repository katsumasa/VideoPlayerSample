# VideoPlayerSample
UnityEngine.Video.VideoPlayer Sample

## 概要
UnityEngine.Video.VideoPlayerを使用した動画ファイルのいくつかのサンプルコードを紹介しています。
VideoPlayerのスクリプトリファレンス
https://docs.unity3d.com/ja/2017.4/ScriptReference/Video.VideoPlayer.html

## 動作環境
下記のUnityで動作確認を行っています。
- Unity2017.4.30f1
- Unity2018.4.12f1
- Unity2019.3.11f1

## 収録Sceneについて
下記の４種類のSceneを収録しています。
- Sample1:スクリプトを使用せずVideoPlayerコンポーネントのみを使用してInternet上の動画ファイルを再生するサンプルです。
- Sample2:Internet上の動画ファイルを端末のローカルストレージ(Application.persistentDataPath)へ保存してから再生するサンプルです。
- Sample3:*AssetBundle*化した動画ファイルを再生するサンプルです。(Unity2018.1以降でのみ動作します)
- Sample4:uGUIを使用したシークバーの実装例です。

## 注意事項
- AssetBundleから再生する場合下記に注意して下さい。
  - Unityのバージョンは2018.1以降
  - AssetBundleは非圧縮でビルドしている
  - AssetBundleはApplication.persistentDataPathもしくはApplication.streamingAssetsPath以下に置かれている必要があります。
- Internet上の動画ファイルを作成する場合、iOSのバージョンによってはURLはhttpではなくhttpsで行わないとエラーになる場合があります。
- VideoPlayerにおいてGraphics APIがVulkanに対応しているUnityは2019.2以降です。
