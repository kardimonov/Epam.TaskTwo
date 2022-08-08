function onclick(e) {
    var newPrimaryPhoneId = e.target.value;
    $.ajax({
        method: 'POST',
        url: '/Phone/ResetPrimaryPhone',
        data: {
            phoneId: newPrimaryPhoneId
        }
    })
}
for (var i = 0; i < primaryPhone.phoneId.length; i++) {
    primaryPhone.phoneId[i].addEventListener("click", onclick);
}