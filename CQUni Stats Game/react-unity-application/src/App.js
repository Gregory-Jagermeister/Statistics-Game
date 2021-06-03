import './App.css';
import Unity, { UnityContent } from "react-unity-webgl";



function App() {

  const unityContext = new UnityContent('UnityBuild/Build/UnityBuild.json', 'UnityBuild/Build/UnityLoader.js');

  unityContext.on("debug", (message) => {
    console.log(message);
  });

  function handleOnClickFullscreen() {
    unityContext.setFullscreen(true);
  }

  return (
    <div className="App">
      <header className="App-header">
        <button onClick={handleOnClickFullscreen}>Fullscreen</button>
        <Unity unityContent={unityContext}></Unity>
      </header>
    </div>
  );
}

export default App;
