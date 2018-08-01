var express = require('express');
var app = express();
var bodyParser = require('body-parser');


app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true })); // for parsing application/x-www-form-urlencoded

app.listen(3000, () => console.log('Example app listening on port 3000!'))
// on the request to root (localhost:3000/)
app.post('/', function (req, res) {
    var a=JSON.parse(req.body.data);
    console.log( a[0]["Level"]);
    res.send({"code":1,data:JSON.stringify(a)});
});

app.get('/download', function(req, res){
    var file = __dirname + '/testevent.event';
    res.download(file); // Set disposition and send it.
  });