"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

/* TODO Try and get this working, would be more elegant solution
 * connection.on("ReceiveMessages", function (chatMessage(user, message)[]) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    for (chatmessage in chatMessage) {
        var encodedMsg = chatmessage.user + " says " + msg;
        var li = document.createElement("li");
        li.textContent = encodedMsg;
        document.getElementById("messagesList").appendChild(li);
    }
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
}); */

connection.start().then(function () {
    document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("sendButton").addEventListener("click", function (event) {

    // TODO change this so it doesn't come from the form but the logged-in user's name get's sent
    var message = document.getElementById("messageInput").value;
    var token = JSON.parse(localStorage.getItem('currentUser'));
    connection.invoke("SendMessage", token.username, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});