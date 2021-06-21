import './App.css';
import Unity, { UnityContent } from "react-unity-webgl";
import { useEffect } from 'react';

function App() {

  const unityContext = new UnityContent('UnityBuild/Build/UnityBuild.json', 'UnityBuild/Build/UnityLoader.js');
  
  useEffect(() => {
    unityContext.on("canvas", function (canvas) {
      canvas.width = 100;
      canvas.height = 50;
    });
    window.onbeforeunload = confirmExit;
    function confirmExit()
    {
      return "show warning";
    }
  }, [])

  unityContext.on("debug", (message) => {
    console.log(message);
  });

  function handleOnClickFullscreen() {
    unityContext.setFullscreen(true);
    unityContext.send("GameController", "TestReactJS");
  }

  return (
    <div className="App">
      <header className="App-header">
        <button onClick={handleOnClickFullscreen}>Fullscreen</button>
        <Unity unityContent={unityContext} matchWebGLToCanvasSize={false}></Unity>
      </header>
    </div>
  );
}

export default App;
