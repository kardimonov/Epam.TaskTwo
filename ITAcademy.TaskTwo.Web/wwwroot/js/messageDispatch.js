function onclick(e) {
    let messageId = e.target.id; 
    let dispatchResult = document.getElementById("result" + messageId)
    $.ajax({
        method: 'POST',
        url: '/Message/SendAgain',
        data: {
            id: messageId
        }
    }).done(function (response) {
        if (response.id == messageId) {
            dispatchResult.innerHTML = response.result;
        }
    })
}
for (var i = 0; i < document.getElementsByName("messages").length; i++) {
    document.getElementsByName("messages")[i].addEventListener("click", onclick);
}