function login (form) {

    // Gonna finish this up properly and make it pretty
    var xhr = new XMLHttpRequest();
    xhr.open("POST", window.location.href.replace("login", "api/Auth"));
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.send(JSON.stringify({
        username: form.username.value,
        password: form.password.value
    }));
    console.log(form.username);
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {

            // If response was OK, save the returned cookie
            var data = JSON.parse(this.responseText);
            window.localStorage.setItem('currentUser', JSON.stringify(
                { username: data.username, token: data.token, isAdmin: data.isAdmin }));
            window.location.href = window.location.href.replace("login", "Home");
        } else {
            // If resonse was not OK, display error
            window.alert("Username or Password incorrect!");
        }

        // end of state change: it can be after some time (async)
    };
}
function checkToken() {
    var xhr = new XMLHttpRequest();
    var token = JSON.parse(localStorage.getItem('currentUser'));
    var currentUrl = window.location.href;
    const myHeaders = new Headers({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    });

    myHeaders.append('Content-Type', 'application/json');
    myHeaders.set('Authorization', 'Bearer ' + token);

    console.log(myHeaders.get('Authorization'));
    // Checking the current url and setting call to correct one, needed cause default home has 2 endpoints
    if (currentUrl.indexOf("Home") !== -1 || currentUrl.indexOf("home") !== -1) {
        if (currentUrl.indexOf("Home") !== -1) {
           var requestUrl = currentUrl.replace("Home", "api/Auth");
        } else {
           var requestUrl = currentUrl.replace("home", "api/Auth");
        }
    } else {
       var requestUrl = currentUrl + "api/Auth";
    }

    //xhr.open("GET", requestUrl);
    //xhr.setRequestHeader('Authorization', 'Bearer ' + token && token.token);
    //xhr.setRequestHeader('Content-Type', 'application/json');

    //xhr.send();
    var request = fetch(requestUrl, {
        method: 'GET',
        headers: myHeaders
    }).then(response => {
        console.log("Accepted");
    }).catch(error => {
        console.error(error.message);
    });
    console.log(request);
}

//function checkToken() {
//    var token = JSON.parse(localStorage.getItem('currentUser'));
//    var currentUrl = window.location.href;
//    var xhr = new XMLHttpRequest();
//    // Checking the current url and setting call to correct one, needed cause default home has 2 endpoints
//    //if (currentUrl.indexOf("Home") !== -1 || currentUrl.indexOf("home") !== -1) {
//    //    if (currentUrl.indexOf("Home") !== -1) {
//    //        requestUrl = currentUrl.replace("Home", "api/Auth");
//    //    } else {
//    //        requestUrl = currentUrl.replace("home", "api/Auth");
//    //    }
//    //} else {
//    //    requestUrl = currentUrl + "api/Auth";
//    //}
//            // Checking the current url and setting call to correct one, needed cause default home has 2 endpoints
//    if (currentUrl.indexOf("Home") !== -1 || currentUrl.indexOf("home") !== -1) {
//        if (currentUrl.indexOf("Home") !== -1) {
//            xhr.open("GET", currentUrl.replace("Home", "api/Auth"));
//        } else {
//            xhr.open("GET", currentUrl.replace("home", "api/Auth"));
//        }
//    } else {
//        xhr.open("GET", currentUrl + "api/Auth");
//    }

//    xhr.setRequestHeader('Content-Type', 'application/json');
//    xhr.setRequestHeader("Authorization", "Bearer " + token && token.token);
//    console.log(xhr)
//    console.log(token)
//    xhr.send();
//    xhr.onreadystatechange = function () {
//        if (this.readyState != 4) return;

//        if (this.status == 200) {

//            // If response was OK, everything is fine, no need to disturb user
//            console.log("Cookie accepted");
//        } else {
//            // If resonse was not OK, navigate to login screen

//            window.alert("You need to be logged in to see this site");

//            //if (currentUrl.indexOf("Home") !== -1 || currentUrl.indexOf("home") !== -1) {
//            //    if (currentUrl.indexOf("Home") !== -1) {
//            //        window.location.href =  currentUrl.replace("Home", "login");
//            //    } else {
//            //        window.location.href =  currentUrl.replace("home", "login");
//            //    }
//            //} else {
//            //    window.location.href =  currentUrl + "login";
//            //}
//        }
//    };
//}
function register(form) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", window.location.href.replace("register", "api/User"));
    xhr.setRequestHeader('Content-Type', 'application/json');

    // Need to add some sort of validation into here for username and pw
    xhr.send(JSON.stringify({
        username: form.username.value,
        password: form.password.value
    }));

    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {

            // If response was OK, tell user, navigate to login page
            window.alert("Register Successful! Please log in.");
            window.location.href = window.location.href.replace("register", "login");
        } if (this.status == 401) {
            // If resonse was not OK, display error
            window.alert("Username is already taken!");
        }
    };
}