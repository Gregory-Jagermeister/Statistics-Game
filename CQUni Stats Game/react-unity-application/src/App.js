import './App.css';
import 'react-confirm-alert/src/react-confirm-alert.css';
import Unity, { UnityContent } from "react-unity-webgl";
import { useState, useEffect } from 'react';
import { confirmAlert } from 'react-confirm-alert';

function Game({unityContext, handleOnClickFullscreen}) {
  return (
    <>
      <div className="GameContainer">
        <Unity
          className='Game'
          unityContent={unityContext}>
        </Unity>
      </div>
      <button onClick={handleOnClickFullscreen} className="FullScreen btn btn-info btn-lg">
        <span className="glyphicon glyphicon-fullscreen"></span> Fullscreen
      </button>
    </>
    );
}

function App() {

  const [hasConfirmed, setHasConfirmed] = useState(false)
  const unityContext = new UnityContent('UnityBuild/Build/UnityBuild.json', 'UnityBuild/Build/UnityLoader.js');
  
  useEffect(() => {
    window.onbeforeunload = confirmExit;
    function confirmExit()
    {
      unityContext.send("GameController", "TestReactJS");
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
        {hasConfirmed ?  <header className="App-header"><Game unityContext={unityContext} handleOnClickFullscreen={handleOnClickFullscreen}/></header> : confirmAlert({
        title: 'Disclaimer:',
        message: ' ',
        childrenElement: () => <p>Please note that game play will be recorded for the purpose of third party research. Such game play pertains to:<br/><br/>- Interactions with exhibits <br/>- Quiz answers <br/>- Dialogue choices <br/><br/>Be advised that at no point will the game ask for your personal details and/or sensitive information. If you are uncomfortable with the above information being recorded please exit the game.</p>,
          buttons: [
          {
            label: 'I Agree',
            onClick: () => setHasConfirmed(true)
          },
          {
            label: 'I Disagree',
            onClick: () => window.location.reload()
          }
        ]
        })}        
    </div>
  );


}

export default App;
