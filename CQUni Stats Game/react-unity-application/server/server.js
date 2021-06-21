const express = require("express");
var cors = require("cors");
var path = require("path");
const app = express();
const fs = require('fs');
const ytdl = require('ytdl-core')
const information = JSON.parse(fs.readFileSync(path.join(process.cwd(), 'server/Information.json')).toString());
const proxy = require('cors-anywhere').createServer({
  originWhitelist: [], // Allow all origins
  requireHeaders: [], // Do not require any headers.
  removeHeaders: [] // Do not remove any headers.
});

let thingo = {};

async function findVideo(url) {
  console.log(url);
  try {
    let info = await ytdl.getInfo(url);
    info = info["player_response"].streamingData.formats.find((e) => e['qualityLabel'] === '360p')['url'];
    thingo = info;
  } catch (error) {
    console.log(error);
  }
}

app.use(cors());
app.use(express.json());
app.use(express.static("build"));

app.get('/proxy/:proxyUrl*', (req, res) => {
  req.url = req.url.replace('/proxy/', '/'); // Strip '/proxy' from the front of the URL, else the proxy won't work.
  proxy.emit('request', req, res);
});

app.get('/Images/:imgName', (req, res) => {
  var __dirname = 'server/Images'
  res.sendFile(req.params.imgName, { root: __dirname });
});

app.get('/api/information', (req, res) => {
  console.log("Information was Sought");
  return res.json(information);
});

app.get('/api/video/:url*', (req, res) => {
  findVideo(req.url.replace('/api/video/', '')).then((thing) => {
    return res.send(thingo);
  });

});

app.get("/*", function (req, res) {
  res.sendFile(path.join(process.cwd(), "build/index.html"), function (err) {
    if (err) {
      console.log("Path is: " + path.join(process.cwd(), "build/index.html"));
      res.status(500).send(err);
    }
  });
});

const PORT = process.env.PORT || 3001;
app.listen(PORT, () => {
  console.log(`Server running on port ${PORT}`);
});
