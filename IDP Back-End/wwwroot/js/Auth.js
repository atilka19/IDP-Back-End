function login (form) {

    // Gonna finish this up properly and make it pretty
    var xhr = new XMLHttpRequest();
    xhr.open("POST", window.location.href.replace("login", "api/Auth"));
    xhr.setRequestHeader('Content-Type', 'application/json');
    console.log(form.username.value);
    console.log(form.password.value);
    xhr.send(JSON.stringify({
        username: form.username.value,
        password: form.password.value
    }));
    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;

        if (this.status == 200) {

            // If response was OK, save the returned cookie
            var data = JSON.parse(this.responseText);
            window.localStorage.setItem('currentUser', JSON.stringify(
                { username: data.username, token: data.token, isAdmin: data.isAdmin }));
            window.location.href = window.location.origin + "/home";
        } else {
            // If resonse was not OK, display error
            window.alert("Username or Password incorrect!");
        }

        // end of state change: it can be after some time (async)
    };
}
function checkToken() {
    var token = JSON.parse(localStorage.getItem('currentUser'));

    if (token == null) {
        window.alert("You need the be logged in!");
        window.location.href = window.location.origin + "/login"
    } else {
        return;
    }
    
}
function appendUserName(formID, fieldID) {
    document.getElementById(formID).onsubmit = function () {
        var txt = document.getElementById(fieldID);
        txt.value = this.getUserName();
        console.log("gotten username");
    };
}

//function checkToken() {
//    var xhr = new XMLHttpRequest();
//    var token = JSON.parse(localStorage.getItem('currentUser'));
//    var currentUrl = window.location.href;

//    // Checking the current url and setting call to correct one, needed cause default home has 2 endpoints
//    if (currentUrl.indexOf("Home") !== -1 || currentUrl.indexOf("home") !== -1) {
//        if (currentUrl.indexOf("Home") !== -1) {
//           var requestUrl = currentUrl.replace("Home", "api/Auth");
//        } else {
//           var requestUrl = currentUrl.replace("home", "api/Auth");
//        }
//    } else {
//       var requestUrl = currentUrl + "api/Auth";
//    }
//    var request = fetch(requestUrl, {
//        method: 'GET',
//        headers: new Headers({
//            'Content-Type': 'application/json',
//            'Authorization': 'Bearer ' + token && token.token
//        })
//    }).then(response => {
//        console.log("Accepted");
//    }).catch(error => {
//        console.error(error.message);
//    });
//    console.log(request);
//}
function register(form) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", window.location.origin + "/api/User");
    xhr.setRequestHeader('Content-Type', 'application/json');
    console.log(form.username.value);
    console.log(form.password.value);
    // Need to add some sort of validation into here for username and pw
    xhr.send(JSON.stringify({
        username: form.username.value,
        password: form.password.value
    }));

    xhr.onreadystatechange = function () {
        if (this.readyState != 4) return;
        console.log(xhr.responseText)
        // If error was because of a corrupt token, tell user, remove token, navigate to login page
        if (xhr.responseText == "Password must be between 6 and 20 characters." || xhr.responseText == "Password must contain atleast 1 number and letter.") {
            window.alert(xhr.responseText)
        } else {
            // If it was something else, notify the user
            window.alert("Something went wrong");
        }
    };
}