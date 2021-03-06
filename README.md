# TextLoggingService
Logs input to temp file

## Build image in docker
```
docker build https://github.com/wesselbosman/TextLoggingService.git -t textlogger
```

## Then run the image with a volume so you can check the log file on the host
```
docker run -d -v logs:/tmp/ -p 80:80 --name textlogger textlogger
```
if you feeling like a docker swarm cluster you can do below instead and get 10 replicas
```
docker swarm init
docker service create --name textlogger --replicas 10 -p 80:80 --mount source=logs,destination=/tmp/ textlogger
```
You can test a running version here (don't enter personally identifiable data pls, I don't want to get GDPR'd...also I didn't bother with letsencrypt so http only)

GET => http://wessel.drinkstoomuch.coffee/api/logging/read

POST => http://wessel.drinkstoomuch.coffee/api/logging/write

Example JSON
```
{
  "id": 0,
  "date": "01/11/2011",
  "message": "Hello World!"
}
```

Example curl request - Bash
```
curl --header "Content-Type: application/json" \
  --request POST \
  --data '{"id": 0,"date": "01-11-2011","message": "Hello World!"}' \
  http://wessel.drinkstoomuch.coffee/api/logging/write
```
Example curl request - CMD/Powershell
```
curl --header "Content-Type: application/json" `
  --request POST `
  --data '{"id": 0,"date": "01-11-2011","message": "Hello World!"}' `
  http://wessel.drinkstoomuch.coffee/api/logging/write
```

If you mounted a volume you can find the location of the logs in the temp file by running
```
docker volume inspect logs
```
This will give you the mountpoint on the host which you can `cat` e.g.
```
cat /var/lib/docker/volumes/logs/_data/textapp-18-11-28.log
```
Returns
```
1/11/11 12:00:00 AM | 0 | => Sup James!
```
