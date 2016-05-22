# Sample Blog API

[![Build status](https://ci.appveyor.com/api/projects/status/d4f6ese1h399vlqd?svg=true)](https://ci.appveyor.com/project/akornatskyy/sample-blog-api-net)

A simple blog API written using [ASP.NET Web API](http://www.asp.net/web-api).

## curl

Validation error:

	$ curl -i -X POST -d '{}' http://localhost:41188/api/v1/signin

	HTTP/1.1 400 Bad Request
	Cache-Control: no-cache
	Pragma: no-cache
	Content-Type: application/json; charset=utf-8
	Expires: -1
	Content-Length: 105
	
	{"username":["Required field cannot be left blank."],"password":["Required field
	 cannot be left blank."]}

General error:

	$ curl -i -X POST -d 'username=js&password=password' \
		http://localhost:41188/api/v1/signin

	HTTP/1.1 400 Bad Request
	Cache-Control: no-cache
	Pragma: no-cache
	Content-Type: application/json; charset=utf-8
	Expires: -1
	Content-Length: 78

	{"__ERROR__":["The account is locked. Contact system administrator, please."]}

Valid:

	$ curl -i -X POST -H "Content-Type: application/json" \
		-d '{"username": "demo", "password": "password"}' \
		http://localhost:41188/api/v1/signin

	HTTP/1.1 200 OK
	Cache-Control: no-cache
	Pragma: no-cache
	Content-Type: application/json; charset=utf-8
	Expires: -1
	Content-Length: 19
	
	{"username":"demo"}