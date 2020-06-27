
## USocketNet - WordPress Plugin

#### Short Description: Self-Host Realtime Bidirectional Event-Based Communication for your Game or Chat Application. 

The USocketNet is currently designed and developed for Unity Engine. It is a multi-platform by design that can be used through mobile, computers, or even web. We advised the developers to report any issues or bugs immediately to further fix and improve our code. We are driven to add new features that will make this project awesome for everyone.

### Its main features are:

- Realtime WebSocket connection using the stable and reliable socket.io-engine.
- Host your own server anywhere, it can be any VPS, CLOUD, or NodeJS hosting.
- Cross-Platform with Unity, if unity supports it, we will also support it.
- Yes! We support WebGL build even if threading is not allowed on the browser.
- Reconnect event handling which automatically resyncs client to the server.
- Dedicated Realtime GUI backend page for all server instance.
- Stability (socket.io), Security (NodeJS) and Scalability (NGINX).
- Matchmaking mechanism for auto, create, join and lots of options.
- Dedicate and easy to use, we have messaging service for private and public.
- Lots of features to be announce! Stay tuned for more updates.

* Connections are established even in the presence of:
  - proxies and load balancers with Nginx Server.
  - personal firewall and antivirus software by Socket.IO.
  - in memory json data cache with Redis Server.
  - multi instancing and keymetrics by npm PM2.
  - easy npm devDependencies updates NPM npm-gui.

**Note:** USocketNet is not a WebSocket implementation. Although USocketNet indeed uses WebSocket as a transport when possible, it adds some metadata to each packet: the packet type, the namespace and the ack id when a message acknowledgement is needed. That is why a WebSocket client will not be able to successfully connect to a USocketNet server, and a USocketNet client will not be able to connect to a WebSocket server (like `ws://echo.websocket.org`) either.

### IMPORTANT FIXES!

FIXING:
- [ON ACTIVATE PLUGIN] The plugin generated 24 characters of unexpected output during activation. If you notice “headers already sent” messages, problems with syndication feeds or other issues, try deactivating or removing this plugin.

## REST API LIST

### Authentication

CLIENT REQUEST:
- METHOD: POST
- URL: wp_url + /wp-json/usocketnet/v1/auth
- TYPE: x-www-form-urlencoded
- PARAM: UN->Username/Email, PW:->Password

SERVER RESPONSE:
- {"code": "unknown_request", "message": "Please contact your administrator.", "data", null} - Not set Request Type or Params.
- {"code": "empty_username", "message": "<strong>ERROR</strong>: The username field is empty.", "data", null} - Username or Email field is currently empty.
- {"code": "empty_password", "message": "<strong>ERROR</strong>: The password field is empty.", "data", null} - Password field is currently empty.
- {"code": "invalid_username", "message": "Unknown username. Check again or try your email address.", "data", null} - Password field is currently empty.
- {"code": "incorrect_password", "message": "<strong>ERROR</strong>: The password you entered for the username <strong>test</strong> is incorrect. <a href='http://localhost/wordpress/wp-login.php?action=lostpassword\'>Lost your password?</a>", "data", null} - Password field is currently empty.
- {"code": "auth_success", "message": "<strong>Success</strong>: Welcome to USocketNet Rest Api.", "data", "See More Below"} - Password field is currently empty.

## Built With

This project was made possible by NodeJS / OpenJS developers. Thank you for making server side development easy.
Special shoutout to WordPress.org for making the source code public!

## Contributing

Please read [CONTRIBUTING](CONTRIBUTING) for details on our code of conduct, and the process for submitting pull requests to us.

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/BytesCrafter). 

## Authors

* **Bytes Crafter** - [Website](https://www.bytescrafter.net) - [Github](https://github.com/BytesCrafter/USocketNet-on-NodeJS)

## License

This project is licensed under the GNU GPL License - see the [LICENSE](LICENSE) file for details

## Acknowledgments

* NodeJS - OpenJS Developer
* Unity - Game Engine
* StackOverflow :D