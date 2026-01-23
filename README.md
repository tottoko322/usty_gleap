# ★後期ゲーム開発概要★
### 目次
1. コンセプト、ストーリー
1. 設計理念について
1. BuffとEffectの設計
1. 操作の設計
1. ディレクトリ構成
## コンセプト、ストーリー
今回のゲームは、ダダサバイバーのような見下ろし方2Dアクションゲームをベースとして、一般的な死の概念を覆したゲームを作成する。具体的には、Playerの体力がなくなったらGAMEOVERではなく、Graveというお墓を生成してステージを進むという感じになる。  
舞台は中世で、主人公が勇者である。
## 設計理念について
今回の開発では、すべてGameObjectにStatusHolder、StatusActionHolder、StatusManagerをアタッチすることによって、共通した汎用性の高い実装を試みた。目的としてはEnemyもPlayerもGraveも同じようにStatusのやり取りをできるようにすることで、多種多様なゲーム実装を可能にするためだ。
- **StatusHolderの役割**  
ScriptableObjectを利用して、オブジェクトごとにStatusを作成
。基本となるBaseStatusと、基本のバフとなるBuffStatusを保持。

- **StatusManagerの役割**  
Statusを有するすべてのGameObjectにアタッチするコンポーネント。Statusを用いた計算ルールを実装する。取得には(baseStatus + addStatus)*multipleStatusのように式構造によって計算・出力し、ダメージや回復の計算も行う。また、バフやエフェクトの適用も行う。

- **StatusActionHolderの役割**  
StatusActionHolderにはStatusActionを格納する。StatusActionには2パターンあり、TargetStatusActionとSelfStatusActionである。
    - **TargetStatusAction**  
    自身のStatusManagerから必要な値の取得、相手のStatusMangerから値変更メソッドを使い、Statusを変更。  
    例） TargetStatusAction.Execute(gameObject, other.gameObject);

    - **SelfStatusAction**  
    自身のStatusMangerを用いた処理。  
    例） SelfStatusAction.Execute(gameObject);

    - **GenerateAction**  
    オブジェクトを生成するための処理。自分自身の位置を用いて処理。  
    例)  GenerateAction.Execute(gameObject);

以下のコンポーネントは必ずアタッチする。  

<img width="304" height="68" alt="image" src="https://github.com/user-attachments/assets/9eba275e-ccae-41b4-a590-19cdd77b363e" />


## BuffとEffectの設計
バフとエフェクトは以下のように定義する。
- **Buff**  
Buffとは、GameObjectのBuffStatusに一時的な値の変化を与えるための要素である。一時的にBuffStatusを合算することで実装する。また、抽象クラスのBuffを継承したScriptableObjectクラスで、メソッドをoverrideすることで、支配的なBuffも適用できる。

- **Effect**  
Effectとは、GameObjectのStatusに一時的な変化を与える要素である。例えば、毒の実装では、等間隔でダメージを与えるという形で実装する。バフに対してBaseStatusへの効果と考えてほしい。

また、以下二つのルールをバフとエフェクトに適用する。
- **同一効果には排他的である**  
効果を与えたオブジェクトが同じ、効果が同じ。これら二つを満たしている効果は重複して存在しない。メソッド内でバリデーションにより防いでいる。

- **持続時間**  
durationとして効果時間を定義し、この時間分効果は持続する。

- **発動間隔**  
staticIntervalで、durationの持続時間の中でどのくらいの間隔で効果が発動するか決める。0の場合は、効果時間の中でずっと発動していることになる。

## 操作の設計
PlayerActionは以下のPC操作に対応して動作する。
- ### 操作方法  
    - **WASD**  
    Playerの移動

    - **マウス(左クリック)**  
    武器の使用

    - **マウス(ホイール)**  
    武器の切り替え
## ディレクトリ構成
ディレクトリは「機能 => ファイル形式 => 意味」の順で構成する。以下に例を示す。

<img width="337" height="285" alt="image" src="https://github.com/user-attachments/assets/b0764d93-db55-4621-b793-4d2cdee7660b" />
