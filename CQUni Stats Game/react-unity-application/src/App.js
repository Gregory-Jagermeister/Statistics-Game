import './App.css';
import React from 'react'
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

        unityInstance.SendMessage('MyGameObject', 'MyFunction');
        <Unity unityContent={unityContext}></Unity>
      </header>
    </div>
  );
}

export default App;
