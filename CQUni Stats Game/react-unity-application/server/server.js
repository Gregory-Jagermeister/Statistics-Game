const express = require("express");
var cors = require("cors");
var path = require("path");
const app = express();

app.use(cors());
app.use(express.json());
app.use(express.static("build"));

app.get('/api/information', (req, res) => {
  console.log("Information was Sought");
  return res.json({
    "art": [
      {
        "artifactId": "Exhibit1",
        "heading": "Poverty #1",
        "imagePath": "https://www.savethechildren.org/content/dam/usa/images/usp/us-education/us-west-virginia-esss-ch1426637-sq.jpg/_jcr_content/renditions/cq5dam.thumbnail.768.768.jpg",
        "videoUrl": "https://www.youtube.com/watch?v=ElG5-nXD0B8",
        "content": "Poverty is bad"
      },
      {
        "artifactId": "Exhibit2",
        "heading": "Poverty #2",
        "imagePath": "https://www.savethechildren.org/content/dam/usa/images/usp/us-education/us-west-virginia-esss-ch1426637-sq.jpg/_jcr_content/renditions/cq5dam.thumbnail.768.768.jpg",
        "videoUrl": "https://www.youtube.com/watch?v=i9aSp9bFmMg",
        "content": "Poverty is not for the faint of heart"
      },
      {
        "artifactId": "Exhibit3",
        "heading": "Poverty #3",
        "imagePath": "https://www.savethechildren.org/content/dam/usa/images/usp/us-education/us-west-virginia-esss-ch1426637-sq.jpg/_jcr_content/renditions/cq5dam.thumbnail.768.768.jpg",
        "videoUrl": "https://www.youtube.com/watch?v=aLwRZibUqL0",
        "content": "Poverty should be fixed give money to all"
      },
      {
        "artifactId": "Exhibit4",
        "heading": "Poverty #3",
        "imagePath": "https://www.savethechildren.org/content/dam/usa/images/usp/us-education/us-west-virginia-esss-ch1426637-sq.jpg/_jcr_content/renditions/cq5dam.thumbnail.768.768.jpg",
        "videoUrl": "https://www.youtube.com/watch?v=aLwRZibUqL0",
        "content": "Poverty should be fixed give money to all"
      }
    ],
    'lvl1Quiz': [
      {
        "question": "How many dick in a Basket",
        "answers": [1, 2, 3, 4],
        "correctAnswer": 4,
        "isMultiChoice" : true
      }
    ],
    'lvl2Quiz': [],
    'lvl3Quiz': [],
    'lvl1Dialouge': [],
    'lvl2Dialouge': [],
    'lvl3Dialouge': [],
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
