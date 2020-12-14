function dragstart_handler(ev) {
  console.log("dragStart");

  var dti = ev.dataTransfer.items;

  dti.add(ev.target.id, "text/plain");
  ev.effectAllowed = 'move';
}

function dragover_handler(ev) {
  console.log("dragOver");

  ev.preventDefault();
}

function drop_handler(ev) {
    console.log("Drop");
    var dti = ev.dataTransfer.items;
  
    ev.preventDefault();
    // Get the id of the card and add the moved element to the card's DOM
    const data = ev.dataTransfer.getData("text/plain");
    ev.target.appendChild(document.getElementById(data));

    for (var i=0; i < dti.length; i++) {
    console.log("Drop: item[" + i + "].kind = " + dti[i].kind + " ; item[" + i + "].type = " + dti[i].type);
  
    if ((dti[i].kind == "string") && (dti[i].type.match('^text/plain'))) {
        dti[i].getAsString(function (id) {
            if (ev.target.classList.contains("table-box")) {
                const list = ev.target.querySelector(".table-box-list");
                list.appendChild(document.getElementById(id));
            }                    
        });
    }
    }

    // Calling http controller to save the change in DB
    var xhr = new XMLHttpRequest();
    xhr.open("POST", window.location.origin + "/api/changeTaskCategory");
    xhr.setRequestHeader('Content-Type', 'application/json');

    xhr.send(JSON.stringify({
        taskId: data.substring(4, data.lastIndexOf("-")),
        newCategoryName: ev.target.childNodes[1].innerText
    }));
}

function dragend_handler(ev) {
  console.log("dragEnd");

  ev.dataTransfer.clearData();
}

