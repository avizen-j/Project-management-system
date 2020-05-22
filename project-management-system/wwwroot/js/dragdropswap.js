function allowDrop(ev) {
    ev.preventDefault();
}
function dragStart(ev) {
    ev.dataTransfer.setData("text/plain", ev.target.id);
}
function dropIt(ev) {
    ev.preventDefault();
    let sourceId = ev.dataTransfer.getData("text/plain");
    let sourceIdEl = document.getElementById(sourceId);
    let sourceIdParentEl = sourceIdEl.parentElement;
    let targetEl = document.getElementById(ev.target.id);
    let targetId = ev.target.id;
    let targetParentEl = targetEl.parentElement;
    if (!$.isNumeric(targetId)) {
    if (targetParentEl.id !== sourceIdParentEl.id) {
        if (targetEl.className === sourceIdEl.className) {
            targetParentEl.appendChild(sourceIdEl);
        } else {
            targetEl.appendChild(sourceIdEl);

        }

    } else {
        let holder = targetEl;
        let holderText = holder.innerHTML;
        targetEl.innerHTML = sourceIdEl.innerHTML;
        sourceIdEl.innerHTML = holderText;
        holderText = '';
    }

    
        AjaxDisplayString(sourceId, targetId);
    }
    
}

function AjaxDisplayString(var1, var2) {
    var urlString = '';
    $.ajax({
        url: urlString.concat('UpdateBoardAssignment?assignmentId=', var1, '&status=', var2),
        dataType: "json",
        type: "POST",
        cache: false,
        data: { assignmentId: var1, status: var2},
});
}

