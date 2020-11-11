(function(){function r(e,n,t){function o(i,f){if(!n[i]){if(!e[i]){var c="function"==typeof require&&require;if(!f&&c)return c(i,!0);if(u)return u(i,!0);var a=new Error("Cannot find module '"+i+"'");throw a.code="MODULE_NOT_FOUND",a}var p=n[i]={exports:{}};e[i][0].call(p.exports,function(r){var n=e[i][1][r];return o(n||r)},p,p.exports,r,e,n,t)}return n[i].exports}for(var u="function"==typeof require&&require,i=0;i<t.length;i++)o(t[i]);return o}return r})()({1:[function(require,module,exports){
var T = new Twit({
    consumer_key: '0Uv01QshcLim4irHA9OcRU7vD',
    consumer_secret: 'Ob2SlXFo3KiMfptKMAOqDNz6lVKpjifq08RYViJWQW0TKGpfAw',
    access_token: '1325414546182430721-2PEKxM3xbmAwmQ8XCJ85wd3QijyrU6',
    access_token_secret: 'lY0FbrSAa1eEDIFAmORjD04kOfkK8lrcP9VZZJxSik3DF',
    //timeout_ms: 60 * 1000,  // optional HTTP request timeout to apply to all requests.
    //strictSSL: true,     // optional - requires SSL certificates to be valid.
})

//
//  tweet 'hello world!'
//
T.post('statuses/update', { status: 'hello world!' }, function (err, data, response) {
    console.log(data)
})


},{}]},{},[1]);
