import './App.css';
import Unity, { UnityContent } from "react-unity-webgl";



function App() {

  const unityContext = new UnityContent('UnityBuild/Build/UnityBuild.json', 'UnityBuild/Build/UnityLoader.js');

  return (
    <div className="App">
      <header className="App-header">
        <Unity unityContent={unityContext}></Unity>
      </header>
    </div>
  );
}

export default App;
