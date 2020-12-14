function newTask(list) {
    const template = document.getElementById("card-template");
    const node = template.content.cloneNode(true);
    const task = node.querySelector(".task");

    task.setAttribute("id", "task" + (new Date().getTime()));
    list.appendChild(task);



    const input = list.querySelector(".task:last-child .card-input");
    
    input.addEventListener("keydown", function(e) {
        const input = e.currentTarget;
        const task = input.parentNode;
        const previousTask = task.previousElementSibling;
        const nextTask = task.nextElementSibling;
        if(e.key === "ArrowUp") {
            if(previousTask) {
                const previousInput = previousTask.querySelector(".card-input");
                previousInput.focus();
            }
        } else if(e.key === "ArrowDown") {
            if(nextTask) {
                const nextInput = nextTask.querySelector(".card-input");
                nextInput.focus();
            }
        }
    })
    input.focus();
    // Adding Listener for when focus has ended on new element
    input.addEventListener("blur", (event) => {
        var xhr = new XMLHttpRequest();
        xhr.open("POST", window.location.href +  "api/addTask");
        xhr.setRequestHeader('Content-Type', 'application/json');

        xhr.send(JSON.stringify({
            username: JSON.parse(localStorage.getItem('currentUser')).username,
            title: event.currentTarget.value,
            categoryName: input.parentNode.parentNode.parentNode.childNodes[1].innerText
        }));
        
        xhr.onreadystatechange = function () {
            if (this.readyState != 4) return;

            // If error was because of a corrupt token, tell user, remove token, navigate to login page
            if (xhr.responseText == "User was not found! Please log in again!") {
                window.alert(xhr.responseText)
                window.localStorage.removeItem("currentUser");
                window.location.href = window.location.origin + "/login"
            } else {
                // If it was something else, notify the user
                window.alert("Something went wrong");
            }
        };
    });
    
}

function addTask(list, data) {
    const template = document.getElementById("card-template");
    const node = template.content.cloneNode(true);
    let task = node.querySelector(".task");
    const taskId = "task" + data.id + "-" + (new Date().getTime());

    task.setAttribute("id", taskId);
    list.appendChild(task);

    task = document.getElementById(taskId);

    const input = task.querySelector(".card-input");
    input.value = data.title;
    const editLink = task.querySelector(".edit-task-link");
    editLink.setAttribute("href", editLink.getAttribute("href") + "?taskID=" + data.id);

    const taskUsername = task.querySelector(".task-username");
    taskUsername.innerText = data.username;

    const taskStatus = task.querySelector(".task-status");
    taskStatus.checked = data.done;

    input.addEventListener("keydown", function (e) {
        const input = e.currentTarget;
        const task = input.parentNode;
        const previousTask = task.previousElementSibling;
        const nextTask = task.nextElementSibling;
        if (e.key === "ArrowUp") {
            if (previousTask) {
                const previousInput = previousTask.querySelector(".card-input");
                previousInput.focus();
            }
        } else if (e.key === "ArrowDown") {
            if (nextTask) {
                const nextInput = nextTask.querySelector(".card-input");
                nextInput.focus();
            }
        }
    })
    input.focus();


}

window.addEventListener("DOMContentLoaded", function() {
    const cardBoxes = document.querySelectorAll(".table-box");
    cardBoxes.forEach(function(cardBox) {
        const list = cardBox.querySelector(".table-box-list");
        const button = cardBox.querySelector(".task-button");

        button.addEventListener("click", function() {
            newTask(list);
        })
    })
})

