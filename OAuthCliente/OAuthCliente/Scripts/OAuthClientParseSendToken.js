﻿function parse_and_send() {
    // http://code.google.com/intl/pt-PT/apis/accounts/docs/OAuth2UserAgent.html

    // First, parse the query string
    var params = {}, queryString = location.hash.substring(1),
    regex = /([^&=]+)=([^&]*)/g, m;
    while (m = regex.exec(queryString)) {
        params[decodeURIComponent(m[1])] = decodeURIComponent(m[2]);
    }

    // And send the token over to the server
    var req = new XMLHttpRequest();
    // consider using POST so query isn't logged
    req.open('GET', 'https://' + window.location.host + '/catchtoken?' + queryString, true);

    req.onreadystatechange = function (e) {
        if (req.readyState == 4) {
            if (req.status == 200) {
                window.location = params['state'];
            }
            else if (req.status == 400) {
                alert('There was an error processing the token.');
            }
            else {
                alert('something else other than 200 was returned');
            }
        }
    };
    req.send(null);
}
